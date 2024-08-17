using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpsManagerAPI.Infrastructure.Migrations.Application;

/// <inheritdoc />
public partial class InitialMigrations : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "Auditing");

        migrationBuilder.EnsureSchema(
            name: "Dbo");

        migrationBuilder.EnsureSchema(
            name: "Identity");

        migrationBuilder.CreateTable(
            name: "AuditTrails",
            schema: "Auditing",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                AffectedColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PrimaryKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AuditTrails", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ComplaintCategories",
            schema: "Dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ComplaintCategories", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ComplaintSubCategories",
            schema: "Dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ComplaintSubCategories", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "DisconnectionsReasons",
            schema: "Dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                NumberOfMonth = table.Column<int>(type: "int", nullable: false),
                Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                PercentagePayment = table.Column<int>(type: "int", nullable: false),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DisconnectionsReasons", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "DownloadManager",
            schema: "Dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Roles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DownloadManager", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "OfficeLevels",
            schema: "Dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false),
                LevelId = table.Column<int>(type: "int", nullable: false),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OfficeLevels", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Roles",
            schema: "Identity",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Roles", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Tariffs",
            schema: "Dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UniqueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                TariffCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                MinimumHours = table.Column<int>(type: "int", nullable: false),
                TariffRate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                ServiceBandName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tariffs", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Users",
            schema: "Identity",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsActive = table.Column<bool>(type: "bit", nullable: false),
                RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                ObjectId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                FCMToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                AccessFailedCount = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Offices",
            schema: "Dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                OfficeLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Offices", x => x.Id);
                table.ForeignKey(
                    name: "FK_Offices_OfficeLevels_OfficeLevelId",
                    column: x => x.OfficeLevelId,
                    principalSchema: "Dbo",
                    principalTable: "OfficeLevels",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "RoleClaims",
            schema: "Identity",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RoleClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_RoleClaims_Roles_RoleId",
                    column: x => x.RoleId,
                    principalSchema: "Identity",
                    principalTable: "Roles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UserClaims",
            schema: "Identity",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserClaims_Users_UserId",
                    column: x => x.UserId,
                    principalSchema: "Identity",
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UserLogins",
            schema: "Identity",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserLogins", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserLogins_Users_UserId",
                    column: x => x.UserId,
                    principalSchema: "Identity",
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UserRoles",
            schema: "Identity",
            columns: table => new
            {
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                table.ForeignKey(
                    name: "FK_UserRoles_Roles_RoleId",
                    column: x => x.RoleId,
                    principalSchema: "Identity",
                    principalTable: "Roles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_UserRoles_Users_UserId",
                    column: x => x.UserId,
                    principalSchema: "Identity",
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UserTokens",
            schema: "Identity",
            columns: table => new
            {
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                table.ForeignKey(
                    name: "FK_UserTokens_Users_UserId",
                    column: x => x.UserId,
                    principalSchema: "Identity",
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "DistributionTransformers",
            schema: "Dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                InstallationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                Capacity = table.Column<int>(type: "int", nullable: false),
                Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Rating = table.Column<int>(type: "int", nullable: false),
                Maker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                FeederPillarType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DistributionTransformers", x => x.Id);
                table.ForeignKey(
                    name: "FK_DistributionTransformers_Offices_OfficeId",
                    column: x => x.OfficeId,
                    principalSchema: "Dbo",
                    principalTable: "Offices",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Staffs",
            schema: "Dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UniqueStaffId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                LGA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Staffs", x => x.Id);
                table.ForeignKey(
                    name: "FK_Staffs_Offices_OfficeId",
                    column: x => x.OfficeId,
                    principalSchema: "Dbo",
                    principalTable: "Offices",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Teams",
            schema: "Dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                OfficeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                TeamLeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Teams", x => x.Id);
                table.ForeignKey(
                    name: "FK_Teams_Offices_OfficeId",
                    column: x => x.OfficeId,
                    principalSchema: "Dbo",
                    principalTable: "Offices",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Customers",
            schema: "Dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                MeterNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                TariffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DistributionTransformerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                LGA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                CustomerType = table.Column<int>(type: "int", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                AccountType = table.Column<int>(type: "int", nullable: false),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Customers", x => x.Id);
                table.ForeignKey(
                    name: "FK_Customers_DistributionTransformers_DistributionTransformerId",
                    column: x => x.DistributionTransformerId,
                    principalSchema: "Dbo",
                    principalTable: "DistributionTransformers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Customers_Tariffs_TariffId",
                    column: x => x.TariffId,
                    principalSchema: "Dbo",
                    principalTable: "Tariffs",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "StaffTeams",
            schema: "Dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_StaffTeams", x => x.Id);
                table.ForeignKey(
                    name: "FK_StaffTeams_Staffs_StaffId",
                    column: x => x.StaffId,
                    principalSchema: "Dbo",
                    principalTable: "Staffs",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_StaffTeams_Teams_TeamId",
                    column: x => x.TeamId,
                    principalSchema: "Dbo",
                    principalTable: "Teams",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "BillDistributions",
            schema: "Dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                BillAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                DistributionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDelivered = table.Column<bool>(type: "bit", nullable: false),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BillDistributions", x => x.Id);
                table.ForeignKey(
                    name: "FK_BillDistributions_Customers_CustomerId",
                    column: x => x.CustomerId,
                    principalSchema: "Dbo",
                    principalTable: "Customers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_BillDistributions_Staffs_StaffId",
                    column: x => x.StaffId,
                    principalSchema: "Dbo",
                    principalTable: "Staffs",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Billings",
            schema: "Dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                BillDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                Consumption = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Arrears = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                VAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                CurrentCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                TotalCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                TotalDue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                AccountType = table.Column<int>(type: "int", nullable: false),
                CustomerType = table.Column<int>(type: "int", nullable: false),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Billings", x => x.Id);
                table.ForeignKey(
                    name: "FK_Billings_Customers_CustomerId",
                    column: x => x.CustomerId,
                    principalSchema: "Dbo",
                    principalTable: "Customers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Complaints",
            schema: "Dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                SubCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Status = table.Column<int>(type: "int", nullable: false),
                CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                DistributionTransformerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Complaints", x => x.Id);
                table.ForeignKey(
                    name: "FK_Complaints_ComplaintCategories_CategoryId",
                    column: x => x.CategoryId,
                    principalSchema: "Dbo",
                    principalTable: "ComplaintCategories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Complaints_ComplaintSubCategories_SubCategoryId",
                    column: x => x.SubCategoryId,
                    principalSchema: "Dbo",
                    principalTable: "ComplaintSubCategories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Complaints_Customers_CustomerId",
                    column: x => x.CustomerId,
                    principalSchema: "Dbo",
                    principalTable: "Customers",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Complaints_DistributionTransformers_DistributionTransformerId",
                    column: x => x.DistributionTransformerId,
                    principalSchema: "Dbo",
                    principalTable: "DistributionTransformers",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Complaints_Staffs_StaffId",
                    column: x => x.StaffId,
                    principalSchema: "Dbo",
                    principalTable: "Staffs",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Disconnections",
            schema: "Dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                AmountOwed = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                DateLogged = table.Column<DateTime>(type: "datetime2", nullable: false),
                AmountToPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ReportedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ApprovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Status = table.Column<int>(type: "int", nullable: false),
                DateDisconnected = table.Column<DateTime>(type: "datetime2", nullable: true),
                DateApproved = table.Column<DateTime>(type: "datetime2", nullable: true),
                DisconnectedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Disconnections", x => x.Id);
                table.ForeignKey(
                    name: "FK_Disconnections_Customers_CustomerId",
                    column: x => x.CustomerId,
                    principalSchema: "Dbo",
                    principalTable: "Customers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Disconnections_Staffs_StaffId",
                    column: x => x.StaffId,
                    principalSchema: "Dbo",
                    principalTable: "Staffs",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Enumerations",
            schema: "Dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                MeterNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                DistributionTransformerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ContactFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ContactLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                LGA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                BuildingDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Landmark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                BusinessType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PremiseType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ServiceBand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                CustomerType = table.Column<int>(type: "int", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                AccountType = table.Column<int>(type: "int", nullable: false),
                TariffId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ProposedTariffId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                IsMetered = table.Column<bool>(type: "bit", nullable: false),
                ImagesUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Enumerations", x => x.Id);
                table.ForeignKey(
                    name: "FK_Enumerations_Customers_CustomerId",
                    column: x => x.CustomerId,
                    principalSchema: "Dbo",
                    principalTable: "Customers",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Enumerations_DistributionTransformers_DistributionTransformerId",
                    column: x => x.DistributionTransformerId,
                    principalSchema: "Dbo",
                    principalTable: "DistributionTransformers",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Enumerations_Staffs_StaffId",
                    column: x => x.StaffId,
                    principalSchema: "Dbo",
                    principalTable: "Staffs",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Enumerations_Tariffs_TariffId",
                    column: x => x.TariffId,
                    principalSchema: "Dbo",
                    principalTable: "Tariffs",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Evaluations",
            schema: "Dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                BuildingStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ContactSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ContactFirstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CustomerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                LGA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                FeederId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                DssId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CustomerFirstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CustomerLastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CustomerMiddlename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CustomerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CustomerLatitude = table.Column<double>(type: "float", nullable: false),
                CustomerLongitude = table.Column<double>(type: "float", nullable: false),
                CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CustomerStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                BusinessNature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PremiseUse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                MeterInstalled = table.Column<bool>(type: "bit", nullable: false),
                AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                MeterNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PresentMeterReadingKwh = table.Column<double>(type: "float", nullable: false),
                LastActualReadingKwh = table.Column<double>(type: "float", nullable: false),
                LoadReadingRed = table.Column<double>(type: "float", nullable: false),
                LoadReadingYellow = table.Column<double>(type: "float", nullable: false),
                LoadReadingBlue = table.Column<double>(type: "float", nullable: false),
                Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CertifiedOk = table.Column<bool>(type: "bit", nullable: false),
                OverdraftKwh = table.Column<double>(type: "float", nullable: false),
                OverdraftNaira = table.Column<double>(type: "float", nullable: false),
                TariffDifference = table.Column<double>(type: "float", nullable: false),
                MeterImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                BypassImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                MeterVideo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CorrectionImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CorrectionVideo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                RpaFinalRecommendation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ExistingTariffId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CorrectTariffId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ExistingServiceBand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CorrectServiceBand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Last3MonthsConsumption = table.Column<double>(type: "float", nullable: false),
                PowerFactor = table.Column<double>(type: "float", nullable: false),
                LoadFactor = table.Column<double>(type: "float", nullable: false),
                AvailabilityOfSupply = table.Column<double>(type: "float", nullable: false),
                EnergyBilledMonth = table.Column<double>(type: "float", nullable: false),
                EnergyBilledAvg = table.Column<double>(type: "float", nullable: false),
                LrLyLbAvg = table.Column<double>(type: "float", nullable: false),
                UnBilledEnergy = table.Column<double>(type: "float", nullable: false),
                NumOfLorMonth = table.Column<int>(type: "int", nullable: false),
                LorChargedForMeteringIssues = table.Column<double>(type: "float", nullable: false),
                IsSubsequentOffender = table.Column<bool>(type: "bit", nullable: false),
                AdministrativeCharge = table.Column<double>(type: "float", nullable: false),
                LorChargedForEnergyTheft = table.Column<double>(type: "float", nullable: false),
                PenaltyChargedForEnergyTheft = table.Column<double>(type: "float", nullable: false),
                NumOfAvailabilityDays = table.Column<int>(type: "int", nullable: false),
                Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Landmark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Evaluations", x => x.Id);
                table.ForeignKey(
                    name: "FK_Evaluations_Customers_CustomerId",
                    column: x => x.CustomerId,
                    principalSchema: "Dbo",
                    principalTable: "Customers",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Evaluations_Staffs_StaffId",
                    column: x => x.StaffId,
                    principalSchema: "Dbo",
                    principalTable: "Staffs",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "MeterReadings",
            schema: "Dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                DistributionTransformerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                PreviousReading = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                PresentReading = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Consumption = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                ReadingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsApproved = table.Column<bool>(type: "bit", nullable: false),
                IsMeterRead = table.Column<bool>(type: "bit", nullable: false),
                Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                MeterReadingType = table.Column<int>(type: "int", nullable: false),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MeterReadings", x => x.Id);
                table.ForeignKey(
                    name: "FK_MeterReadings_Customers_CustomerId",
                    column: x => x.CustomerId,
                    principalSchema: "Dbo",
                    principalTable: "Customers",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_MeterReadings_DistributionTransformers_DistributionTransformerId",
                    column: x => x.DistributionTransformerId,
                    principalSchema: "Dbo",
                    principalTable: "DistributionTransformers",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_MeterReadings_Staffs_StaffId",
                    column: x => x.StaffId,
                    principalSchema: "Dbo",
                    principalTable: "Staffs",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Payments",
            schema: "Dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PaymentSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                AccountType = table.Column<int>(type: "int", nullable: false),
                CustomerType = table.Column<int>(type: "int", nullable: false),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Payments", x => x.Id);
                table.ForeignKey(
                    name: "FK_Payments_Customers_CustomerId",
                    column: x => x.CustomerId,
                    principalSchema: "Dbo",
                    principalTable: "Customers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Reconnections",
            schema: "Dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DateLogged = table.Column<DateTime>(type: "datetime2", nullable: false),
                Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ReportedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ApprovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Status = table.Column<int>(type: "int", nullable: false),
                DateReconnected = table.Column<DateTime>(type: "datetime2", nullable: false),
                DateApproved = table.Column<DateTime>(type: "datetime2", nullable: false),
                ReconnectedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Reconnections", x => x.Id);
                table.ForeignKey(
                    name: "FK_Reconnections_Customers_CustomerId",
                    column: x => x.CustomerId,
                    principalSchema: "Dbo",
                    principalTable: "Customers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Reconnections_Staffs_StaffId",
                    column: x => x.StaffId,
                    principalSchema: "Dbo",
                    principalTable: "Staffs",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_BillDistributions_CustomerId",
            schema: "Dbo",
            table: "BillDistributions",
            column: "CustomerId");

        migrationBuilder.CreateIndex(
            name: "IX_BillDistributions_StaffId",
            schema: "Dbo",
            table: "BillDistributions",
            column: "StaffId");

        migrationBuilder.CreateIndex(
            name: "IX_Billings_CustomerId",
            schema: "Dbo",
            table: "Billings",
            column: "CustomerId");

        migrationBuilder.CreateIndex(
            name: "IX_Complaints_CategoryId",
            schema: "Dbo",
            table: "Complaints",
            column: "CategoryId");

        migrationBuilder.CreateIndex(
            name: "IX_Complaints_CustomerId",
            schema: "Dbo",
            table: "Complaints",
            column: "CustomerId");

        migrationBuilder.CreateIndex(
            name: "IX_Complaints_DistributionTransformerId",
            schema: "Dbo",
            table: "Complaints",
            column: "DistributionTransformerId");

        migrationBuilder.CreateIndex(
            name: "IX_Complaints_StaffId",
            schema: "Dbo",
            table: "Complaints",
            column: "StaffId");

        migrationBuilder.CreateIndex(
            name: "IX_Complaints_SubCategoryId",
            schema: "Dbo",
            table: "Complaints",
            column: "SubCategoryId");

        migrationBuilder.CreateIndex(
            name: "IX_Customers_DistributionTransformerId",
            schema: "Dbo",
            table: "Customers",
            column: "DistributionTransformerId");

        migrationBuilder.CreateIndex(
            name: "IX_Customers_TariffId",
            schema: "Dbo",
            table: "Customers",
            column: "TariffId");

        migrationBuilder.CreateIndex(
            name: "IX_Disconnections_CustomerId",
            schema: "Dbo",
            table: "Disconnections",
            column: "CustomerId");

        migrationBuilder.CreateIndex(
            name: "IX_Disconnections_StaffId",
            schema: "Dbo",
            table: "Disconnections",
            column: "StaffId");

        migrationBuilder.CreateIndex(
            name: "IX_DistributionTransformers_OfficeId",
            schema: "Dbo",
            table: "DistributionTransformers",
            column: "OfficeId");

        migrationBuilder.CreateIndex(
            name: "IX_Enumerations_CustomerId",
            schema: "Dbo",
            table: "Enumerations",
            column: "CustomerId");

        migrationBuilder.CreateIndex(
            name: "IX_Enumerations_DistributionTransformerId",
            schema: "Dbo",
            table: "Enumerations",
            column: "DistributionTransformerId");

        migrationBuilder.CreateIndex(
            name: "IX_Enumerations_StaffId",
            schema: "Dbo",
            table: "Enumerations",
            column: "StaffId");

        migrationBuilder.CreateIndex(
            name: "IX_Enumerations_TariffId",
            schema: "Dbo",
            table: "Enumerations",
            column: "TariffId");

        migrationBuilder.CreateIndex(
            name: "IX_Evaluations_CustomerId",
            schema: "Dbo",
            table: "Evaluations",
            column: "CustomerId");

        migrationBuilder.CreateIndex(
            name: "IX_Evaluations_StaffId",
            schema: "Dbo",
            table: "Evaluations",
            column: "StaffId");

        migrationBuilder.CreateIndex(
            name: "IX_MeterReadings_CustomerId",
            schema: "Dbo",
            table: "MeterReadings",
            column: "CustomerId");

        migrationBuilder.CreateIndex(
            name: "IX_MeterReadings_DistributionTransformerId",
            schema: "Dbo",
            table: "MeterReadings",
            column: "DistributionTransformerId");

        migrationBuilder.CreateIndex(
            name: "IX_MeterReadings_StaffId",
            schema: "Dbo",
            table: "MeterReadings",
            column: "StaffId");

        migrationBuilder.CreateIndex(
            name: "IX_Offices_OfficeLevelId",
            schema: "Dbo",
            table: "Offices",
            column: "OfficeLevelId");

        migrationBuilder.CreateIndex(
            name: "IX_Payments_CustomerId",
            schema: "Dbo",
            table: "Payments",
            column: "CustomerId");

        migrationBuilder.CreateIndex(
            name: "IX_Reconnections_CustomerId",
            schema: "Dbo",
            table: "Reconnections",
            column: "CustomerId");

        migrationBuilder.CreateIndex(
            name: "IX_Reconnections_StaffId",
            schema: "Dbo",
            table: "Reconnections",
            column: "StaffId");

        migrationBuilder.CreateIndex(
            name: "IX_RoleClaims_RoleId",
            schema: "Identity",
            table: "RoleClaims",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "RoleNameIndex",
            schema: "Identity",
            table: "Roles",
            columns: new[] { "NormalizedName", "TenantId" },
            unique: true,
            filter: "[NormalizedName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_Staffs_OfficeId",
            schema: "Dbo",
            table: "Staffs",
            column: "OfficeId");

        migrationBuilder.CreateIndex(
            name: "IX_StaffTeams_StaffId",
            schema: "Dbo",
            table: "StaffTeams",
            column: "StaffId");

        migrationBuilder.CreateIndex(
            name: "IX_StaffTeams_TeamId",
            schema: "Dbo",
            table: "StaffTeams",
            column: "TeamId");

        migrationBuilder.CreateIndex(
            name: "IX_Teams_OfficeId",
            schema: "Dbo",
            table: "Teams",
            column: "OfficeId");

        migrationBuilder.CreateIndex(
            name: "IX_UserClaims_UserId",
            schema: "Identity",
            table: "UserClaims",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_UserLogins_LoginProvider_ProviderKey_TenantId",
            schema: "Identity",
            table: "UserLogins",
            columns: new[] { "LoginProvider", "ProviderKey", "TenantId" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_UserLogins_UserId",
            schema: "Identity",
            table: "UserLogins",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_UserRoles_RoleId",
            schema: "Identity",
            table: "UserRoles",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "EmailIndex",
            schema: "Identity",
            table: "Users",
            column: "NormalizedEmail");

        migrationBuilder.CreateIndex(
            name: "UserNameIndex",
            schema: "Identity",
            table: "Users",
            columns: new[] { "NormalizedUserName", "TenantId" },
            unique: true,
            filter: "[NormalizedUserName] IS NOT NULL");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "AuditTrails",
            schema: "Auditing");

        migrationBuilder.DropTable(
            name: "BillDistributions",
            schema: "Dbo");

        migrationBuilder.DropTable(
            name: "Billings",
            schema: "Dbo");

        migrationBuilder.DropTable(
            name: "Complaints",
            schema: "Dbo");

        migrationBuilder.DropTable(
            name: "Disconnections",
            schema: "Dbo");

        migrationBuilder.DropTable(
            name: "DisconnectionsReasons",
            schema: "Dbo");

        migrationBuilder.DropTable(
            name: "DownloadManager",
            schema: "Dbo");

        migrationBuilder.DropTable(
            name: "Enumerations",
            schema: "Dbo");

        migrationBuilder.DropTable(
            name: "Evaluations",
            schema: "Dbo");

        migrationBuilder.DropTable(
            name: "MeterReadings",
            schema: "Dbo");

        migrationBuilder.DropTable(
            name: "Payments",
            schema: "Dbo");

        migrationBuilder.DropTable(
            name: "Reconnections",
            schema: "Dbo");

        migrationBuilder.DropTable(
            name: "RoleClaims",
            schema: "Identity");

        migrationBuilder.DropTable(
            name: "StaffTeams",
            schema: "Dbo");

        migrationBuilder.DropTable(
            name: "UserClaims",
            schema: "Identity");

        migrationBuilder.DropTable(
            name: "UserLogins",
            schema: "Identity");

        migrationBuilder.DropTable(
            name: "UserRoles",
            schema: "Identity");

        migrationBuilder.DropTable(
            name: "UserTokens",
            schema: "Identity");

        migrationBuilder.DropTable(
            name: "ComplaintCategories",
            schema: "Dbo");

        migrationBuilder.DropTable(
            name: "ComplaintSubCategories",
            schema: "Dbo");

        migrationBuilder.DropTable(
            name: "Customers",
            schema: "Dbo");

        migrationBuilder.DropTable(
            name: "Staffs",
            schema: "Dbo");

        migrationBuilder.DropTable(
            name: "Teams",
            schema: "Dbo");

        migrationBuilder.DropTable(
            name: "Roles",
            schema: "Identity");

        migrationBuilder.DropTable(
            name: "Users",
            schema: "Identity");

        migrationBuilder.DropTable(
            name: "DistributionTransformers",
            schema: "Dbo");

        migrationBuilder.DropTable(
            name: "Tariffs",
            schema: "Dbo");

        migrationBuilder.DropTable(
            name: "Offices",
            schema: "Dbo");

        migrationBuilder.DropTable(
            name: "OfficeLevels",
            schema: "Dbo");
    }
}
