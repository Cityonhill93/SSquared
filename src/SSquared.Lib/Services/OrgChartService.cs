using SSquared.Lib.Data.Entities;
using SSquared.Lib.Exceptions;
using SSquared.Lib.OrgChart;
using SSquared.Lib.Repositories;

namespace SSquared.Lib.Services
{
    public class OrgChartService : IOrgChartService
    {
        public OrgChartService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        private readonly IEmployeeRepository _employeeRepository;

        public async Task<OrgChartNode> GetOrgChartForEmployeeAsync(int employeeId, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeRepository.GetAsync(employeeId, cancellationToken);
            if (employee is null)
            {
                throw new NotFoundException<Employee>(employeeId);
            }

            return await GetOrgChartForEmployeeAsync(employee, cancellationToken);
        }

        public async Task<OrgChartNode> GetOrgChartForEmployeeAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            var topManager = await GetTopManagerAsync(employee, cancellationToken);
            return await BuildNodeForEmployeeAsync(topManager, cancellationToken);
        }

        private async Task<OrgChartNode> BuildNodeForEmployeeAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            var employeeIds = employee
                .Employees
                .Select(e => e.Id)
                .Distinct()
                .ToList();
            var hydratedEmployees = employeeIds.Any()
                ? await _employeeRepository.GetAsync(
                    ids: employeeIds,
                    includeNavProperties: true,
                    cancellationToken: cancellationToken)
                : new List<Employee>();

            var employeeNodes = new List<OrgChartNode>();
            foreach (var subEmployee in hydratedEmployees)
            {
                var subEmployeeNode = await BuildNodeForEmployeeAsync(subEmployee);
                employeeNodes.Add(subEmployeeNode);
            }

            var node = new OrgChartNode(
                Id: employee.Id,
                FirstName: employee.FirstName,
                LastName: employee.LastName)
            {
                Nodes = employeeNodes
            };

            return node;
        }

        private async Task<Employee> GetTopManagerAsync(Employee employee, CancellationToken cancellationToken)
        {
            var managerId = employee.ManagerId;
            if (managerId is null)
            {
                return employee;
            }

            var manager = await _employeeRepository.GetAsync(managerId.Value);
            if (manager is null)
            {
                throw new NotFoundException<Employee>(managerId.Value);
            }

            return await GetTopManagerAsync(manager, cancellationToken);
        }
    }
}
