using Application;
using Application.Flowdesk.DTO.Auth;
using DataAccess.FlowDesk;
using FlowDesk.API.ExceptionLogging;
using FlowDesk.API.JWT;
using FlowDeskAPI;
using Implementation;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace FlowWith.API
{
    public static class ApiExtensions
    {
        public static bool IsLocal(this IWebHostEnvironment env)
        {
            return env.EnvironmentName == "Development";
        }

        public static void SetupApplication(this IServiceCollection services, AppSettings settings)
        {
            services.AddSingleton(settings);
            services.AddTransient(x => new FlowDbContext(settings.ConnString));
            services.AddTransient<IExceptionLogger, SentryExceptionLogger>();
            services.AddTransient<IApplicationUser, UnauthorizedUser>();
            services.AddTransient<JwtHandler>();

            services.AddTransient<IApplicationUser>(container =>
            {
                var accessor = container.GetService<IHttpContextAccessor>(); //service locator

                if (accessor.HttpContext == null)
                {
                    return new UnauthorizedUser();
                }

                if (!accessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    return new UnauthorizedUser();
                }

                var header = accessor.HttpContext.Request.Headers.Authorization; //Bearer token
                var headerParts = header.ToString().Split(" ");

                if (headerParts.Count() != 2 || headerParts[0] != "Bearer")
                {
                    return new UnauthorizedUser();
                }

                var token = headerParts[1];

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var permissionsClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "PermissionsIds")?.Value;

                var permissions = !string.IsNullOrEmpty(permissionsClaim)
                                                ? JsonConvert.DeserializeObject<List<string>>(permissionsClaim)
                                                : new List<string>();

                return new JwtUser
                {
                    Id = int.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "Id").Value),
                    Email = jwtToken.Claims.FirstOrDefault(x => x.Type == "Email").Value,
                    FirstName = jwtToken.Claims.FirstOrDefault(x => x.Type == "FirstName").Value,
                    LastName = jwtToken.Claims.FirstOrDefault(x => x.Type == "LastName").Value,
                    Username = jwtToken.Claims.FirstOrDefault(x => x.Type == "Username").Value,
                    Permissions = permissions,
                    Role = new RoleResponse
                    {
                        Id = int.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "RoleId").Value),
                        Name = jwtToken.Claims.FirstOrDefault(x => x.Type == "RoleName").Value
                    }
                };
            });
        }
    }
}
