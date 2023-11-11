using Microsoft.Extensions.DependencyInjection;

namespace SSquared.Lib.Services.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSSquaredServices(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<IOrgChartService, OrgChartService>()
                .AddTransient<IManagerService, ManagerService>();

        }
    }
}
