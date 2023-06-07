using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseWeb.Migrations
{
    public partial class ChangedInputOutputRelationshipWithStorageItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorageItem_StorageItemInputOutput_StorageItemInputOutputId",
                table: "StorageItem");

            migrationBuilder.DropIndex(
                name: "IX_StorageItem_StorageItemInputOutputId",
                table: "StorageItem");

            migrationBuilder.DropColumn(
                name: "StorageItemInputOutputId",
                table: "StorageItem");

            migrationBuilder.AddColumn<long>(
                name: "StorageItemId",
                table: "StorageItemInputOutput",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_StorageItemInputOutput_StorageItemId",
                table: "StorageItemInputOutput",
                column: "StorageItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_StorageItemInputOutput_StorageItem_StorageItemId",
                table: "StorageItemInputOutput",
                column: "StorageItemId",
                principalTable: "StorageItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorageItemInputOutput_StorageItem_StorageItemId",
                table: "StorageItemInputOutput");

            migrationBuilder.DropIndex(
                name: "IX_StorageItemInputOutput_StorageItemId",
                table: "StorageItemInputOutput");

            migrationBuilder.DropColumn(
                name: "StorageItemId",
                table: "StorageItemInputOutput");

            migrationBuilder.AddColumn<long>(
                name: "StorageItemInputOutputId",
                table: "StorageItem",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_StorageItem_StorageItemInputOutputId",
                table: "StorageItem",
                column: "StorageItemInputOutputId");

            migrationBuilder.AddForeignKey(
                name: "FK_StorageItem_StorageItemInputOutput_StorageItemInputOutputId",
                table: "StorageItem",
                column: "StorageItemInputOutputId",
                principalTable: "StorageItemInputOutput",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
