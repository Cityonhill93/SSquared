using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SSquared.Lib.Employees;

namespace SSquared.Lib.Data.Configuration
{
    internal class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            const int nameMaxLength = 100;
            builder
                .Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(nameMaxLength);

            builder
                .Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(nameMaxLength);

            builder
                .Property(e => e.EmployeeId)
                .IsRequired()
                .HasMaxLength(10);  //Spec does not specify what the max length is, so I just picked something.
        }
    }
}
