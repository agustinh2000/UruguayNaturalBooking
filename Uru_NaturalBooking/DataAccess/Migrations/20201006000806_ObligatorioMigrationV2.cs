using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class ObligatorioMigrationV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Lodgings");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Lodgings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Lodgings");

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Lodgings",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
