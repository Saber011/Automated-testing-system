using System;
using System.Text.Json;
using System.Threading.Tasks;
using Automated.Testing.System.Core.Execute.models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Automated.Testing.System.Web
{
    internal sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }
        
        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            var response = new ServiceResponse<object>
            {
                Content =  null,
                ResponseInfo = new ResponseInfo
                {
                    ErrorMessage = exception.Message
                },
            };
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}