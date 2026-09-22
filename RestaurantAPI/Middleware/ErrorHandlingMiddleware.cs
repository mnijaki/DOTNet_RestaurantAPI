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
        catch (Exception e)
        {
            _logger.LogError(e, "{Quota}", e.Message);
            // Prevent from passing the exception stack trace to the client.
            // Redirect the client to a page with a generic error message.
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong.");
        }
    }
}