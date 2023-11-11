using SSquared.Lib.Data.Entities;
using SSquared.Lib.Repositories;

namespace SSquared.Lib.Services
{
    public class ManagerService : IManagerService
    {
        public ManagerService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        private readonly IEmployeeRepository _employeeRepository;

        public async Task<IEnumerable<Employee>> GetValidManagersForEmployee(Employee employee, CancellationToken cancellationToken = default)
        {
            //Looping through each of this is gross, because we will have numerous DB hits, but I don't know that there is a great way to improve it.
            //We can't parallelize them, because EF doesn't let us access the context more than once at a time
            //Given the complexity of the logic we may just need to put up with it
            var allEmployees = await _employeeRepository.GetAsync(cancellationToken);
            var validManagers = new List<Employee>();
            foreach (var potentialManager in allEmployees)
            {
                var isValidManager = await MayBeManagedByAsync(
                    employee: employee,
                    potentialManager: potentialManager,
                    cancellationToken: cancellationToken);
                if (isValidManager)
                {
                    validManagers.Add(potentialManager);
                }
            }

            return validManagers;
        }

        public async Task<bool> MayBeManagedByAsync(Employee employee, Employee potentialManager, CancellationToken cancellationToken = default)
        {
            if (employee.Id == potentialManager.Id)
            {
                //You may not manage yourself
                return false;
            }

            var employees = await GetEmployeesRecursive(employee, cancellationToken);
            if (employees.Any(e => e.Id == potentialManager.Id))
            {
                //You may not be managed by one of your employees
                return false;
            }

            return true;
        }

        //We may want to re-use this at some point....consider moving it to the repo?
        private async Task<IEnumerable<Employee>> GetEmployeesRecursive(Employee employee, CancellationToken cancellationToken)
        {
            var employees = new List<Employee>();

            var directReports = await _employeeRepository.GetByManagerId(employee.Id, cancellationToken);
            employees.AddRange(directReports);
            foreach (var directReport in directReports)
            {
                var indirectReports = await GetEmployeesRecursive(directReport, cancellationToken);
                var indiredctReportsToAdd = indirectReports.Where(idr => !employees.Any(e => e.Id == idr.Id));
                if (indiredctReportsToAdd.Any())
                {
                    employees.AddRange(indiredctReportsToAdd);
                }
            }

            return employees;
        }
    }
}
