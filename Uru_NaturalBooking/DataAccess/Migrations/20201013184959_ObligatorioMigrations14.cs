using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ObligatorioMigrations14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LodgingPictures_Lodgings_LodgingId1",
                table: "LodgingPictures");

            migrationBuilder.DropIndex(
                name: "IX_LodgingPictures_LodgingId1",
                table: "LodgingPictures");

            migrationBuilder.DropColumn(
                name: "LodgingId1",
                table: "LodgingPictures");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LodgingId1",
                table: "LodgingPictures",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LodgingPictures_LodgingId1",
                table: "LodgingPictures",
                column: "LodgingId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LodgingPictures_Lodgings_LodgingId1",
                table: "LodgingPictures",
                column: "LodgingId1",
                principalTable: "Lodgings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
