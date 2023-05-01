using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class InitTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Identity_Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "1"),
                    AvatarPhoto = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CoverPhoto = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PasswordChangeRequired = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    PasswordValidUntilDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordHashTemporary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "1"),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValueSql: "3"),
                    UserName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "1"),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    LockoutEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "1"),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false, defaultValueSql: "0"),
                    Otp = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true, defaultValueSql: "'000000'"),
                    OtpValidEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OtpCount = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identity_Accounts_Identity_Accounts_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Identity_Accounts_Identity_Accounts_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DMP_Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMP_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DMP_Bookings_Identity_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DMP_Bookings_Identity_Accounts_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DMP_Bookings_Identity_Accounts_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DMP_Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShortenUrl = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMP_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DMP_Categories_Identity_Accounts_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DMP_Categories_Identity_Accounts_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DMP_Theaters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMP_Theaters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DMP_Theaters_Identity_Accounts_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DMP_Theaters_Identity_Accounts_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Identity_AccountLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_AccountLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_Identity_AccountLogins_Identity_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identity_AccountTokens",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_AccountTokens", x => new { x.LoginProvider, x.Name, x.AccountId });
                    table.ForeignKey(
                        name: "FK_Identity_AccountTokens_Identity_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identity_Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, defaultValueSql: "'?'"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identity_Permissions_Identity_Accounts_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Identity_Permissions_Identity_Accounts_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Identity_Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identity_Roles_Identity_Accounts_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Identity_Roles_Identity_Accounts_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DMP_Films",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortenUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Actor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Premiere = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMP_Films", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DMP_Films_DMP_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "DMP_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DMP_Films_Identity_Accounts_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DMP_Films_Identity_Accounts_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DMP_Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    TheaterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMP_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DMP_Rooms_DMP_Theaters_TheaterId",
                        column: x => x.TheaterId,
                        principalTable: "DMP_Theaters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DMP_Rooms_Identity_Accounts_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DMP_Rooms_Identity_Accounts_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Identity_AccountRoles",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_AccountRoles", x => new { x.AccountId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Identity_AccountRoles_Identity_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Identity_AccountRoles_Identity_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Identity_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identity_RolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_RolePermissions", x => new { x.PermissionId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Identity_RolePermissions_Identity_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Identity_Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Identity_RolePermissions_Identity_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Identity_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DMP_FilmSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilmId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMP_FilmSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DMP_FilmSchedules_DMP_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "DMP_Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DMP_FilmSchedules_DMP_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "DMP_Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DMP_FilmSchedules_Identity_Accounts_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DMP_FilmSchedules_Identity_Accounts_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DMP_BookingDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMP_BookingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DMP_BookingDetails_DMP_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "DMP_Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DMP_BookingDetails_DMP_FilmSchedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "DMP_FilmSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DMP_BookingDetails_Identity_Accounts_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DMP_BookingDetails_Identity_Accounts_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DMP_Seats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMP_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DMP_Seats_DMP_FilmSchedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "DMP_FilmSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DMP_Seats_Identity_Accounts_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DMP_Seats_Identity_Accounts_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DMP_Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMP_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DMP_Tickets_DMP_FilmSchedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "DMP_FilmSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DMP_Tickets_Identity_Accounts_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DMP_Tickets_Identity_Accounts_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "Identity_Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Identity_Accounts",
                columns: new[] { "Id", "AvatarPhoto", "CoverPhoto", "Created", "CreatedById", "Email", "EmailConfirmed", "FirstName", "Gender", "LastModified", "LastModifiedById", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "Otp", "OtpCount", "OtpValidEnd", "PasswordHash", "PasswordHashTemporary", "PasswordValidUntilDate", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "UserName" },
                values: new object[] { new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"), null, null, new DateTime(2023, 4, 30, 15, 14, 7, 445, DateTimeKind.Utc).AddTicks(642), new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"), "nva030801@gmail.com", true, "Nguyen", true, new DateTime(2023, 4, 30, 15, 14, 7, 445, DateTimeKind.Utc).AddTicks(638), new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"), "An", true, null, "Van", "NVA030801@GMAIL.COM", "NVA3801", "000000", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AMJoiJQ9xLazxisVPXx+lBDRw7wfWBerhXipsLpHNGLXGAAKIeCnwi5XhIRbTbqovA==", null, null, "0966093801", true, "811033A0-22BE-4804-9BBA-4A634AA333C6", 3, "nva3801" });

            migrationBuilder.InsertData(
                table: "Identity_Permissions",
                columns: new[] { "Id", "Code", "Created", "CreatedById", "Description", "LastModified", "LastModifiedById", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("49e3275a-d497-4b45-bbcb-3214f3769d6e"), "ROOT:ROOT:SYSADMIN", new DateTime(2023, 4, 30, 15, 14, 7, 445, DateTimeKind.Utc).AddTicks(9850), new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"), "The system admin permission", new DateTime(2023, 4, 30, 15, 14, 7, 445, DateTimeKind.Utc).AddTicks(9851), new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"), "System Admin", "SYSTEM ADMIN" },
                    { new Guid("49e3275a-d497-4b45-bbcb-3214f3769d6f"), "ROOT:ROOT:SYSADMIN", new DateTime(2023, 4, 30, 15, 14, 7, 445, DateTimeKind.Utc).AddTicks(9860), new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"), "The supply chain user permission", new DateTime(2023, 4, 30, 15, 14, 7, 445, DateTimeKind.Utc).AddTicks(9861), new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"), "SPC", "SPC" }
                });

            migrationBuilder.InsertData(
                table: "Identity_Roles",
                columns: new[] { "Id", "Created", "CreatedById", "Description", "LastModified", "LastModifiedById", "Name", "NormalizedName", "Status" },
                values: new object[,]
                {
                    { new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7e"), new DateTime(2023, 4, 30, 15, 14, 7, 446, DateTimeKind.Utc).AddTicks(859), new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"), "The system Admin Role", new DateTime(2023, 4, 30, 15, 14, 7, 446, DateTimeKind.Utc).AddTicks(860), new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"), "System Admin", "SYSTEM ADMIN", 1 },
                    { new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"), new DateTime(2023, 4, 30, 15, 14, 7, 446, DateTimeKind.Utc).AddTicks(866), new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"), "The SP Role", new DateTime(2023, 4, 30, 15, 14, 7, 446, DateTimeKind.Utc).AddTicks(866), new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"), "SP", "SP", 1 }
                });

            migrationBuilder.InsertData(
                table: "Identity_AccountRoles",
                columns: new[] { "AccountId", "RoleId" },
                values: new object[] { new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"), new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7e") });

            migrationBuilder.InsertData(
                table: "Identity_AccountRoles",
                columns: new[] { "AccountId", "RoleId" },
                values: new object[] { new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f"), new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7f") });

            migrationBuilder.InsertData(
                table: "Identity_RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { new Guid("49e3275a-d497-4b45-bbcb-3214f3769d6e"), new Guid("49e3275a-d497-4b45-bbcb-3214f3769d7e") });

            migrationBuilder.CreateIndex(
                name: "IX_DMP_BookingDetails_BookingId",
                table: "DMP_BookingDetails",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_BookingDetails_CreatedById",
                table: "DMP_BookingDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_BookingDetails_LastModifiedById",
                table: "DMP_BookingDetails",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_BookingDetails_ScheduleId",
                table: "DMP_BookingDetails",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Bookings_AccountId",
                table: "DMP_Bookings",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Bookings_CreatedById",
                table: "DMP_Bookings",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Bookings_LastModifiedById",
                table: "DMP_Bookings",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Categories_CreatedById",
                table: "DMP_Categories",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Categories_LastModifiedById",
                table: "DMP_Categories",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Categories_Name",
                table: "DMP_Categories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Categories_ShortenUrl",
                table: "DMP_Categories",
                column: "ShortenUrl");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Categories_Status",
                table: "DMP_Categories",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Films_CategoryId",
                table: "DMP_Films",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Films_CreatedById",
                table: "DMP_Films",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Films_LastModifiedById",
                table: "DMP_Films",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_FilmSchedules_CreatedById",
                table: "DMP_FilmSchedules",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_FilmSchedules_FilmId",
                table: "DMP_FilmSchedules",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_FilmSchedules_LastModifiedById",
                table: "DMP_FilmSchedules",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_FilmSchedules_RoomId",
                table: "DMP_FilmSchedules",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Rooms_CreatedById",
                table: "DMP_Rooms",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Rooms_LastModifiedById",
                table: "DMP_Rooms",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Rooms_TheaterId",
                table: "DMP_Rooms",
                column: "TheaterId");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Seats_CreatedById",
                table: "DMP_Seats",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Seats_LastModifiedById",
                table: "DMP_Seats",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Seats_ScheduleId",
                table: "DMP_Seats",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Theaters_CreatedById",
                table: "DMP_Theaters",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Theaters_LastModifiedById",
                table: "DMP_Theaters",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Tickets_CreatedById",
                table: "DMP_Tickets",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Tickets_LastModifiedById",
                table: "DMP_Tickets",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DMP_Tickets_ScheduleId",
                table: "DMP_Tickets",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_AccountLogins_AccountId",
                table: "Identity_AccountLogins",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_AccountLogins_LoginProvider_ProviderKey",
                table: "Identity_AccountLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_Identity_AccountRoles_AccountId_RoleId",
                table: "Identity_AccountRoles",
                columns: new[] { "AccountId", "RoleId" });

            migrationBuilder.CreateIndex(
                name: "IX_Identity_AccountRoles_RoleId",
                table: "Identity_AccountRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Accounts_CreatedById",
                table: "Identity_Accounts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Accounts_Email",
                table: "Identity_Accounts",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Accounts_LastModifiedById",
                table: "Identity_Accounts",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Accounts_NormalizedEmail",
                table: "Identity_Accounts",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Accounts_PhoneNumber",
                table: "Identity_Accounts",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_AccountTokens_AccountId",
                table: "Identity_AccountTokens",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_AccountTokens_LoginProvider_Name_AccountId",
                table: "Identity_AccountTokens",
                columns: new[] { "LoginProvider", "Name", "AccountId" });

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Permissions_CreatedById",
                table: "Identity_Permissions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Permissions_LastModifiedById",
                table: "Identity_Permissions",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Permissions_Name",
                table: "Identity_Permissions",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Permissions_NormalizedName",
                table: "Identity_Permissions",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_RolePermissions_PermissionId_RoleId",
                table: "Identity_RolePermissions",
                columns: new[] { "PermissionId", "RoleId" });

            migrationBuilder.CreateIndex(
                name: "IX_Identity_RolePermissions_RoleId",
                table: "Identity_RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Roles_CreatedById",
                table: "Identity_Roles",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Roles_LastModifiedById",
                table: "Identity_Roles",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Roles_Name",
                table: "Identity_Roles",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Roles_NormalizedName",
                table: "Identity_Roles",
                column: "NormalizedName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DMP_BookingDetails");

            migrationBuilder.DropTable(
                name: "DMP_Seats");

            migrationBuilder.DropTable(
                name: "DMP_Tickets");

            migrationBuilder.DropTable(
                name: "Identity_AccountLogins");

            migrationBuilder.DropTable(
                name: "Identity_AccountRoles");

            migrationBuilder.DropTable(
                name: "Identity_AccountTokens");

            migrationBuilder.DropTable(
                name: "Identity_RolePermissions");

            migrationBuilder.DropTable(
                name: "DMP_Bookings");

            migrationBuilder.DropTable(
                name: "DMP_FilmSchedules");

            migrationBuilder.DropTable(
                name: "Identity_Permissions");

            migrationBuilder.DropTable(
                name: "Identity_Roles");

            migrationBuilder.DropTable(
                name: "DMP_Films");

            migrationBuilder.DropTable(
                name: "DMP_Rooms");

            migrationBuilder.DropTable(
                name: "DMP_Categories");

            migrationBuilder.DropTable(
                name: "DMP_Theaters");

            migrationBuilder.DropTable(
                name: "Identity_Accounts");
        }
    }
}
