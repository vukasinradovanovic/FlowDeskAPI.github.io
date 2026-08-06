using FlowDeskAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FlowDesk.API.Middleware
{
    public class ApiKeyAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        public ApiKeyAuthorizationMiddleware(RequestDelegate next, AppSettings settings)
        {
            _next = next;
            _appSettings = settings;
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();

            if (endpoint == null) 
            { 
                await _next(context);
                return;
            }

            var attribute = endpoint.Metadata.GetMetadata<ApiKeyAuthorizationAttribute>();

            if (attribute == null)
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.ContainsKey("x-api-key"))
            {
                context.Response.StatusCode = 401;
                return;
            }

            var apiKey = context.Request.Headers["x-api-key"].ToString();
            
            if(!_appSettings.ApiKeys.Contains(apiKey))
            {
                context.Response.StatusCode = 401;
                return;
            }

            await _next(context);
        }
    }
}
