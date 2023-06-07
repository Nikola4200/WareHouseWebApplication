using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseWeb.Migrations
{
    public partial class deleteQuantityAmountInProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityAmount_ClassificationValueId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "QuantityAmount_QuantityAmount",
                table: "Product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "QuantityAmount_ClassificationValueId",
                table: "Product",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "QuantityAmount_QuantityAmount",
                table: "Product",
                type: "float",
                nullable: true);
        }
    }
}
