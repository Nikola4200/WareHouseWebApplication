using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseWeb.Migrations
{
    public partial class ChangedPropertiesOfStorage_TotalQuantityRemovedAddedCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalQuantity",
                table: "Storage");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Storage",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Storage");

            migrationBuilder.AddColumn<double>(
                name: "TotalQuantity",
                table: "Storage",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
