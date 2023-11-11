using Microsoft.EntityFrameworkCore;
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

        public async Task<Employee> AddAsync(string firstName, string lastName, string employeeId, int? managerId, IEnumerable<int> roleIds, CancellationToken cancellationToken = default)
        {
            var employee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                EmployeeId = employeeId,
                ManagerId = managerId,
                EmployeeRoles = roleIds
                    .Distinct()
                    .Select(roleId => new EmployeeRole
                    {
                        RoleId = roleId
                    }).ToList()
            };

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

        public async Task<Employee> UpdateAsync(int id, string firstName, string lastName, string employeeId, int? managerId, IEnumerable<int> roleIds, CancellationToken cancellationToken = default)
        {
            var existingEmployee = await GetAsync(id);
            if (existingEmployee is null)
            {
                throw new NotFoundException<Employee>(id);
            }

            existingEmployee.FirstName = firstName;
            existingEmployee.LastName = lastName;
            existingEmployee.EmployeeId = employeeId;
            existingEmployee.ManagerId = managerId;

            var rolesToRemove = existingEmployee
                .EmployeeRoles
                .Where(r => !roleIds.Contains(r.RoleId))
                .ToList();
            foreach (var role in rolesToRemove)
            {
                existingEmployee.EmployeeRoles.Remove(role);
            }

            var rolesToAdd = roleIds
                .Where(id => !existingEmployee.EmployeeRoles.Any(role => role.Id == id))
                .Select(id => new EmployeeRole
                {
                    RoleId = id
                })
                .ToList();

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
