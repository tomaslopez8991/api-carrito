using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ShoppingCart.Domain.Exceptions;

namespace ShoppingCart.Infrastructure.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next) => _next = next;

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

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var status = exception switch
        {
            NotFoundException => HttpStatusCode.NotFound,
            ValidationException => HttpStatusCode.BadRequest,
            BusinessException => HttpStatusCode.UnprocessableEntity,
            _ => HttpStatusCode.InternalServerError
        };

        var response = new
        {
            status = (int)status,
            errorType = exception.GetType().Name,
            message = exception.Message
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}
