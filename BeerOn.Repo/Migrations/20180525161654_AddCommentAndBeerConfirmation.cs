using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BeerOn.Repo.Migrations
{
    public partial class AddCommentAndBeerConfirmation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeerRatings");

            migrationBuilder.AddColumn<bool>(
                name: "Confirmation",
                table: "Beers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTime",
                table: "Beers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Beers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Beers_UserId",
                table: "Beers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_Users_UserId",
                table: "Beers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beers_Users_UserId",
                table: "Beers");

            migrationBuilder.DropIndex(
                name: "IX_Beers_UserId",
                table: "Beers");

            migrationBuilder.DropColumn(
                name: "Confirmation",
                table: "Beers");

            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                table: "Beers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Beers");

            migrationBuilder.CreateTable(
                name: "BeerRatings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Appearance = table.Column<int>(nullable: false),
                    Flavor = table.Column<int>(nullable: false),
                    Smell = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerRatings", x => x.Id);
                });
        }
    }
}
