using Birdi.TaskManagement.Data.Contract;
using Birdi.TaskManagement.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Birdi.TaskManagement.Data.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveDataDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
