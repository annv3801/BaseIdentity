using Domain.Entities.Identity;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Identity;
public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);
        builder.HasIndex(r => r.Name);
        builder.HasIndex(r => r.NormalizedName);
        builder.Property(r => r.Id).ValueGeneratedOnAdd();
        builder.ToTable("Identity_Roles");
        builder.HasData(new List<Role>()
        {
            new Role()
            {
                Id = Guid.Parse("49e3275a-d497-4b45-bbcb-3214f3769d7e"),
                Created = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                CreatedById = Guid.Parse("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                LastModifiedById = Guid.Parse("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                Description = "The system Admin Role",
                Name = "System Admin",
                Status = RoleStatus.Active,
                NormalizedName = "SYSTEM ADMIN",
            },
            new Role()
            {
                Id = Guid.Parse("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                Created = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                CreatedById = Guid.Parse("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                LastModifiedById = Guid.Parse("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                Description = "The SP Role",
                Name = "SP",
                Status = RoleStatus.Active,
                NormalizedName = "SP",
            }
        });
    }
}
