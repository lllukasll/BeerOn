using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BeerOn.Repo.Migrations
{
    public partial class AddedBeerRatingsV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BeerRatings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Appearance = table.Column<int>(nullable: false),
                    Average = table.Column<int>(nullable: false),
                    BeerId = table.Column<int>(nullable: false),
                    Flavor = table.Column<int>(nullable: false),
                    Smell = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BeerRatings_Beers_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeerRatings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeerRatings_BeerId",
                table: "BeerRatings",
                column: "BeerId");

            migrationBuilder.CreateIndex(
                name: "IX_BeerRatings_UserId",
                table: "BeerRatings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeerRatings");
        }
    }
}
