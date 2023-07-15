using Domain.Exception;
using System.Net;

namespace UI.Middlewares;

public class ExceptionMiddleware
{
    private RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }

        catch (ApplicationException ex)
        {
            if (ex.GetType().BaseType?.Name == nameof(DomainException)) 
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(ex.Message);
            }
        }
    }

}
