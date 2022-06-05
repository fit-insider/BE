using Microsoft.EntityFrameworkCore.Migrations;

namespace FI.Migrations.Migrations
{
    public partial class AddMealHealthLabels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HealthLabels",
                table: "Meals",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HealthLabels",
                table: "Meals");
        }
    }
}
