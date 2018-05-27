using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BeerOn.Repo.Migrations
{
    public partial class AddedAverageRatingForBeerV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AverageRating",
                table: "Beers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Beers");
        }
    }
}
