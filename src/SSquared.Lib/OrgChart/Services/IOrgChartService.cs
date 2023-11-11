using SSquared.Lib.Data.Entities;

namespace SSquared.Lib.OrgChart.Services
{
    public interface IOrgChartService
    {
        Task<OrgChartNode> GetOrgChartForEmployeeAsync(int employeeId, CancellationToken cancellationToken = default);

        Task<OrgChartNode> GetOrgChartForEmployeeAsync(Employee employee, CancellationToken cancellationToken = default);
    }
}
