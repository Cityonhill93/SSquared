using SSquared.Lib.Data.Entities;

namespace SSquared.Lib.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> AddAsync(string firstName, string lastName, string employeeId, int? managerId, IEnumerable<int> roleIds, CancellationToken cancellationToken = default);

        Task<IEnumerable<Employee>> GetAsync(string? query, CancellationToken cancellationToken = default);

        Task<Employee> UpdateAsync(int id, string firstName, string lastName, string employeeId, int? managerId, IEnumerable<int> roleIds, CancellationToken cancellationToken = default);
    }
}
