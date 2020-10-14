using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class aMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserves_Lodgings_LodgingOfReserveId",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "TouristSpots");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "Lodgings");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "TouristSpots",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    LodgingId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Picture_Lodgings_LodgingId",
                        column: x => x.LodgingId,
                        principalTable: "Lodgings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TouristSpots_ImageId",
                table: "TouristSpots",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_LodgingId",
                table: "Picture",
                column: "LodgingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserves_Lodgings_LodgingOfReserveId",
                table: "Reserves",
                column: "LodgingOfReserveId",
                principalTable: "Lodgings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TouristSpots_Picture_ImageId",
                table: "TouristSpots",
                column: "ImageId",
                principalTable: "Picture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserves_Lodgings_LodgingOfReserveId",
                table: "Reserves");

            migrationBuilder.DropForeignKey(
                name: "FK_TouristSpots_Picture_ImageId",
                table: "TouristSpots");

            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.DropIndex(
                name: "IX_TouristSpots_ImageId",
                table: "TouristSpots");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "TouristSpots");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "TouristSpots",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Images",
                table: "Lodgings",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserves_Lodgings_LodgingOfReserveId",
                table: "Reserves",
                column: "LodgingOfReserveId",
                principalTable: "Lodgings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
