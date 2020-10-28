using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class ObligatorioMigrationV14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LodgingId",
                table: "Reserves",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reserves_LodgingId",
                table: "Reserves",
                column: "LodgingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserves_Lodgings_LodgingId",
                table: "Reserves",
                column: "LodgingId",
                principalTable: "Lodgings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserves_Lodgings_LodgingId",
                table: "Reserves");

            migrationBuilder.DropIndex(
                name: "IX_Reserves_LodgingId",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "LodgingId",
                table: "Reserves");
        }
    }
}
