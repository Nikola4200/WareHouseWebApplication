using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseWeb.Migrations
{
    public partial class ChangedPropertyUnitOfMeasurementOfQuantityAndRemovedClassValueFromProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ClassificationValue_ClassificationValueId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ClassificationValueId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "QuantityAmount_ClassificationValueId",
                table: "StorageItemInputOutput");

            migrationBuilder.DropColumn(
                name: "QuantityAmount_ClassificationValueId",
                table: "StorageItem");

            migrationBuilder.DropColumn(
                name: "ClassificationValueId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "QuantityAmount_ClassificationValueId",
                table: "OrderItem");

            migrationBuilder.AddColumn<long>(
                name: "QuantityAmount_UnitOfMeasurementId",
                table: "StorageItemInputOutput",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "QuantityAmount_UnitOfMeasurementId",
                table: "StorageItem",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "QuantityAmount_UnitOfMeasurementId",
                table: "OrderItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityAmount_UnitOfMeasurementId",
                table: "StorageItemInputOutput");

            migrationBuilder.DropColumn(
                name: "QuantityAmount_UnitOfMeasurementId",
                table: "StorageItem");

            migrationBuilder.DropColumn(
                name: "QuantityAmount_UnitOfMeasurementId",
                table: "OrderItem");

            migrationBuilder.AddColumn<long>(
                name: "QuantityAmount_ClassificationValueId",
                table: "StorageItemInputOutput",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "QuantityAmount_ClassificationValueId",
                table: "StorageItem",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ClassificationValueId",
                table: "Product",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "QuantityAmount_ClassificationValueId",
                table: "OrderItem",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ClassificationValueId",
                table: "Product",
                column: "ClassificationValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ClassificationValue_ClassificationValueId",
                table: "Product",
                column: "ClassificationValueId",
                principalTable: "ClassificationValue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
