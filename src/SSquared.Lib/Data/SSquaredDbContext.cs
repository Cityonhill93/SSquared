﻿using Microsoft.EntityFrameworkCore;
using SSquared.Lib.Employees;
using System.Reflection;

namespace SSquared.Lib.Data
{
    public class SSquaredDbContextDbContext : DbContext
    {

        public SSquaredDbContextDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "SSquared.db");
        }

        protected string DbPath { get; }

        public DbSet<Employee> Employees => Set<Employee>();

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