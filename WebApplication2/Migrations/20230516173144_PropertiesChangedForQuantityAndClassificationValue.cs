using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseWeb.Migrations
{
    public partial class PropertiesChangedForQuantityAndClassificationValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitOfMeasurement",
                table: "StorageItem");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasurement",
                table: "OrderItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasurement",
                table: "StorageItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasurement",
                table: "OrderItem",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
