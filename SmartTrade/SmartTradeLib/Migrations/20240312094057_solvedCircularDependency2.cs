using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTradeLib.Migrations
{
    /// <inheritdoc />
    public partial class solvedCircularDependency2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_User_CostumerEmail",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Addresses_BillingAddressId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Adresses");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_CostumerEmail",
                table: "Adresses",
                newName: "IX_Adresses_CostumerEmail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adresses",
                table: "Adresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_User_CostumerEmail",
                table: "Adresses",
                column: "CostumerEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Adresses_BillingAddressId",
                table: "User",
                column: "BillingAddressId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_User_CostumerEmail",
                table: "Adresses");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Adresses_BillingAddressId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adresses",
                table: "Adresses");

            migrationBuilder.RenameTable(
                name: "Adresses",
                newName: "Addresses");

            migrationBuilder.RenameIndex(
                name: "IX_Adresses_CostumerEmail",
                table: "Addresses",
                newName: "IX_Addresses_CostumerEmail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_User_CostumerEmail",
                table: "Addresses",
                column: "CostumerEmail",
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
