﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.Identity.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("0");

                    b.Property<string>("AvatarPhoto")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CoverPhoto")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("EmailConfirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("1");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Gender")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("1");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LastModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("1");

                    b.Property<DateTime?>("LockoutEnd")
                        .HasColumnType("datetime2");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NormalizedEmail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Otp")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)")
                        .HasDefaultValueSql("'000000'");

                    b.Property<int>("OtpCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("OtpValidEnd")
                        .HasColumnType("datetime2");

                    b.Property<bool>("PasswordChangeRequired")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("0");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHashTemporary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PasswordValidUntilDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("1");

                    b.Property<string>("SecurityStamp")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("3");

                    b.Property<bool>("TwoFactorEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("0");

                    b.Property<string>("UserName")
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("Email");

                    b.HasIndex("LastModifiedById");

                    b.HasIndex("NormalizedEmail");

                    b.HasIndex("PhoneNumber")
                        .IsUnique()
                        .HasFilter("[PhoneNumber] IS NOT NULL");

                    b.ToTable("Identity_Accounts", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                            AccessFailedCount = 0,
                            Created = new DateTime(2023, 2, 15, 15, 34, 4, 19, DateTimeKind.Utc).AddTicks(9122),
                            CreatedById = new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                            Email = "nva030801@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Nguyen",
                            Gender = true,
                            LastModified = new DateTime(2023, 2, 15, 15, 34, 4, 19, DateTimeKind.Utc).AddTicks(9119),
                            LastModifiedById = new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                            LastName = "An",
                            LockoutEnabled = true,
                            MiddleName = "Van",
                            NormalizedEmail = "NVA030801@GMAIL.COM",
                            NormalizedUserName = "NVA3801",
                            Otp = "000000",
                            OtpCount = 0,
                            OtpValidEnd = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PasswordChangeRequired = false,
                            PasswordHash = "AMJoiJQ9xLazxisVPXx+lBDRw7wfWBerhXipsLpHNGLXGAAKIeCnwi5XhIRbTbqovA==",
                            PhoneNumber = "0966093801",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "8F412264-B014-458E-AEDE-8A0FA4665A86",
                            Status = 3,
                            TwoFactorEnabled = false,
                            UserName = "nva3801"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Identity.AccountLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProviderDisplayName")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("AccountId");

                    b.HasIndex("LoginProvider", "ProviderKey");

                    b.ToTable("Identity_AccountLogins", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Identity.AccountRole", b =>
                {
                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AccountId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("AccountId", "RoleId");

                    b.ToTable("Identity_AccountRoles", (string)null);

                    b.HasData(
                        new
                        {
                            AccountId = new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                            RoleId = new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7e")
                        },
                        new
                        {
                            AccountId = new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                            RoleId = new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f")
                        });
                });

            modelBuilder.Entity("Domain.Entities.Identity.AccountToken", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoginProvider", "Name", "AccountId");

                    b.HasIndex("AccountId");

                    b.HasIndex("LoginProvider", "Name", "AccountId");

                    b.ToTable("Identity_AccountTokens", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Identity.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)")
                        .HasDefaultValueSql("'?'");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LastModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("LastModifiedById");

                    b.HasIndex("Name");

                    b.HasIndex("NormalizedName");

                    b.ToTable("Identity_Permissions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("49e3275a-d497-4b45-bbcb-3214f3769d6e"),
                            Code = "ROOT:ROOT:SYSADMIN",
                            Created = new DateTime(2023, 2, 15, 15, 34, 4, 20, DateTimeKind.Utc).AddTicks(7215),
                            CreatedById = new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                            Description = "The system admin permission",
                            LastModified = new DateTime(2023, 2, 15, 15, 34, 4, 20, DateTimeKind.Utc).AddTicks(7216),
                            LastModifiedById = new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                            Name = "System Admin",
                            NormalizedName = "SYSTEM ADMIN"
                        },
                        new
                        {
                            Id = new Guid("49e3275a-d497-4b45-bbcb-3214f3769d6f"),
                            Code = "ROOT:ROOT:SYSADMIN",
                            Created = new DateTime(2023, 2, 15, 15, 34, 4, 20, DateTimeKind.Utc).AddTicks(7223),
                            CreatedById = new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                            Description = "The supply chain user permission",
                            LastModified = new DateTime(2023, 2, 15, 15, 34, 4, 20, DateTimeKind.Utc).AddTicks(7223),
                            LastModifiedById = new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                            Name = "SPC",
                            NormalizedName = "SPC"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Identity.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LastModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("LastModifiedById");

                    b.HasIndex("Name");

                    b.HasIndex("NormalizedName");

                    b.ToTable("Identity_Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7e"),
                            Created = new DateTime(2023, 2, 15, 15, 34, 4, 20, DateTimeKind.Utc).AddTicks(8002),
                            CreatedById = new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                            Description = "The system Admin Role",
                            LastModified = new DateTime(2023, 2, 15, 15, 34, 4, 20, DateTimeKind.Utc).AddTicks(8003),
                            LastModifiedById = new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                            Name = "System Admin",
                            NormalizedName = "SYSTEM ADMIN",
                            Status = 1
                        },
                        new
                        {
                            Id = new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                            Created = new DateTime(2023, 2, 15, 15, 34, 4, 20, DateTimeKind.Utc).AddTicks(8009),
                            CreatedById = new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                            Description = "The SP Role",
                            LastModified = new DateTime(2023, 2, 15, 15, 34, 4, 20, DateTimeKind.Utc).AddTicks(8009),
                            LastModifiedById = new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"),
                            Name = "SP",
                            NormalizedName = "SP",
                            Status = 1
                        });
                });

            modelBuilder.Entity("Domain.Entities.Identity.RolePermission", b =>
                {
                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PermissionId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("PermissionId", "RoleId");

                    b.ToTable("Identity_RolePermissions", (string)null);

                    b.HasData(
                        new
                        {
                            PermissionId = new Guid("49e3275a-d497-4b45-bbcb-3214f3769d6e"),
                            RoleId = new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7e")
                        });
                });

            modelBuilder.Entity("Domain.Entities.Identity.Account", b =>
                {
                    b.HasOne("Domain.Entities.Identity.Account", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Domain.Entities.Identity.Account", "LastModifiedBy")
                        .WithMany()
                        .HasForeignKey("LastModifiedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("CreatedBy");

                    b.Navigation("LastModifiedBy");
                });

            modelBuilder.Entity("Domain.Entities.Identity.AccountLogin", b =>
                {
                    b.HasOne("Domain.Entities.Identity.Account", "Account")
                        .WithMany("AccountLogins")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Domain.Entities.Identity.AccountRole", b =>
                {
                    b.HasOne("Domain.Entities.Identity.Account", "Account")
                        .WithMany("AccountRoles")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Identity.Role", "Role")
                        .WithMany("AccountRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Entities.Identity.AccountToken", b =>
                {
                    b.HasOne("Domain.Entities.Identity.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Domain.Entities.Identity.Permission", b =>
                {
                    b.HasOne("Domain.Entities.Identity.Account", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("Domain.Entities.Identity.Account", "LastModifiedBy")
                        .WithMany()
                        .HasForeignKey("LastModifiedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("LastModifiedBy");
                });

            modelBuilder.Entity("Domain.Entities.Identity.Role", b =>
                {
                    b.HasOne("Domain.Entities.Identity.Account", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("Domain.Entities.Identity.Account", "LastModifiedBy")
                        .WithMany()
                        .HasForeignKey("LastModifiedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("LastModifiedBy");
                });

            modelBuilder.Entity("Domain.Entities.Identity.RolePermission", b =>
                {
                    b.HasOne("Domain.Entities.Identity.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Identity.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Entities.Identity.Account", b =>
                {
                    b.Navigation("AccountLogins");

                    b.Navigation("AccountRoles");
                });

            modelBuilder.Entity("Domain.Entities.Identity.Permission", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("Domain.Entities.Identity.Role", b =>
                {
                    b.Navigation("AccountRoles");

                    b.Navigation("RolePermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
