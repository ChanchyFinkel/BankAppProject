namespace CustomerAccount.WebApi.Middlewares
{
    public class HandlerExecptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HandlerExecptionMiddleware> _logger;

        public HandlerExecptionMiddleware(RequestDelegate next,ILogger<HandlerExecptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

            }
            catch(Exception error)
            {
                _logger.LogError(error,error.Message);
                throw;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseHandlerExecptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HandlerExecptionMiddleware>();
        }
    }
}
