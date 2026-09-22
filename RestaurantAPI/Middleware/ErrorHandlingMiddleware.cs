using RestaurantAPI.Exceptions;

namespace RestaurantAPI.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    
    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            // Invoke the next middleware in the pipeline.
            // The next middleware will also have 'InvokeAsync. next.Invoke(context)' - that will lead to invoking all middlewares in the chain. 
            await next.Invoke(context);
        }
        catch (NotFoundException e)
        {
            // Prevent from passing the exception stack trace to the client by setting the status code to 'Not Found' with a generic error message.
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Quota}", e.Message);
            // Prevent from passing the exception stack trace to the client by setting the status code to 'Internal Server Error' with a generic error message.
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong.");
        }
    }
}