using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class ObligatorioMigrationv12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Lodgings_LodgingId",
                table: "Pictures");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Pictures_Path",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_LodgingId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "LodgingId",
                table: "Pictures");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "Pictures",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "LodgingPicture",
                columns: table => new
                {
                    LodgingId = table.Column<Guid>(nullable: false),
                    PictureId = table.Column<Guid>(nullable: false),
                    LodgingId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LodgingPicture", x => new { x.PictureId, x.LodgingId });
                    table.ForeignKey(
                        name: "FK_LodgingPicture_Lodgings_LodgingId",
                        column: x => x.LodgingId,
                        principalTable: "Lodgings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LodgingPicture_Lodgings_LodgingId1",
                        column: x => x.LodgingId1,
                        principalTable: "Lodgings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LodgingPicture_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LodgingPicture_LodgingId",
                table: "LodgingPicture",
                column: "LodgingId");

            migrationBuilder.CreateIndex(
                name: "IX_LodgingPicture_LodgingId1",
                table: "LodgingPicture",
                column: "LodgingId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LodgingPicture");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "Pictures",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LodgingId",
                table: "Pictures",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Pictures_Path",
                table: "Pictures",
                column: "Path");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_LodgingId",
                table: "Pictures",
                column: "LodgingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Lodgings_LodgingId",
                table: "Pictures",
                column: "LodgingId",
                principalTable: "Lodgings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
