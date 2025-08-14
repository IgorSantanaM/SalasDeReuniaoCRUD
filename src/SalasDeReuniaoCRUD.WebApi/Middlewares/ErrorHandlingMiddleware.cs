using FluentValidation;
using SalasDeReuniaoCRUD.Application.Exceptions;
using SalasDeReuniaoCRUD.Domain.Core.Exceptions;
using System.Net;
using System.Text.Json;

namespace SalasDeReuniaoCRUD.WebApi.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, title, errors) = exception switch
            {

                ArgumentException argumentException => (
                    HttpStatusCode.BadRequest,
                    argumentException.Message,
                    null
                ),

                ValidException validEx =>(
                    HttpStatusCode.BadRequest,
                    validEx.Message,
                    null
                ),

                ValidationException validationEx => (
                    HttpStatusCode.BadRequest,
                    "Validation failed",
                    validationEx.Errors.Select(e => new { field = e.PropertyName, error = e.ErrorMessage })
                ),
                DomainException domainEx => (
                    HttpStatusCode.BadRequest,
                    domainEx.Message,
                    null
                ),
                NotFoundException notFoundEx => (
                    HttpStatusCode.NotFound,
                    notFoundEx.Message,
                    null
                ),
                _ => (
                    HttpStatusCode.InternalServerError,
                    "An unexpected error occurred, try again later.",
                    null
                )
            };

            var problem = new
            {
                status = (int)statusCode,
                title,
                errors
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var json = JsonSerializer.Serialize(problem, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            });

            await context.Response.WriteAsync(json);
        }
    }
}
