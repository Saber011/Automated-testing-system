using System;
using System.Net;
using System.Threading.Tasks;
using Automated.Testing.System.Analytics.UseCases.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Automated.Testing.System.Analytics.Web
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (EntityNotFoundException e)
            {
                await HandleException(httpContext, HttpStatusCode.NotFound, e);
            }
        }

        private async Task HandleException(HttpContext httpContext, HttpStatusCode code, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)code;
            await httpContext.Response.WriteAsync(exception.Message);
        }
    }
}