namespace Explorer.API.Utilities
{
    public class DatabaseInitializerMiddleware
    {

        private readonly RequestDelegate _next;

        public DatabaseInitializerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IServiceProvider services)
        {
            var initializer = services.GetRequiredService<DatabaseInitializer>();

          
            await initializer.StakeholdersDatabaseAsync(services);
            await initializer.ToursDatabaseAsync(services);
            await initializer.BlogsDatabaseAsync(services);
            await initializer.PaymentsDatabaseAsync(services);

         
            await _next(httpContext);
        }
    }
}
