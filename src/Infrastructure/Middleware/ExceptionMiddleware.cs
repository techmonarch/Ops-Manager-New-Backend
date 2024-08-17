using Microsoft.AspNetCore.Http;
using OpsManagerAPI.Application.Common.Exceptions;
using OpsManagerAPI.Application.Common.Interfaces;
using OpsManagerAPI.Infrastructure.Exceptions;
using Serilog;
using System.Text;

namespace OpsManagerAPI.Infrastructure.Middleware;

internal class ExceptionMiddleware : IMiddleware
{
    private readonly ISerializerService _jsonSerializer;

    public ExceptionMiddleware(ISerializerService jsonSerializer) => _jsonSerializer = jsonSerializer;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var errorResult = new ApiErrorResponse
            {
                Message = "Service currently unavailable, Please try again later",
                Data = exception.Data,
            };

            // Default status code for technical errors
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            // Log the exception message for technical errors
            Log.Error(exception, "An unexpected error occurred.");

            // Check for specific exceptions and customize the response accordingly
            switch (exception)
            {
                case FluentValidation.ValidationException fluentException:
                    var errorMessages = new StringBuilder("One or More Validations failed.");
                    foreach (var error in fluentException.Errors)
                    {
                        errorMessages.AppendLine(error.ErrorMessage);
                    }

                    errorResult.Message = errorMessages.ToString();
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;

                case UnauthorizedException unauthorizedException:
                    errorResult.Message = unauthorizedException.Message;
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    break;

                case PermissionDeniedException permissionDeniedException:
                    errorResult.Message = permissionDeniedException.Message;
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    break;

                case NotFoundException notFoundException:
                    errorResult.Message = notFoundException.Message;
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;

                case BadHttpRequestException badHttpRequestException:
                    errorResult.Message = badHttpRequestException.Message;
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;

                case ForbiddenException forbiddenException:
                    errorResult.Message = forbiddenException.Message;
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    break;

                case InvalidOperationException invalidOperationException:
                    errorResult.Message = invalidOperationException.Message;
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    break;
            }

            // Write the response if it hasn't started
            var response = context.Response;
            if (!response.HasStarted)
            {
                response.ContentType = "application/json";
                await response.WriteAsync(_jsonSerializer.Serialize(errorResult));
            }
            else
            {
                Log.Warning("Can't write error response. Response has already started.");
            }
        }
    }
}
