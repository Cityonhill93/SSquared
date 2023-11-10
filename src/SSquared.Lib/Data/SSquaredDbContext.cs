using Microsoft.EntityFrameworkCore;
using SSquared.Lib.Data.Entities;
using System.Reflection;

namespace SSquared.Lib.Data
{
    public class SSquaredDbContextDbContext : DbContext
    {

        public SSquaredDbContextDbContext()
        {
            DbPath = "./SSquared.db";
        }

        protected string DbPath { get; }

        public DbSet<Employee> Employees => Set<Employee>();

        public DbSet<EmployeeRole> EmployeeRoles => Set<EmployeeRole>();

        public DbSet<Role> Roles => Set<Role>();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("SSquared.Lib"));
        }
    }
}
