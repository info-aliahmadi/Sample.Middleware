using static System.Net.Mime.MediaTypeNames;

namespace Sample.Middleware.Middleware
{
    public class TimingMiddleware
    {
        public readonly ILogger<TimingMiddleware> _logger;
        public readonly RequestDelegate _next;
        public TimingMiddleware(ILogger<TimingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;

            _next = next;

        }

        public async Task Invoke(HttpContext ctx)
        {
            var dateTime = DateTime.UtcNow;

            await _next.Invoke(ctx);
            _logger.LogInformation($"Timing : {ctx.Request.Path}: {DateTime.UtcNow - dateTime}");

        }
    }
}
