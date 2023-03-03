using System;
using System.Collections.Generic;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#pragma warning disable 8602
namespace Infrastructure.Persistence.Configurations.Identity
{
    public class AccountRoleConfiguration : IEntityTypeConfiguration<AccountRole>
    {
        public void Configure(EntityTypeBuilder<AccountRole> builder)
        {
            builder.HasKey(u => new {u.AccountId, u.RoleId});
            builder.HasIndex(u => new {u.AccountId, u.RoleId});
            builder.ToTable("Identity_AccountRoles");
            builder.HasOne(r => r.Role).WithMany(r => r.AccountRoles).HasForeignKey(r => r.RoleId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(u => u.Account).WithMany(u => u.AccountRoles).HasForeignKey(u => u.AccountId).OnDelete(DeleteBehavior.Cascade);
            builder.HasData(new List<AccountRole>()
            {
                new AccountRole()
                {
                    RoleId = Guid.Parse("49e3275a-d497-4b45-bbcb-3214f3769d7e"),
                    AccountId = Guid.Parse("49e3275a-d497-4b45-bbcb-3214f3769d7f")
                },
                new AccountRole()
                {
                    RoleId = Guid.Parse("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                    AccountId = Guid.Parse("49e3275a-d497-4b45-bbcb-3214f3769d7f")
                },
            });
        }
    }
}