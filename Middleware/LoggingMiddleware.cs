namespace HR_API.Middleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;
    public LoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory?.CreateLogger<LoggingMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory)); ;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path} at {DateTime.Now.ToLongTimeString()}");
        await _next(context);
        _logger.LogInformation($"Response: {context.Response.StatusCode} at {DateTime.Now.ToLongTimeString()}");
    }
}
