using Microsoft.Extensions.DependencyInjection;

namespace SSquared.Lib.Services.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddOrgChartService(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient<IOrgChartService, OrgChartService>();

        }
    }
}
