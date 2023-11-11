using Microsoft.EntityFrameworkCore;
using SSquared.Lib.Arguments;
using SSquared.Lib.Data;
using SSquared.Lib.Data.Entities;
using SSquared.Lib.Exceptions;

namespace SSquared.Lib.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(SSquaredDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Employee> AddAsync(ModifyEmployeeArguments args, CancellationToken cancellationToken = default)
        {
            var employee = new Employee();
            employee.Modify(args);

            _dbContext
                .Employees
                .Add(employee);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAsync(string? query, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(query))
            {
                return await GetQueryable().ToListAsync(cancellationToken);
            }

            return await GetQueryable()
                .Where(employee => employee.FirstName.Contains(query)
                    || employee.LastName.Contains(query)
                    || employee.EmployeeId.Contains(query))
                .OrderBy(employee => employee.FirstName)
                .ThenBy(employee => employee.LastName)
                .ToListAsync(cancellationToken);
        }

        public override Task<Employee?> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return GetQueryableWithNavProperties().FirstOrDefaultAsync(employee => employee.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Employee>> GetPotentialManagersAsync(int id, CancellationToken cancellationToken = default)
        {
            var employee = await GetAsync(id, cancellationToken);
            if (employee is null)
            {
                throw new NotFoundException<Employee>(id);
            }

            var allOtherEmployees = await GetQueryableWithNavProperties()
                .Where(e => e.Id != id)
                .ToListAsync(cancellationToken);

            return allOtherEmployees
                .Where(e => employee.MayBeManagedBy(e))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();
        }

        public async Task<Employee> UpdateAsync(int id, ModifyEmployeeArguments args, CancellationToken cancellationToken = default)
        {
            var existingEmployee = await GetAsync(id);
            if (existingEmployee is null)
            {
                throw new NotFoundException<Employee>(id);
            }

            existingEmployee.Modify(args);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return existingEmployee;
        }

        protected IQueryable<Employee> GetQueryableWithNavProperties()
        {
            return GetQueryable()
                .Include(e => e.Manager)
                .Include(e => e.Employees)
                .Include(e => e.EmployeeRoles)
                .ThenInclude(er => er.Role);
        }
    }
}
