using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using FluentValidation;

using Microsoft.AspNetCore.Http;

using Notes.Application.Common.Exceptions;

namespace Notes.WebApi.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var responseText = string.Empty;

            switch (exception)
            {
                case ValidationException ex:
                    code = HttpStatusCode.BadRequest;
                    responseText = JsonSerializer.Serialize(ex.Errors);
                    break;
                case EntityNotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
            }
            if (string.IsNullOrEmpty(responseText))
            {
                responseText = JsonSerializer.Serialize(new { error = exception.Message });
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(responseText);
        }
    }
}
