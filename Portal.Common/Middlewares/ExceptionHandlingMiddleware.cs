using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Portal.Common.Exceptions;
using System.Net;

namespace Portal.Common.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly bool _showStackTrace;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, bool showStackTrace = true)
        {
            _next = next;
            _showStackTrace = showStackTrace;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var requestId = context.Request.Headers["portal-correlation-id"];

            string result;
            switch (exception)
            {
                case NotFoundException notFoundException:
                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    result = notFoundException.Message;
                    break;
                case AccessException accessException:
                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    result = accessException.Message;
                    break;
                case ValidationException:
                case Exceptions.ApplicationException:
                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    result = exception.Message;
                    break;
                default:
                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    result = !_showStackTrace ? $"Sorry, something went wrong. Send this id to admin: {requestId}" :
                        $"Correlation Id: {requestId}\n{exception.GetType()}: {exception.Message}\n{exception.StackTrace}" ?? "Sorry, no stack trace.";

                    _logger.LogError(exception, "An internal server error occured");

                    break;
            }

            return context.Response.WriteAsync(result);
        }
    }
}
