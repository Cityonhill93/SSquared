using Microsoft.Extensions.DependencyInjection;

namespace SSquared.Lib.Repositories.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient<IEmployeeRepository, EmployeeRepository>()
                .AddTransient<IRoleRepository, RoleRepository>();
        }
    }
}
