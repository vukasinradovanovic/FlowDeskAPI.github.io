using Application.Flowdesk.Commands.Auth;
using Application.Flowdesk.Commands.Projects;
using Application.Flowdesk.Commands.Teams;
using Application.Flowdesk.Queries.Projects;
using Application.Flowdesk.Queries.Statuses;
using Application.Flowdesk.Queries.Teams;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Domain.Identity;
using FlowDesk.API.Middleware;
using FlowDeskAPI;
using FlowDeskAPI.Extentions;
using FlowWith.API;
using FluentValidation;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. Core Framework Services
// ==========================================
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// ==========================================
// 2. CORS Policy Configuration
// ==========================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// ==========================================
// 3. Application Configuration & Security Services
// ==========================================
var settings = new AppSettings();
builder.Configuration.Bind(settings);
builder.Services.AddHttpContextAccessor();

builder.Services.SetupApplication(settings);
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = settings.JwtSettings.Issuer,
        ValidateIssuer = true,
        ValidAudience = "Any",
        ValidateAudience = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSettings.SecretKey)),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
    cfg.Events.OnTokenValidated = context =>
    {
        var dbContext = context.HttpContext.RequestServices.GetRequiredService<FlowDbContext>();
        var tokenId = context.Principal?.Claims.FirstOrDefault(x => x.Type == "TokenId")?.Value;
        if (string.IsNullOrEmpty(tokenId))
        {
            context.Fail("Unauthorized");
            return Task.CompletedTask;
        }

        AuthToken dbToken = dbContext.AuthTokens.FirstOrDefault(x => x.TokenId == tokenId);

        if (dbToken == null || dbToken.InvalidatedAt.HasValue)
        {
            context.Fail("Unauthorized");
        }

        return Task.CompletedTask;
    };

});

// ==========================================
// 4. Services Configuration
// ==========================================

// 1. Handlers & Core Infrastructure
builder.Services.AddScoped<PermissionHandler>();

// 2. Options Settings
builder.Services.Configure<RoleSettings>(builder.Configuration.GetSection("RoleSettings"));
builder.Services.Configure<StatusSettings>(builder.Configuration.GetSection("StatusSettings"));
builder.Services.Configure<DefaultPermissionSettings>(builder.Configuration.GetSection("DefaultPermissionSettings"));

// 3. Validators
builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProjectValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateProjectValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<DeleteProjectValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateTeamValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateTeamValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<DeleteTeamValidator>();

// 4. Commands & Permissions
builder.Services.AddTransient<IRegisterUserCommand, EfRegisterCommand>();

//          Team Section
builder.Services.AddTransient<IGetUsersTeamQuery, EfGetUserTeamsQuery>();
builder.Services.AddTransient<IGetTeamByIdQuery, EfGetTeamByIdQuery>();
builder.Services.AddTransient<IGetAllTeamsQuery, EfGetAllTeams>();
builder.Services.AddTransient<ICreateTeamCommand, EfCreateTeamCommand>();
builder.Services.AddTransient<IUpdateTeamCommand, EfUpdateTeamCommand>();
builder.Services.AddTransient<IDeleteTeamCommand, EfDeleteTeamCommand>();

//         Project Section
builder.Services.AddTransient<IGetUserProjectsQuery, EfGetAllUserProjectsQuery>();
builder.Services.AddTransient<IGetProjectsQuery, EfGetAllProjectsQuery>();
builder.Services.AddTransient<IGetProjectBySlugQuery, EfGetProjectBySlugQuery>();
builder.Services.AddTransient<ICreateProjectCommand, EfCreateProjectCommand>();
builder.Services.AddTransient<IUpdateProjectCommand, EfUpdateProjectCommand>();
builder.Services.AddTransient<IDeleteProjectCommand, EfDeleteProjectCommand>();

//         Status Section
builder.Services.AddTransient<IGetAllStatusesQuery, EfGetAllStatusesQuery>();


var app = builder.Build();

// ==========================================
// 5. Seeder Execution Rule
// ==========================================
if (app.RunCommandLineSeeders(args))
    return;

// ==========================================
// 6. HTTP Request Pipeline Configuration
// ==========================================
app.UseCors("AllowAngularDev");

if (app.Environment.IsLocal())
{
    Console.WriteLine("Lokalno okruzenje.");
}
else
{
    Console.WriteLine(app.Environment.EnvironmentName);
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseMiddleware<ApiKeyAuthorizationMiddleware>();
app.UseAuthorization();
app.MapControllers();


app.Run();