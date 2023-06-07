using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseWeb.Migrations
{
    public partial class addedRelationshipProductClassificationValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ClassificationValueId",
                table: "Product",
                nullable: false,
                defaultValue: 0L);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ClassificationValue_ClassificationValueId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ClassificationValueId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ClassificationValueId",
                table: "Product");
        }
    }
}
