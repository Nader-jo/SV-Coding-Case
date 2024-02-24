using SV_CodingCase.Domain.Contract;
using SV_CodingCase.Domain.Models.Configuration;
using SV_CodingCase.Domain.Repositories;
using SV_CodingCase.Domain.Services;

namespace SV_CodingCase.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly)
                .Configure<ApplicationOptions>(configuration)
                .AddHttpClient()
                .AddMemoryCache()
                .AddServices()
                .AddRepositories()
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()))
                .AddHealthChecks();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISearchService, SearchService>();
            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDataRepository, DataRepository>();
            return services;
        }
    }
}
