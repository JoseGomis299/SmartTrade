using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTradeAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixedGifts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_GiftLists_GiftListId",
                table: "Gifts");

            migrationBuilder.AlterColumn<int>(
                name: "GiftListId",
                table: "Gifts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_GiftLists_GiftListId",
                table: "Gifts",
                column: "GiftListId",
                principalTable: "GiftLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_GiftLists_GiftListId",
                table: "Gifts");

            migrationBuilder.AlterColumn<int>(
                name: "GiftListId",
                table: "Gifts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_GiftLists_GiftListId",
                table: "Gifts",
                column: "GiftListId",
                principalTable: "GiftLists",
                principalColumn: "Id");
        }
    }
}
