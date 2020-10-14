using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class MiMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lodgings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    QuantityOfStars = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Images = table.Column<byte[]>(nullable: true),
                    PricePerNight = table.Column<double>(nullable: false),
                    TotalPrice = table.Column<double>(nullable: false),
                    TouristSpotId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lodgings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lodgings_TouristSpots_TouristSpotId",
                        column: x => x.TouristSpotId,
                        principalTable: "TouristSpots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reserves",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumberOfContact = table.Column<int>(nullable: false),
                    DescriptionForGuest = table.Column<string>(nullable: true),
                    LodgingOfReserveId = table.Column<Guid>(nullable: true),
                    CheckIn = table.Column<DateTime>(nullable: false),
                    CheckOut = table.Column<DateTime>(nullable: false),
                    QuantityOfAdult = table.Column<int>(nullable: false),
                    QuantityOfChild = table.Column<int>(nullable: false),
                    QuantityOfBaby = table.Column<int>(nullable: false),
                    StateOfReserve = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reserves_Lodgings_LodgingOfReserveId",
                        column: x => x.LodgingOfReserveId,
                        principalTable: "Lodgings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lodgings_TouristSpotId",
                table: "Lodgings",
                column: "TouristSpotId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserves_LodgingOfReserveId",
                table: "Reserves",
                column: "LodgingOfReserveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reserves");

            migrationBuilder.DropTable(
                name: "Lodgings");
        }
    }
}
