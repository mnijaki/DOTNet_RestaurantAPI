using System.Diagnostics;

namespace RestaurantAPI.Middleware;

public class ResponseTimeMiddleware: IMiddleware
{
    private readonly ILogger<ResponseTimeMiddleware> _logger;
    private readonly Stopwatch _stopwatch;
    
    public ResponseTimeMiddleware(ILogger<ResponseTimeMiddleware> logger)
    {
        _logger = logger;
        _stopwatch = new Stopwatch();
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _stopwatch.Start();
        await next.Invoke(context);
        _stopwatch.Stop();
        
        var responseTimeInMilliseconds = _stopwatch.ElapsedMilliseconds;
        if (responseTimeInMilliseconds > 2000)
        {
            _logger.LogInformation("Response time for action [{RequestMethod} {Path}] took {ResponseTimeInMilliseconds} ms", 
                context.Request.Method, context.Request.Path, responseTimeInMilliseconds);
        }
    }
}