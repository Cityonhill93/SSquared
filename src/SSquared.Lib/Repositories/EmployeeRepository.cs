﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<Employee> AddAsync(string firstName, string lastName, string employeeId, int? managerId, CancellationToken cancellationToken = default)
        {
            var employee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                EmployeeId = employeeId,
                ManagerId = managerId
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
