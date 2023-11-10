using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SSquared.Lib.Data.Entities;

namespace SSquared.Lib.Data.Configuration
{
    public class EmployeeRoleEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeRole>
    {
        public void Configure(EntityTypeBuilder<EmployeeRole> builder)
        {
            builder.HasKey(er => er.Id);

            builder
                .Property(er => er.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(er => er.Employee)
                .WithMany(e => e.EmployeeRoles)
                .HasForeignKey(er => er.EmployeeId);

            builder
                .HasOne(er => er.Role)
                .WithMany(r => r.EmployeeRoles)
                .HasForeignKey(er => er.RoleId);
        }
    }
}
