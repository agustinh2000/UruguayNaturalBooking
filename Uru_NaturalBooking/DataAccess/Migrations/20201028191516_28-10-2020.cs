using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class _28102020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ReviewsAverageScore",
                table: "Lodgings",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    NameOfWhoComments = table.Column<string>(nullable: true),
                    LastNameOfWhoComments = table.Column<string>(nullable: true),
                    LodgingOfReviewId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Lodgings_LodgingOfReviewId",
                        column: x => x.LodgingOfReviewId,
                        principalTable: "Lodgings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Review_LodgingOfReviewId",
                table: "Review",
                column: "LodgingOfReviewId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropColumn(
                name: "ReviewsAverageScore",
                table: "Lodgings");
        }
    }
}
