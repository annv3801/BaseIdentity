using System;
using System.Collections.Generic;
using Application.Common;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Identity
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Name);
            builder.HasIndex(p => p.NormalizedName);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name).IsUnicode().IsRequired().HasMaxLength(Constants.FieldLength.TextMaxLength);
            builder.Property(p => p.NormalizedName).IsUnicode().IsRequired().HasMaxLength(Constants.FieldLength.TextMaxLength);
            builder.Property(p => p.Description).IsUnicode().HasMaxLength(Constants.FieldLength.DescriptionMaxLength);
            builder.Property(p => p.Code).IsUnicode().HasMaxLength(Constants.FieldLength.TextMaxLength).HasDefaultValueSql("'?'");

            builder.ToTable("Identity_Permissions");
            builder.HasData(new List<Permission>()
            {
                new Permission()
                {
                    Id = Guid.Parse("49e3275a-d497-4b45-bbcb-3214f3769d6e"),
                    Created = DateTime.UtcNow,
                    LastModified = DateTime.UtcNow,
                    CreatedById = Guid.Parse("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                    LastModifiedById = Guid.Parse("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                    Description = "The system admin permission",
                    Name = "System Admin",
                    Code = Constants.Permissions.SysAdmin,
                    NormalizedName = "SYSTEM ADMIN",
                },
                new Permission()
                {
                Id = Guid.Parse("49e3275a-d497-4b45-bbcb-3214f3769d6f"),
                Created = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                CreatedById = Guid.Parse("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                LastModifiedById = Guid.Parse("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                Description = "The supply chain user permission",
                Name = "SPC",
                Code = Constants.Permissions.SysAdmin,
                NormalizedName = "SPC",
            }
            });
        }
    }
}