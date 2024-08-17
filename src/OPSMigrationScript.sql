IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF SCHEMA_ID(N'Auditing') IS NULL EXEC(N'CREATE SCHEMA [Auditing];');
GO

IF SCHEMA_ID(N'OpsManager') IS NULL EXEC(N'CREATE SCHEMA [OpsManager];');
GO

IF SCHEMA_ID(N'Identity') IS NULL EXEC(N'CREATE SCHEMA [Identity];');
GO

CREATE TABLE [Auditing].[AuditTrails] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [Type] nvarchar(max) NULL,
    [TableName] nvarchar(max) NULL,
    [DateTime] datetime2 NOT NULL,
    [OldValues] nvarchar(max) NULL,
    [NewValues] nvarchar(max) NULL,
    [AffectedColumns] nvarchar(max) NULL,
    [PrimaryKey] nvarchar(max) NULL,
    [TenantId] nvarchar(64) NOT NULL,
    CONSTRAINT [PK_AuditTrails] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [OpsManager].[Brand] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(256) NOT NULL,
    [Description] nvarchar(max) NULL,
    [TenantId] nvarchar(64) NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastModifiedBy] uniqueidentifier NOT NULL,
    [LastModifiedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] uniqueidentifier NULL,
    CONSTRAINT [PK_Brand] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [OpsManager].[OfficeLevels] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [IsActive] bit NOT NULL,
    [LevelId] int NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastModifiedBy] uniqueidentifier NOT NULL,
    [LastModifiedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] uniqueidentifier NULL,
    CONSTRAINT [PK_OfficeLevels] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Identity].[Roles] (
    [Id] nvarchar(450) NOT NULL,
    [Description] nvarchar(max) NULL,
    [TenantId] nvarchar(64) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [OpsManager].[Staffs] (
    [Id] uniqueidentifier NOT NULL,
    [OfficeId] uniqueidentifier NOT NULL,
    [ApplicationUserId] uniqueidentifier NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastModifiedBy] uniqueidentifier NOT NULL,
    [LastModifiedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] uniqueidentifier NULL,
    CONSTRAINT [PK_Staffs] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [OpsManager].[Tariffs] (
    [Id] uniqueidentifier NOT NULL,
    [UniqueId] nvarchar(max) NOT NULL,
    [TariffCode] nvarchar(max) NOT NULL,
    [MinimumHours] int NOT NULL,
    [TariffRate] nvarchar(max) NOT NULL,
    [Amount] decimal(18,2) NOT NULL,
    [ServiceBandName] nvarchar(max) NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastModifiedBy] uniqueidentifier NOT NULL,
    [LastModifiedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] uniqueidentifier NULL,
    CONSTRAINT [PK_Tariffs] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Identity].[Users] (
    [Id] nvarchar(450) NOT NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [Address] nvarchar(max) NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [MiddleName] nvarchar(max) NULL,
    [ImageUrl] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [RefreshToken] nvarchar(max) NULL,
    [RefreshTokenExpiryTime] datetime2 NOT NULL,
    [ObjectId] nvarchar(256) NULL,
    [TenantId] nvarchar(64) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [OpsManager].[Product] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(1024) NOT NULL,
    [Description] nvarchar(max) NULL,
    [Rate] decimal(18,2) NOT NULL,
    [ImagePath] nvarchar(2048) NULL,
    [BrandId] uniqueidentifier NOT NULL,
    [TenantId] nvarchar(64) NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastModifiedBy] uniqueidentifier NOT NULL,
    [LastModifiedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] uniqueidentifier NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Product_Brand_BrandId] FOREIGN KEY ([BrandId]) REFERENCES [OpsManager].[Brand] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [OpsManager].[Offices] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [OfficeLevelId] uniqueidentifier NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastModifiedBy] uniqueidentifier NOT NULL,
    [LastModifiedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] uniqueidentifier NULL,
    CONSTRAINT [PK_Offices] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Offices_OfficeLevels_OfficeLevelId] FOREIGN KEY ([OfficeLevelId]) REFERENCES [OpsManager].[OfficeLevels] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[RoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [CreatedBy] nvarchar(max) NULL,
    [CreatedOn] datetime2 NOT NULL,
    [TenantId] nvarchar(64) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_RoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Identity].[Roles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[UserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    [TenantId] nvarchar(64) NOT NULL,
    CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[UserLogins] (
    [Id] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    [TenantId] nvarchar(64) NOT NULL,
    CONSTRAINT [PK_UserLogins] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[UserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    [TenantId] nvarchar(64) NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Identity].[Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[UserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    [TenantId] nvarchar(64) NOT NULL,
    CONSTRAINT [PK_UserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [OpsManager].[DistributionTransformers] (
    [Id] uniqueidentifier NOT NULL,
    [AssetId] uniqueidentifier NOT NULL,
    [OfficeId] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Longitude] decimal(18,2) NOT NULL,
    [Latitude] decimal(18,2) NOT NULL,
    [InstallationDate] datetime2 NOT NULL,
    [Capacity] int NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [Rating] int NOT NULL,
    [Maker] nvarchar(max) NOT NULL,
    [FeederPillarType] nvarchar(max) NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastModifiedBy] uniqueidentifier NOT NULL,
    [LastModifiedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] uniqueidentifier NULL,
    CONSTRAINT [PK_DistributionTransformers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DistributionTransformers_Offices_OfficeId] FOREIGN KEY ([OfficeId]) REFERENCES [OpsManager].[Offices] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [OpsManager].[Customers] (
    [Id] uniqueidentifier NOT NULL,
    [AccountNumber] nvarchar(max) NOT NULL,
    [MeterNumber] nvarchar(max) NULL,
    [TariffId] uniqueidentifier NOT NULL,
    [DistributionTransformerId] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [MiddleName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [State] nvarchar(max) NOT NULL,
    [LGA] nvarchar(max) NOT NULL,
    [Longitude] decimal(18,2) NOT NULL,
    [Latitude] decimal(18,2) NOT NULL,
    [CustomerType] int NOT NULL,
    [AccountType] int NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastModifiedBy] uniqueidentifier NOT NULL,
    [LastModifiedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] uniqueidentifier NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Customers_DistributionTransformers_DistributionTransformerId] FOREIGN KEY ([DistributionTransformerId]) REFERENCES [OpsManager].[DistributionTransformers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Customers_Tariffs_TariffId] FOREIGN KEY ([TariffId]) REFERENCES [OpsManager].[Tariffs] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [OpsManager].[Enumerations] (
    [Id] uniqueidentifier NOT NULL,
    [AccountNumber] nvarchar(max) NOT NULL,
    [DistributionTransformerId] uniqueidentifier NULL,
    [Title] nvarchar(max) NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [MiddleName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Gender] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [LGA] nvarchar(max) NOT NULL,
    [State] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [PropertyUse] nvarchar(max) NOT NULL,
    [TariffId] uniqueidentifier NULL,
    [StaffId] uniqueidentifier NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastModifiedBy] uniqueidentifier NOT NULL,
    [LastModifiedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] uniqueidentifier NULL,
    CONSTRAINT [PK_Enumerations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Enumerations_DistributionTransformers_DistributionTransformerId] FOREIGN KEY ([DistributionTransformerId]) REFERENCES [OpsManager].[DistributionTransformers] ([Id]),
    CONSTRAINT [FK_Enumerations_Staffs_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [OpsManager].[Staffs] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Enumerations_Tariffs_TariffId] FOREIGN KEY ([TariffId]) REFERENCES [OpsManager].[Tariffs] ([Id])
);
GO

CREATE TABLE [OpsManager].[BillDistributions] (
    [Id] uniqueidentifier NOT NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    [StaffId] uniqueidentifier NOT NULL,
    [BillAmount] decimal(18,2) NOT NULL,
    [Latitude] decimal(18,2) NOT NULL,
    [Longitude] decimal(18,2) NOT NULL,
    [DistributionDate] datetime2 NULL,
    [IsDelivered] bit NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastModifiedBy] uniqueidentifier NOT NULL,
    [LastModifiedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] uniqueidentifier NULL,
    CONSTRAINT [PK_BillDistributions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BillDistributions_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [OpsManager].[Customers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BillDistributions_Staffs_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [OpsManager].[Staffs] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [OpsManager].[Billings] (
    [Id] uniqueidentifier NOT NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    [BillDate] datetime2 NOT NULL,
    [DueDate] datetime2 NOT NULL,
    [Consumption] decimal(18,2) NOT NULL,
    [Arrears] decimal(18,2) NOT NULL,
    [VAT] decimal(18,2) NOT NULL,
    [CurrentCharge] decimal(18,2) NOT NULL,
    [TotalCharge] decimal(18,2) NOT NULL,
    [TotalDue] decimal(18,2) NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastModifiedBy] uniqueidentifier NOT NULL,
    [LastModifiedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] uniqueidentifier NULL,
    CONSTRAINT [PK_Billings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Billings_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [OpsManager].[Customers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [OpsManager].[Complaints] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [SubTitle] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [ImagePath] nvarchar(max) NULL,
    [Address] nvarchar(max) NOT NULL,
    [Status] int NOT NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    [AssigneeId] uniqueidentifier NULL,
    [DistributionTransformerId] uniqueidentifier NULL,
    [ComplaintType] int NOT NULL,
    [StaffId] uniqueidentifier NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastModifiedBy] uniqueidentifier NOT NULL,
    [LastModifiedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] uniqueidentifier NULL,
    CONSTRAINT [PK_Complaints] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Complaints_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [OpsManager].[Customers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Complaints_DistributionTransformers_DistributionTransformerId] FOREIGN KEY ([DistributionTransformerId]) REFERENCES [OpsManager].[DistributionTransformers] ([Id]),
    CONSTRAINT [FK_Complaints_Staffs_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [OpsManager].[Staffs] ([Id])
);
GO

CREATE TABLE [OpsManager].[Disconnections] (
    [Id] uniqueidentifier NOT NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    [StaffId] uniqueidentifier NOT NULL,
    [AmountOwed] decimal(18,2) NOT NULL,
    [DateLogged] datetime2 NOT NULL,
    [AmountToPay] decimal(18,2) NOT NULL,
    [Reason] nvarchar(max) NOT NULL,
    [ReportedBy] uniqueidentifier NULL,
    [ApprovedBy] uniqueidentifier NULL,
    [Status] int NOT NULL,
    [DateDisconnected] datetime2 NULL,
    [DateApproved] datetime2 NULL,
    [DisconnectedBy] uniqueidentifier NULL,
    [ImagePath] nvarchar(max) NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastModifiedBy] uniqueidentifier NOT NULL,
    [LastModifiedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] uniqueidentifier NULL,
    CONSTRAINT [PK_Disconnections] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Disconnections_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [OpsManager].[Customers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Disconnections_Staffs_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [OpsManager].[Staffs] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [OpsManager].[Evaluations] (
    [Id] uniqueidentifier NOT NULL,
    [AccountNumber] nvarchar(max) NOT NULL,
    [MeterNumber] nvarchar(max) NOT NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [MiddleName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [State] nvarchar(max) NOT NULL,
    [LGA] nvarchar(max) NOT NULL,
    [CustomerType] int NOT NULL,
    [EstimatedLor] decimal(18,2) NOT NULL,
    [Penalty] decimal(18,2) NOT NULL,
    [AdminCharge] decimal(18,2) NOT NULL,
    [StaffId] uniqueidentifier NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastModifiedBy] uniqueidentifier NOT NULL,
    [LastModifiedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] uniqueidentifier NULL,
    CONSTRAINT [PK_Evaluations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Evaluations_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [OpsManager].[Customers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Evaluations_Staffs_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [OpsManager].[Staffs] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [OpsManager].[MeterReadings] (
    [Id] uniqueidentifier NOT NULL,
    [StaffId] uniqueidentifier NOT NULL,
    [CustomerId] uniqueidentifier NULL,
    [DistributionTransformerId] uniqueidentifier NULL,
    [PreviousReading] decimal(18,2) NOT NULL,
    [PresentReading] decimal(18,2) NOT NULL,
    [Consumption] decimal(18,2) NOT NULL,
    [ReadingDate] datetime2 NOT NULL,
    [ImagePath] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [IsApproved] bit NOT NULL,
    [MeterReadingType] int NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastModifiedBy] uniqueidentifier NOT NULL,
    [LastModifiedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] uniqueidentifier NULL,
    CONSTRAINT [PK_MeterReadings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MeterReadings_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [OpsManager].[Customers] ([Id]),
    CONSTRAINT [FK_MeterReadings_DistributionTransformers_DistributionTransformerId] FOREIGN KEY ([DistributionTransformerId]) REFERENCES [OpsManager].[DistributionTransformers] ([Id]),
    CONSTRAINT [FK_MeterReadings_Staffs_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [OpsManager].[Staffs] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [OpsManager].[Reconnections] (
    [Id] uniqueidentifier NOT NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    [StaffId] uniqueidentifier NOT NULL,
    [DateLogged] datetime2 NOT NULL,
    [Reason] nvarchar(max) NOT NULL,
    [ReportedBy] uniqueidentifier NOT NULL,
    [ApprovedBy] uniqueidentifier NULL,
    [Status] int NOT NULL,
    [DateReconnected] datetime2 NOT NULL,
    [DateApproved] datetime2 NOT NULL,
    [ReconnectedBy] uniqueidentifier NULL,
    [ImagePath] nvarchar(max) NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastModifiedBy] uniqueidentifier NOT NULL,
    [LastModifiedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] uniqueidentifier NULL,
    CONSTRAINT [PK_Reconnections] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Reconnections_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [OpsManager].[Customers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reconnections_Staffs_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [OpsManager].[Staffs] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_BillDistributions_CustomerId] ON [OpsManager].[BillDistributions] ([CustomerId]);
GO

CREATE INDEX [IX_BillDistributions_StaffId] ON [OpsManager].[BillDistributions] ([StaffId]);
GO

CREATE INDEX [IX_Billings_CustomerId] ON [OpsManager].[Billings] ([CustomerId]);
GO

CREATE INDEX [IX_Complaints_CustomerId] ON [OpsManager].[Complaints] ([CustomerId]);
GO

CREATE INDEX [IX_Complaints_DistributionTransformerId] ON [OpsManager].[Complaints] ([DistributionTransformerId]);
GO

CREATE INDEX [IX_Complaints_StaffId] ON [OpsManager].[Complaints] ([StaffId]);
GO

CREATE INDEX [IX_Customers_DistributionTransformerId] ON [OpsManager].[Customers] ([DistributionTransformerId]);
GO

CREATE INDEX [IX_Customers_TariffId] ON [OpsManager].[Customers] ([TariffId]);
GO

CREATE INDEX [IX_Disconnections_CustomerId] ON [OpsManager].[Disconnections] ([CustomerId]);
GO

CREATE INDEX [IX_Disconnections_StaffId] ON [OpsManager].[Disconnections] ([StaffId]);
GO

CREATE INDEX [IX_DistributionTransformers_OfficeId] ON [OpsManager].[DistributionTransformers] ([OfficeId]);
GO

CREATE INDEX [IX_Enumerations_DistributionTransformerId] ON [OpsManager].[Enumerations] ([DistributionTransformerId]);
GO

CREATE INDEX [IX_Enumerations_StaffId] ON [OpsManager].[Enumerations] ([StaffId]);
GO

CREATE INDEX [IX_Enumerations_TariffId] ON [OpsManager].[Enumerations] ([TariffId]);
GO

CREATE INDEX [IX_Evaluations_CustomerId] ON [OpsManager].[Evaluations] ([CustomerId]);
GO

CREATE INDEX [IX_Evaluations_StaffId] ON [OpsManager].[Evaluations] ([StaffId]);
GO

CREATE INDEX [IX_MeterReadings_CustomerId] ON [OpsManager].[MeterReadings] ([CustomerId]);
GO

CREATE INDEX [IX_MeterReadings_DistributionTransformerId] ON [OpsManager].[MeterReadings] ([DistributionTransformerId]);
GO

CREATE INDEX [IX_MeterReadings_StaffId] ON [OpsManager].[MeterReadings] ([StaffId]);
GO

CREATE INDEX [IX_Offices_OfficeLevelId] ON [OpsManager].[Offices] ([OfficeLevelId]);
GO

CREATE INDEX [IX_Product_BrandId] ON [OpsManager].[Product] ([BrandId]);
GO

CREATE INDEX [IX_Reconnections_CustomerId] ON [OpsManager].[Reconnections] ([CustomerId]);
GO

CREATE INDEX [IX_Reconnections_StaffId] ON [OpsManager].[Reconnections] ([StaffId]);
GO

CREATE INDEX [IX_RoleClaims_RoleId] ON [Identity].[RoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [Identity].[Roles] ([NormalizedName], [TenantId]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_UserClaims_UserId] ON [Identity].[UserClaims] ([UserId]);
GO

CREATE UNIQUE INDEX [IX_UserLogins_LoginProvider_ProviderKey_TenantId] ON [Identity].[UserLogins] ([LoginProvider], [ProviderKey], [TenantId]);
GO

CREATE INDEX [IX_UserLogins_UserId] ON [Identity].[UserLogins] ([UserId]);
GO

CREATE INDEX [IX_UserRoles_RoleId] ON [Identity].[UserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [Identity].[Users] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [Identity].[Users] ([NormalizedUserName], [TenantId]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240529214310_InitialMigration', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [OpsManager].[Product];
GO

DROP TABLE [OpsManager].[Brand];
GO

ALTER TABLE [OpsManager].[Tariffs] ADD [TenantId] nvarchar(64) NOT NULL DEFAULT N'';
GO

ALTER TABLE [OpsManager].[Staffs] ADD [TenantId] nvarchar(64) NOT NULL DEFAULT N'';
GO

ALTER TABLE [OpsManager].[Reconnections] ADD [TenantId] nvarchar(64) NOT NULL DEFAULT N'';
GO

ALTER TABLE [OpsManager].[Offices] ADD [TenantId] nvarchar(64) NOT NULL DEFAULT N'';
GO

ALTER TABLE [OpsManager].[OfficeLevels] ADD [TenantId] nvarchar(64) NOT NULL DEFAULT N'';
GO

ALTER TABLE [OpsManager].[MeterReadings] ADD [TenantId] nvarchar(64) NOT NULL DEFAULT N'';
GO

ALTER TABLE [OpsManager].[Evaluations] ADD [TenantId] nvarchar(64) NOT NULL DEFAULT N'';
GO

ALTER TABLE [OpsManager].[Enumerations] ADD [TenantId] nvarchar(64) NOT NULL DEFAULT N'';
GO

ALTER TABLE [OpsManager].[DistributionTransformers] ADD [TenantId] nvarchar(64) NOT NULL DEFAULT N'';
GO

ALTER TABLE [OpsManager].[Disconnections] ADD [TenantId] nvarchar(64) NOT NULL DEFAULT N'';
GO

ALTER TABLE [OpsManager].[Customers] ADD [TenantId] nvarchar(64) NOT NULL DEFAULT N'';
GO

ALTER TABLE [OpsManager].[Complaints] ADD [TenantId] nvarchar(64) NOT NULL DEFAULT N'';
GO

ALTER TABLE [OpsManager].[Billings] ADD [TenantId] nvarchar(64) NOT NULL DEFAULT N'';
GO

ALTER TABLE [OpsManager].[BillDistributions] ADD [TenantId] nvarchar(64) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240601055948_AddStatusToCustomer', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [OpsManager].[Customers] ADD [Status] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240601074114_AddStatusToCustomerModel', N'7.0.4');
GO

COMMIT;
GO

