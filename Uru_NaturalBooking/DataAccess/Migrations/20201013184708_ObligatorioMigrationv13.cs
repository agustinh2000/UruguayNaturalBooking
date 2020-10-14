using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class ObligatorioMigrationv13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LodgingPicture_Lodgings_LodgingId",
                table: "LodgingPicture");

            migrationBuilder.DropForeignKey(
                name: "FK_LodgingPicture_Lodgings_LodgingId1",
                table: "LodgingPicture");

            migrationBuilder.DropForeignKey(
                name: "FK_LodgingPicture_Pictures_PictureId",
                table: "LodgingPicture");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LodgingPicture",
                table: "LodgingPicture");

            migrationBuilder.RenameTable(
                name: "LodgingPicture",
                newName: "LodgingPictures");

            migrationBuilder.RenameIndex(
                name: "IX_LodgingPicture_LodgingId1",
                table: "LodgingPictures",
                newName: "IX_LodgingPictures_LodgingId1");

            migrationBuilder.RenameIndex(
                name: "IX_LodgingPicture_LodgingId",
                table: "LodgingPictures",
                newName: "IX_LodgingPictures_LodgingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LodgingPictures",
                table: "LodgingPictures",
                columns: new[] { "PictureId", "LodgingId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LodgingPictures_Lodgings_LodgingId",
                table: "LodgingPictures",
                column: "LodgingId",
                principalTable: "Lodgings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LodgingPictures_Lodgings_LodgingId1",
                table: "LodgingPictures",
                column: "LodgingId1",
                principalTable: "Lodgings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LodgingPictures_Pictures_PictureId",
                table: "LodgingPictures",
                column: "PictureId",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LodgingPictures_Lodgings_LodgingId",
                table: "LodgingPictures");

            migrationBuilder.DropForeignKey(
                name: "FK_LodgingPictures_Lodgings_LodgingId1",
                table: "LodgingPictures");

            migrationBuilder.DropForeignKey(
                name: "FK_LodgingPictures_Pictures_PictureId",
                table: "LodgingPictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LodgingPictures",
                table: "LodgingPictures");

            migrationBuilder.RenameTable(
                name: "LodgingPictures",
                newName: "LodgingPicture");

            migrationBuilder.RenameIndex(
                name: "IX_LodgingPictures_LodgingId1",
                table: "LodgingPicture",
                newName: "IX_LodgingPicture_LodgingId1");

            migrationBuilder.RenameIndex(
                name: "IX_LodgingPictures_LodgingId",
                table: "LodgingPicture",
                newName: "IX_LodgingPicture_LodgingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LodgingPicture",
                table: "LodgingPicture",
                columns: new[] { "PictureId", "LodgingId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LodgingPicture_Lodgings_LodgingId",
                table: "LodgingPicture",
                column: "LodgingId",
                principalTable: "Lodgings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LodgingPicture_Lodgings_LodgingId1",
                table: "LodgingPicture",
                column: "LodgingId1",
                principalTable: "Lodgings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LodgingPicture_Pictures_PictureId",
                table: "LodgingPicture",
                column: "PictureId",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
