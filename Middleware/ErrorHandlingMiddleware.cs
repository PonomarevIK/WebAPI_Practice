namespace HR_API.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        int statusCode = context.Response.StatusCode;
        if (statusCode >= 400)
        {
            string errorMessage = statusCode switch
            {
                400 => "Bad Request",
                403 => "Forbidden",
                404 => "Not Found",
                409 => "Conflict",
                _ => "Something else"
            };
            HttpRequest request = context.Request;
            var response_str = $"{request.Method} {request.Path} failed!\nError {statusCode}: {errorMessage}";
            await context.Response.WriteAsync(response_str);
        }
        
    }
}