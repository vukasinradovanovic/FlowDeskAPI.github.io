using Application.Exceptions;
using FlowDesk.API.ExceptionLogging;
using FluentValidation;

namespace FlowDesk.API.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionLogger _logger;
        public GlobalExceptionHandlingMiddleware(RequestDelegate next, IExceptionLogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                context.Response.ContentType = "application/json";

                if (ex is ValidationException e)
                {
                    context.Response.StatusCode = 422;
                    var errors = e.Errors.Select(x => new
                    {
                        error = x.ErrorMessage,
                        property = x.PropertyName
                    });

                    await context.Response.WriteAsJsonAsync(errors);
                    return;
                }


                if (ex is UnauthorizedUseCaseException)
                {
                    context.Response.StatusCode = 401;
                    return;
                }

                context.Response.StatusCode = 500;
                Guid id = _logger.Log(ex);

                await context.Response.WriteAsJsonAsync(new
                {
                    message = "An unexpected error has occured. " +
                              $"Please contact support using this parameter: {id}."
                });

            }
        }
    }
}
