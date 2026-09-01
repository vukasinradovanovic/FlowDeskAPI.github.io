using DataAccess.FlowDesk;
using Domain.Identity;
using FlowDesk.API.Middleware;
using FlowDeskAPI;
using FlowDeskAPI.Extentions;
using FlowWith.API;
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

builder.Services.SetupApplication(settings, builder.Configuration);
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


var app = builder.Build();

// ==========================================
// 4. Seeder Execution Rule
// ==========================================
if (app.RunCommandLineSeeders(args))
    return;

// ==========================================
// 5. HTTP Request Pipeline Configuration
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