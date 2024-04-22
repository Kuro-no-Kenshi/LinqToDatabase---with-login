using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinqToDatabase.Migrations
{
    /// <inheritdoc />
    public partial class charLvl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Characters_CharacterId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Items_ItemId",
                table: "Inventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory");

            migrationBuilder.RenameTable(
                name: "Inventory",
                newName: "Inventories");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_ItemId",
                table: "Inventories",
                newName: "IX_Inventories_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_CharacterId",
                table: "Inventories",
                newName: "IX_Inventories_CharacterId");

            migrationBuilder.AddColumn<int>(
                name: "CharacterLevel",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Characters_CharacterId",
                table: "Inventories",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "CharacterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Items_ItemId",
                table: "Inventories",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Characters_CharacterId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Items_ItemId",
                table: "Inventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "CharacterLevel",
                table: "Characters");

            migrationBuilder.RenameTable(
                name: "Inventories",
                newName: "Inventory");

            migrationBuilder.RenameIndex(
                name: "IX_Inventories_ItemId",
                table: "Inventory",
                newName: "IX_Inventory_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventories_CharacterId",
                table: "Inventory",
                newName: "IX_Inventory_CharacterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Characters_CharacterId",
                table: "Inventory",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "CharacterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Items_ItemId",
                table: "Inventory",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
