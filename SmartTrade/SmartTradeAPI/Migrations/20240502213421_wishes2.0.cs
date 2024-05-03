using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTradeAPI.Migrations
{
    /// <inheritdoc />
    public partial class wishes20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminEmail",
                table: "Wish",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerEmail",
                table: "Wish",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wish_AdminEmail",
                table: "Wish",
                column: "AdminEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Wish_SellerEmail",
                table: "Wish",
                column: "SellerEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Wish_User_AdminEmail",
                table: "Wish",
                column: "AdminEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Wish_User_SellerEmail",
                table: "Wish",
                column: "SellerEmail",
                principalTable: "User",
                principalColumn: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wish_User_AdminEmail",
                table: "Wish");

            migrationBuilder.DropForeignKey(
                name: "FK_Wish_User_SellerEmail",
                table: "Wish");

            migrationBuilder.DropIndex(
                name: "IX_Wish_AdminEmail",
                table: "Wish");

            migrationBuilder.DropIndex(
                name: "IX_Wish_SellerEmail",
                table: "Wish");

            migrationBuilder.DropColumn(
                name: "AdminEmail",
                table: "Wish");

            migrationBuilder.DropColumn(
                name: "SellerEmail",
                table: "Wish");
        }
    }
}
