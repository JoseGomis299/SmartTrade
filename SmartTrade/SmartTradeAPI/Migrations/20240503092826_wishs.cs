using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTradeAPI.Migrations
{
    /// <inheritdoc />
    public partial class wishs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wish_Products_ProductId",
                table: "Wish");

            migrationBuilder.DropIndex(
                name: "IX_Wish_ProductId",
                table: "Wish");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Wish");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Wish",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wish_ProductId",
                table: "Wish",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wish_Products_ProductId",
                table: "Wish",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
