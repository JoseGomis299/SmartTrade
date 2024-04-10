using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTrade.Migrations
{
    /// <inheritdoc />
    public partial class imagess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_User_ConsumerEmail",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Addresses_BillingAddressId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_ConsumerEmail",
                table: "Address",
                newName: "IX_Address_ConsumerEmail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_User_ConsumerEmail",
                table: "Address",
                column: "ConsumerEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Address_BillingAddressId",
                table: "User",
                column: "BillingAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_User_ConsumerEmail",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Address_BillingAddressId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameIndex(
                name: "IX_Address_ConsumerEmail",
                table: "Addresses",
                newName: "IX_Addresses_ConsumerEmail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_User_ConsumerEmail",
                table: "Addresses",
                column: "ConsumerEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Addresses_BillingAddressId",
                table: "User",
                column: "BillingAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
