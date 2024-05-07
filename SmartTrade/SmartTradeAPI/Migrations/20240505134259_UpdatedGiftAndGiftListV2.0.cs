using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTradeAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedGiftAndGiftListV20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gift_GiftList_GiftListId",
                table: "Gift");

            migrationBuilder.DropForeignKey(
                name: "FK_Gift_Users_ConsumerEmail",
                table: "Gift");

            migrationBuilder.DropForeignKey(
                name: "FK_GiftList_Users_ConsumerEmail",
                table: "GiftList");

            migrationBuilder.DropIndex(
                name: "IX_Gift_ConsumerEmail",
                table: "Gift");

            migrationBuilder.DropColumn(
                name: "ConsumerEmail",
                table: "Gift");

            migrationBuilder.AlterColumn<string>(
                name: "ConsumerEmail",
                table: "GiftList",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GiftListId",
                table: "Gift",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Gift_GiftList_GiftListId",
                table: "Gift",
                column: "GiftListId",
                principalTable: "GiftList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GiftList_Users_ConsumerEmail",
                table: "GiftList",
                column: "ConsumerEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gift_GiftList_GiftListId",
                table: "Gift");

            migrationBuilder.DropForeignKey(
                name: "FK_GiftList_Users_ConsumerEmail",
                table: "GiftList");

            migrationBuilder.AlterColumn<string>(
                name: "ConsumerEmail",
                table: "GiftList",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "GiftListId",
                table: "Gift",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ConsumerEmail",
                table: "Gift",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gift_ConsumerEmail",
                table: "Gift",
                column: "ConsumerEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Gift_GiftList_GiftListId",
                table: "Gift",
                column: "GiftListId",
                principalTable: "GiftList",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gift_Users_ConsumerEmail",
                table: "Gift",
                column: "ConsumerEmail",
                principalTable: "Users",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_GiftList_Users_ConsumerEmail",
                table: "GiftList",
                column: "ConsumerEmail",
                principalTable: "Users",
                principalColumn: "Email");
        }
    }
}
