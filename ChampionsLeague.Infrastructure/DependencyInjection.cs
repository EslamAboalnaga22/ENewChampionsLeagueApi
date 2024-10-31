namespace ChampionsLeague.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });

            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<ILeagueRepository, LeagueRepository>();
            services.AddScoped<IKnockoutStagesRepository, KnockoutStagesRepository>();
            services.AddScoped<ICreateMatchesRepository, CreateMatchesRepository>();
            services.AddScoped<IStartOverReposittory, StartOverReposittory>();

            services.AddExceptionHandler<GlobalErrorHandling>();
            services.AddProblemDetails();

            return services;
        }
    }
}
