using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FruitablesProject.Migrations
{
    public partial class SoftDeletedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "UnderSliderInformation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "Sliders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "SliderDescription",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "FruitsAdvertisments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "UnderSliderInformation");

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "SliderDescription");

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "FruitsAdvertisments");

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "Categories");
        }
    }
}
