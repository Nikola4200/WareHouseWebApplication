using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseWeb.Migrations
{
    public partial class ChangedRelationshipBetweenStorageItemAndInputOutputBehaviorCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorageItemInputOutput_StorageItem_StorageItemId",
                table: "StorageItemInputOutput");

            migrationBuilder.AddForeignKey(
                name: "FK_StorageItemInputOutput_StorageItem_StorageItemId",
                table: "StorageItemInputOutput",
                column: "StorageItemId",
                principalTable: "StorageItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorageItemInputOutput_StorageItem_StorageItemId",
                table: "StorageItemInputOutput");

            migrationBuilder.AddForeignKey(
                name: "FK_StorageItemInputOutput_StorageItem_StorageItemId",
                table: "StorageItemInputOutput",
                column: "StorageItemId",
                principalTable: "StorageItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
