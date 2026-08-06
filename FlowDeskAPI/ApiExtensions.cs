using Application;
using DataAccess.FlowDesk;
using FlowDesk.API.ExceptionLogging;
using FlowDesk.API.JWT;
using FlowDeskAPI;
using FluentValidation.Results;
using Implementation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

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

                if(accessor.HttpContext == null)
                {
                    return new UnauthorizedUser();
                }

                if (!accessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    return new UnauthorizedUser();
                }
                
                var header = accessor.HttpContext.Request.Headers.Authorization; //Bearer token
                var headerParts = header.ToString().Split(" ");
                
                if(headerParts.Count() != 2 || headerParts[0] != "Bearer")
                {
                    return new UnauthorizedUser();
                }

                var token = headerParts[1];

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                //jwtToken.Claims

                return new JwtUser
                {
                    Id = int.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "Id").Value),
                    Email = jwtToken.Claims.FirstOrDefault(x => x.Type == "Email").Value,
                };
            });
        }
    }
}
