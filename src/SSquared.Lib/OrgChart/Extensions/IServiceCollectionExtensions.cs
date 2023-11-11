using Microsoft.Extensions.DependencyInjection;
using SSquared.Lib.OrgChart.Services;

namespace SSquared.Lib.OrgChart.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddOrgChartService(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient<IOrgChartService, OrgChartService>();

        }
    }
}
