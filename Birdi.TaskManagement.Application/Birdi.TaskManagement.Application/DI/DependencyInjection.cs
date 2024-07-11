using Birdi.TaskManagement.Application.Contract;
using Birdi.TaskManagement.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Birdi.TaskManagement.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveApplicationDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
