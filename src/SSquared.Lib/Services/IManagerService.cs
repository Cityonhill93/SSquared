using SSquared.Lib.Data.Entities;

namespace SSquared.Lib.Services
{
    public interface IManagerService
    {
        Task<IEnumerable<Employee>> GetValidManagersForEmployee(Employee employee, CancellationToken cancellationToken = default);

        Task<bool> MayBeManagedByAsync(Employee employee, Employee potentialManager, CancellationToken cancellationToken = default);
    }
}
