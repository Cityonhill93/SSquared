using SSquared.Lib.Arguments;
using SSquared.Lib.Data.Entities;

namespace SSquared.Lib.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> AddAsync(ModifyEmployeeArguments args, CancellationToken cancellationToken = default);

        Task<IEnumerable<Employee>> GetAsync(string? query, CancellationToken cancellationToken = default);

        Task<IEnumerable<Employee>> GetAsync(IEnumerable<int> ids, bool includeNavProperties, CancellationToken cancellationToken = default);

        Task<IEnumerable<Employee>> GetPotentialManagersAsync(int id, CancellationToken cancellationToken = default);

        Task<Employee> UpdateAsync(int id, ModifyEmployeeArguments args, CancellationToken cancellationToken = default);
    }
}
