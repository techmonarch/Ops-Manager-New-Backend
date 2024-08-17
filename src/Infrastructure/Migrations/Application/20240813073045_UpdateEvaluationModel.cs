using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpsManagerAPI.Infrastructure.Migrations.Application;

/// <inheritdoc />
public partial class UpdateEvaluationModel : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "MeterMaker",
            schema: "Dbo",
            table: "Evaluations",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: string.Empty);

        migrationBuilder.AddColumn<string>(
            name: "MeterRating",
            schema: "Dbo",
            table: "Evaluations",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: string.Empty);

        migrationBuilder.AddColumn<string>(
            name: "MeterType",
            schema: "Dbo",
            table: "Evaluations",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: string.Empty);

        migrationBuilder.AddColumn<string>(
            name: "ModeOfPayment",
            schema: "Dbo",
            table: "Evaluations",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: string.Empty);

        migrationBuilder.AlterColumn<string>(
            name: "ContactEmail",
            schema: "Dbo",
            table: "Enumerations",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "MeterMaker",
            schema: "Dbo",
            table: "Evaluations");

        migrationBuilder.DropColumn(
            name: "MeterRating",
            schema: "Dbo",
            table: "Evaluations");

        migrationBuilder.DropColumn(
            name: "MeterType",
            schema: "Dbo",
            table: "Evaluations");

        migrationBuilder.DropColumn(
            name: "ModeOfPayment",
            schema: "Dbo",
            table: "Evaluations");

        migrationBuilder.AlterColumn<string>(
            name: "ContactEmail",
            schema: "Dbo",
            table: "Enumerations",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: string.Empty,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);
    }
}
