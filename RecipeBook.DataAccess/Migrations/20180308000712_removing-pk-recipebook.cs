using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RecipeBook.DataAccess.Migrations
{
    public partial class removingpkrecipebook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_RecipeBook_ID",
                table: "RecipeBook");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "RecipeBook");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "RecipeBook",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_RecipeBook_ID",
                table: "RecipeBook",
                column: "ID");
        }
    }
}
