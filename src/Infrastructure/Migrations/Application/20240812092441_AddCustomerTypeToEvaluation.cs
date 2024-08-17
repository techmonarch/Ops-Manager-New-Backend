using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpsManagerAPI.Infrastructure.Migrations.Application;

/// <inheritdoc />
public partial class AddCustomerTypeToEvaluation : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Status",
            schema: "Dbo",
            table: "Evaluations");

        migrationBuilder.RenameColumn(
            name: "UniqueStaffId",
            schema: "Dbo",
            table: "Staffs",
            newName: "StaffNumber");

        migrationBuilder.AlterColumn<int>(
            name: "CustomerType",
            schema: "Dbo",
            table: "Evaluations",
            type: "int",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<int>(
            name: "CustomerStatus",
            schema: "Dbo",
            table: "Evaluations",
            type: "int",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AddColumn<int>(
            name: "AccountType",
            schema: "Dbo",
            table: "Evaluations",
            type: "int",
            nullable: false,
            defaultValue: 0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "AccountType",
            schema: "Dbo",
            table: "Evaluations");

        migrationBuilder.RenameColumn(
            name: "StaffNumber",
            schema: "Dbo",
            table: "Staffs",
            newName: "UniqueStaffId");

        migrationBuilder.AlterColumn<string>(
            name: "CustomerType",
            schema: "Dbo",
            table: "Evaluations",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "int");

        migrationBuilder.AlterColumn<string>(
            name: "CustomerStatus",
            schema: "Dbo",
            table: "Evaluations",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "int");

        migrationBuilder.AddColumn<string>(
            name: "Status",
            schema: "Dbo",
            table: "Evaluations",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: string.Empty);
    }
}
