namespace ChampionsLeague.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureDI(configuration)
                    .AddApplicationDI();

            // Rate Limiting
            services.AddRateLimiter(option =>
            {
                option.AddConcurrencyLimiter("concurrencuPolicy", opt =>
                {
                    opt.PermitLimit = 3;
                    opt.QueueLimit = 6;
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                }).RejectionStatusCode = 429;
            });

            // Request Timeout
            services.AddRequestTimeouts(opt =>
            {
                opt.AddPolicy("twoSecond", TimeSpan.FromSeconds(1));
            });

            return services;
        }
    }
}
