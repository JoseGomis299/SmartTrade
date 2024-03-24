using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTradeLib.Migrations
{
    /// <inheritdoc />
    public partial class consumer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_User_CostumerEmail",
                table: "Adresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_User_CostumerEmail",
                table: "Alerts");

            migrationBuilder.DropForeignKey(
                name: "FK_Bizums_User_CostumerEmail",
                table: "Bizums");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_User_CostumerEmail",
                table: "CreditCards");

            migrationBuilder.DropForeignKey(
                name: "FK_PayPals_User_CostumerEmail",
                table: "PayPals");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Products_ProductId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Validated",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Costumer_DNI",
                table: "User",
                newName: "Consumer_DNI");

            migrationBuilder.RenameColumn(
                name: "CostumerEmail",
                table: "PayPals",
                newName: "ConsumerEmail");

            migrationBuilder.RenameIndex(
                name: "IX_PayPals_CostumerEmail",
                table: "PayPals",
                newName: "IX_PayPals_ConsumerEmail");

            migrationBuilder.RenameColumn(
                name: "CostumerEmail",
                table: "CreditCards",
                newName: "ConsumerEmail");

            migrationBuilder.RenameIndex(
                name: "IX_CreditCards_CostumerEmail",
                table: "CreditCards",
                newName: "IX_CreditCards_ConsumerEmail");

            migrationBuilder.RenameColumn(
                name: "CostumerEmail",
                table: "Bizums",
                newName: "ConsumerEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Bizums_CostumerEmail",
                table: "Bizums",
                newName: "IX_Bizums_ConsumerEmail");

            migrationBuilder.RenameColumn(
                name: "CostumerEmail",
                table: "Alerts",
                newName: "ConsumerEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Alerts_CostumerEmail",
                table: "Alerts",
                newName: "IX_Alerts_ConsumerEmail");

            migrationBuilder.RenameColumn(
                name: "CostumerEmail",
                table: "Adresses",
                newName: "ConsumerEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Adresses_CostumerEmail",
                table: "Adresses",
                newName: "IX_Adresses_ConsumerEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_User_ConsumerEmail",
                table: "Adresses",
                column: "ConsumerEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_User_ConsumerEmail",
                table: "Alerts",
                column: "ConsumerEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Bizums_User_ConsumerEmail",
                table: "Bizums",
                column: "ConsumerEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_User_ConsumerEmail",
                table: "CreditCards",
                column: "ConsumerEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_PayPals_User_ConsumerEmail",
                table: "PayPals",
                column: "ConsumerEmail",
                principalTable: "User",
                principalColumn: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_User_ConsumerEmail",
                table: "Adresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_User_ConsumerEmail",
                table: "Alerts");

            migrationBuilder.DropForeignKey(
                name: "FK_Bizums_User_ConsumerEmail",
                table: "Bizums");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_User_ConsumerEmail",
                table: "CreditCards");

            migrationBuilder.DropForeignKey(
                name: "FK_PayPals_User_ConsumerEmail",
                table: "PayPals");

            migrationBuilder.RenameColumn(
                name: "Consumer_DNI",
                table: "User",
                newName: "Costumer_DNI");

            migrationBuilder.RenameColumn(
                name: "ConsumerEmail",
                table: "PayPals",
                newName: "CostumerEmail");

            migrationBuilder.RenameIndex(
                name: "IX_PayPals_ConsumerEmail",
                table: "PayPals",
                newName: "IX_PayPals_CostumerEmail");

            migrationBuilder.RenameColumn(
                name: "ConsumerEmail",
                table: "CreditCards",
                newName: "CostumerEmail");

            migrationBuilder.RenameIndex(
                name: "IX_CreditCards_ConsumerEmail",
                table: "CreditCards",
                newName: "IX_CreditCards_CostumerEmail");

            migrationBuilder.RenameColumn(
                name: "ConsumerEmail",
                table: "Bizums",
                newName: "CostumerEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Bizums_ConsumerEmail",
                table: "Bizums",
                newName: "IX_Bizums_CostumerEmail");

            migrationBuilder.RenameColumn(
                name: "ConsumerEmail",
                table: "Alerts",
                newName: "CostumerEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Alerts_ConsumerEmail",
                table: "Alerts",
                newName: "IX_Alerts_CostumerEmail");

            migrationBuilder.RenameColumn(
                name: "ConsumerEmail",
                table: "Adresses",
                newName: "CostumerEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Adresses_ConsumerEmail",
                table: "Adresses",
                newName: "IX_Adresses_CostumerEmail");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Validated",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductId",
                table: "Products",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_User_CostumerEmail",
                table: "Adresses",
                column: "CostumerEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_User_CostumerEmail",
                table: "Alerts",
                column: "CostumerEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Bizums_User_CostumerEmail",
                table: "Bizums",
                column: "CostumerEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_User_CostumerEmail",
                table: "CreditCards",
                column: "CostumerEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_PayPals_User_CostumerEmail",
                table: "PayPals",
                column: "CostumerEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Products_ProductId",
                table: "Products",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
