using Microsoft.EntityFrameworkCore;

namespace SSquared.Lib.Data
{
    public class SSquaredDbContextDbContext : DbContext
    {

        public SSquaredDbContextDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "SSquared.db");
        }

        public string DbPath { get; }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
