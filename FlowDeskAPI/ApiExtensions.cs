using Application;
using Application.Flowdesk.Commands.Auth;
using Application.Flowdesk.Commands.Projects;
using Application.Flowdesk.Commands.Tasks;
using Application.Flowdesk.Commands.Teams;
using Application.Flowdesk.DTO.Auth;
using Application.Flowdesk.Interfaces;
using Application.Flowdesk.Queries.Auth;
using Application.Flowdesk.Queries.Permissions;
using Application.Flowdesk.Queries.Projects;
using Application.Flowdesk.Queries.Statuses;
using Application.Flowdesk.Queries.Tasks;
using Application.Flowdesk.Queries.Teams;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using FlowDesk.API.ExceptionLogging;
using FlowDesk.API.JWT;
using FlowDeskAPI;
using Implementation;
using Implementation.Emails;
using Implementation.Permissions;
using Implementation.Permissions.Commands.Auth;
using Implementation.Permissions.Commands.Projects;
using Implementation.Permissions.Commands.Teams;
using Implementation.Permissions.Queries.Projects;
using Implementation.Permissions.Queries.Statuses;
using Implementation.Permissions.Queries.Teams;
using Implementation.Permissions.Validators;
using Implementation.Permissions.Validators.Project_Validators;
using Implementation.Permissions.Validators.Team_Validators;
using Implementation.Permissions__UseCases_.Commands.Auth;
using Implementation.Permissions__UseCases_.Commands.Tasks;
using Implementation.Permissions__UseCases_.Queries.Auth;
using Implementation.Permissions__UseCases_.Queries.Permissions;
using Implementation.Permissions__UseCases_.Queries.Statuses;
using Implementation.Permissions__UseCases_.Queries.Tasks;
using Implementation.Permissions__UseCases_.Validators.Taks_Validators;
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

        public static void SetupApplication(this IServiceCollection services, AppSettings settings, IConfiguration configuration)
        {
            // 1. Handlers & Core Infrastructure
            services.AddSingleton(settings);
            services.AddTransient(x => new FlowDbContext(settings.ConnString));
            services.AddTransient<IExceptionLogger, SentryExceptionLogger>();
            services.AddTransient<IApplicationUser, UnauthorizedUser>();
            services.AddTransient<JwtHandler>();
            services.AddScoped<PermissionHandler>();

            // 2. Options Settings
            services.Configure<RoleSettings>(configuration.GetSection("RoleSettings"));
            services.Configure<StatusSettings>(configuration.GetSection("StatusSettings"));
            services.Configure<DefaultPermissionSettings>(configuration.GetSection("DefaultPermissionSettings"));

            // 3. Validators
            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<CreateProjectValidator>();
            services.AddTransient<UpdateProjectValidator>();
            services.AddTransient<DeleteProjectValidator>();
            services.AddTransient<CreateTeamValidator>();
            services.AddTransient<UpdateTeamValidator>();
            services.AddTransient<DeleteTeamValidator>();
            services.AddTransient<CreateTaskValidator>();

            // 4. Commands & Permissions
            services.AddTransient<IRegisterUserCommand, EfRegisterCommand>();
            services.AddTransient<IGetAllUsersQuery, EfGetAllUsersQuery>();
            services.AddTransient<IGetUseCaseLogQuery, EfGetUseCaseLogQuery>();

            //          Team Section
            services.AddTransient<IGetUsersTeamQuery, EfGetUserTeamsQuery>();
            services.AddTransient<IGetTeamByIdQuery, EfGetTeamByIdQuery>();
            services.AddTransient<IGetAllTeamsQuery, EfGetAllTeams>();
            services.AddTransient<ICreateTeamCommand, EfCreateTeamCommand>();
            services.AddTransient<IUpdateTeamCommand, EfUpdateTeamCommand>();
            services.AddTransient<IDeleteTeamCommand, EfDeleteTeamCommand>();

            //         Project Section
            services.AddTransient<IGetUserProjectsQuery, EfGetAllUserProjectsQuery>();
            services.AddTransient<IGetProjectsQuery, EfGetAllProjectsQuery>();
            services.AddTransient<IGetProjectBySlugQuery, EfGetProjectBySlugQuery>();
            services.AddTransient<ICreateProjectCommand, EfCreateProjectCommand>();
            services.AddTransient<IUpdateProjectCommand, EfUpdateProjectCommand>();
            services.AddTransient<IDeleteProjectCommand, EfDeleteProjectCommand>();

            //         Task Section
            services.AddTransient<IGetAllTasksQuery, EfGetAllTasksQuery>();
            services.AddTransient<IGetUsersTasksQuery, EfGetUserTasks>();
            services.AddTransient<IGetTaskBySlugQuery, EfGetTaskBySlugQuery>();
            services.AddTransient<ICreateTaskCommand, EfCreateTaskCommand>();

            //         Status Section
            services.AddTransient<IGetAllStatusesQuery, EfGetAllStatusesQuery>();
            services.AddTransient<IGetStatusByIdQuery, EfGetStatusByIdQuery>();

            //         Email Section
            services.AddTransient<EmailTemplateComposer>();
            services.AddSingleton<IEmailSender, SmtpEmailSender>(x =>
            {
                return new SmtpEmailSender(settings.EmailSettings.FromEmail, settings.EmailSettings.AppPassword, settings.EmailSettings.SmtpHost, settings.EmailSettings.SmtpPort, settings.EmailSettings.Username);
            });
            services.AddTransient<IActivateAccountCommand, EfActivateAccountCommand>();



            services.AddTransient<IApplicationUser>(container =>
            {
                var accessor = container.GetService<IHttpContextAccessor>();

                if (accessor.HttpContext == null)
                {
                    return new UnauthorizedUser();
                }

                if (!accessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    return new UnauthorizedUser();
                }

                var header = accessor.HttpContext.Request.Headers.Authorization;
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
