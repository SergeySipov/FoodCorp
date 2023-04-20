using System.Diagnostics;

namespace FoodCorp.API.Middleware;

public class RequestsLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public RequestsLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<RequestsLoggingMiddleware>();
    }

    public async Task Invoke(HttpContext context)
    {
        var sw = new Stopwatch();
        try
        {
            sw.Start();

            await _next(context);
        }
        finally
        {
            sw.Stop();

            _logger.LogInformation(
                "Request {method} {url} => {statusCode}" 
                + $"{Environment.NewLine} ################# ExecutionTime: {sw.ElapsedMilliseconds} Elapsed ms #################",
                context.Request?.Method,
                context.Request?.Path.Value,
                context.Response?.StatusCode);
        }
    }
}
