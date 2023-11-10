using SSquared.Lib.Data.Entities;

namespace SSquared.Lib.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> AddAsync(string firstName, string lastName, string employeeId, int? managerId, CancellationToken cancellationToken = default);

        Task<IEnumerable<Employee>> GetAsync(string? query, CancellationToken cancellationToken = default);
    }
}
