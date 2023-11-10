using Microsoft.EntityFrameworkCore;
using SSquared.Lib.Data;
using SSquared.Lib.Data.Entities;

namespace SSquared.Lib.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(SSquaredDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Employee>> Get(string query, CancellationToken cancellationToken = default)
        {
            return await GetQueryable()
                .Where(employee => employee.FirstName.Contains(query)
                    || employee.LastName.Contains(query)
                    || employee.EmployeeId.Contains(query))
                .ToListAsync(cancellationToken);
        }

        protected override IQueryable<Employee> GetQueryable()
        {
            return base
                .GetQueryable()
                .Include(e => e.Manager)
                .Include(e => e.Employees)
                .Include(e => e.EmployeeRoles)
                .ThenInclude(er => er.Role);
        }
    }
}
