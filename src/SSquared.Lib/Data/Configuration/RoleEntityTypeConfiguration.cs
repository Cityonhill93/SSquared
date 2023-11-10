using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SSquared.Lib.Data.Entities;

namespace SSquared.Lib.Data.Configuration
{
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder
                .Property(r => r.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(r => r.Name)
                .HasMaxLength(100);

            builder.HasData(Role.All);
        }
    }
}
