using SSquared.Lib.Data.Entities;
using SSquared.Lib.OrgChart;

namespace SSquared.Lib.Services
{
    public interface IOrgChartService
    {
        Task<OrgChartNode> GetOrgChartForEmployeeAsync(int employeeId, CancellationToken cancellationToken = default);

        Task<OrgChartNode> GetOrgChartForEmployeeAsync(Employee employee, CancellationToken cancellationToken = default);
    }
}
