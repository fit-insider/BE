using Microsoft.EntityFrameworkCore.Migrations;

namespace FI.Migrations.Migrations
{
    public partial class MealCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Meals_MealId",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Nutrient_Meals_MealId",
                table: "Nutrient");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Meals_MealId",
                table: "Ingredient",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nutrient_Meals_MealId",
                table: "Nutrient",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Meals_MealId",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Nutrient_Meals_MealId",
                table: "Nutrient");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Meals_MealId",
                table: "Ingredient",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Nutrient_Meals_MealId",
                table: "Nutrient",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
