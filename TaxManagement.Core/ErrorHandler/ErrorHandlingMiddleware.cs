using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using TaxManagement.Core.Exceptions;

namespace TaxManagement.Core.ErrorHandler
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

                else
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var result = System.Text.Json.JsonSerializer.Serialize(new {Error = ex.Message});
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(result);
            }
        }

    }
}
