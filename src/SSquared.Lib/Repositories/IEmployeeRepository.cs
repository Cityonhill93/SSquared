using SSquared.Lib.Data.Entities;

namespace SSquared.Lib.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetAsync(string? query, CancellationToken cancellationToken = default);
    }
}
