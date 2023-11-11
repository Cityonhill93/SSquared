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

            var directReports = await _employeeRepository.GetByManagerId(employee.Id, cancellationToken);
            foreach (var directReport in directReports)
            {
                var managerIsValid = await MayBeManagedByAsync(directReport, potentialManager, cancellationToken);
                if (!managerIsValid)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
