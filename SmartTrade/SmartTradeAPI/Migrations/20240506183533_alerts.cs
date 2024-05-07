using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTradeAPI.Migrations
{
    /// <inheritdoc />
    public partial class alerts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_Products_ProductId",
                table: "Alerts");

            migrationBuilder.DropIndex(
                name: "IX_Alerts_ProductId",
                table: "Alerts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Alerts");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Alerts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Alerts");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Alerts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_ProductId",
                table: "Alerts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_Products_ProductId",
                table: "Alerts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
