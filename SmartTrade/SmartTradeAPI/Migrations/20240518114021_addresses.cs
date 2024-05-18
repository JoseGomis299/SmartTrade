using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTradeAPI.Migrations
{
    /// <inheritdoc />
    public partial class addresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BillingAddressId",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryAddressId",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_BillingAddressId",
                table: "Purchases",
                column: "BillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_DeliveryAddressId",
                table: "Purchases",
                column: "DeliveryAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Addresses_BillingAddressId",
                table: "Purchases",
                column: "BillingAddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Addresses_DeliveryAddressId",
                table: "Purchases",
                column: "DeliveryAddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Addresses_BillingAddressId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Addresses_DeliveryAddressId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_BillingAddressId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_DeliveryAddressId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "BillingAddressId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "DeliveryAddressId",
                table: "Purchases");
        }
    }
}
