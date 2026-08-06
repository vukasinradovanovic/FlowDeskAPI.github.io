using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Domain.Identity;
using FlowDesk.API.Middleware;
using FlowDeskAPI;
using FlowDeskAPI.Extentions;
using FlowWith.API;
using FluentValidation;
using Implementation.UseCases.Validators;
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
// 4. Validation and Settings Services Configuration
// ==========================================
builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
builder.Services.Configure<RoleSettings>(builder.Configuration.GetSection("RoleSettings"));

var app = builder.Build();

// ==========================================
// 5. Seeder Execution Rule
// ==========================================
if (app.RunCommandLineSeeders(args))
    return;

// ==========================================
// 6. HTTP Request Pipeline Configuration
// ==========================================
if (app.Environment.IsLocal())
{
    Console.WriteLine("Lokalno okruzenje.");
    //app.UseSwagger();  
    //app.UseSwaggerUI();
}
else
{
    Console.WriteLine(app.Environment.EnvironmentName);
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularDev");

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseMiddleware<ApiKeyAuthorizationMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();