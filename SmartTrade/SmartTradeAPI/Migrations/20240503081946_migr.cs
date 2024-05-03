using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTradeAPI.Migrations
{
    /// <inheritdoc />
    public partial class migr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bizums_Consumers_ConsumerEmail",
                table: "Bizums");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Consumers_ConsumerEmail",
                table: "CreditCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Gift_Consumers_UserEmail",
                table: "Gift");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Consumers_TargetUserEmail",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_PayPals_Consumers_ConsumerEmail",
                table: "PayPals");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Admins_AdminEmail",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Sellers_SellerEmail",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Admins_AdminEmail",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Consumers_ConsumerEmail",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Sellers_PurchaseSellerEmail",
                table: "Purchases");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Consumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sellers",
                table: "Sellers");

            migrationBuilder.RenameTable(
                name: "Sellers",
                newName: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "IBAN",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DNI",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "BillingAddressId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Consumer_DNI",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BillingAddressId",
                table: "Users",
                column: "BillingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Users_ConsumerEmail",
                table: "Addresses",
                column: "ConsumerEmail",
                principalTable: "Users",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_Users_UserEmail",
                table: "Alerts",
                column: "UserEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bizums_Users_ConsumerEmail",
                table: "Bizums",
                column: "ConsumerEmail",
                principalTable: "Users",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Users_ConsumerEmail",
                table: "CartItem",
                column: "ConsumerEmail",
                principalTable: "Users",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_Users_ConsumerEmail",
                table: "CreditCards",
                column: "ConsumerEmail",
                principalTable: "Users",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Gift_Users_UserEmail",
                table: "Gift",
                column: "UserEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Users_TargetUserEmail",
                table: "Notification",
                column: "TargetUserEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PayPals_Users_ConsumerEmail",
                table: "PayPals",
                column: "ConsumerEmail",
                principalTable: "Users",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_AdminEmail",
                table: "Posts",
                column: "AdminEmail",
                principalTable: "Users",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_SellerEmail",
                table: "Posts",
                column: "SellerEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_AdminEmail",
                table: "Products",
                column: "AdminEmail",
                principalTable: "Users",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Users_ConsumerEmail",
                table: "Purchases",
                column: "ConsumerEmail",
                principalTable: "Users",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Users_PurchaseSellerEmail",
                table: "Purchases",
                column: "PurchaseSellerEmail",
                principalTable: "Users",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Addresses_BillingAddressId",
                table: "Users",
                column: "BillingAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wish_Users_UserEmail",
                table: "Wish",
                column: "UserEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Users_ConsumerEmail",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_Users_UserEmail",
                table: "Alerts");

            migrationBuilder.DropForeignKey(
                name: "FK_Bizums_Users_ConsumerEmail",
                table: "Bizums");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Users_ConsumerEmail",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Users_ConsumerEmail",
                table: "CreditCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Gift_Users_UserEmail",
                table: "Gift");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Users_TargetUserEmail",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_PayPals_Users_ConsumerEmail",
                table: "PayPals");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_AdminEmail",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_SellerEmail",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_AdminEmail",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Users_ConsumerEmail",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Users_PurchaseSellerEmail",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Addresses_BillingAddressId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Wish_Users_UserEmail",
                table: "Wish");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_BillingAddressId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BillingAddressId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Consumer_DNI",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Sellers");

            migrationBuilder.AlterColumn<string>(
                name: "IBAN",
                table: "Sellers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DNI",
                table: "Sellers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "Sellers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sellers",
                table: "Sellers",
                column: "Email");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastNames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Consumers",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BillingAddressId = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DNI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastNames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumers", x => x.Email);
                    table.ForeignKey(
                        name: "FK_Consumers_Addresses_BillingAddressId",
                        column: x => x.BillingAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_BillingAddressId",
                table: "Consumers",
                column: "BillingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Consumers_ConsumerEmail",
                table: "Addresses",
                column: "ConsumerEmail",
                principalTable: "Consumers",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_Consumers_UserEmail",
                table: "Alerts",
                column: "UserEmail",
                principalTable: "Consumers",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bizums_Consumers_ConsumerEmail",
                table: "Bizums",
                column: "ConsumerEmail",
                principalTable: "Consumers",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Consumers_ConsumerEmail",
                table: "CartItem",
                column: "ConsumerEmail",
                principalTable: "Consumers",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_Consumers_ConsumerEmail",
                table: "CreditCards",
                column: "ConsumerEmail",
                principalTable: "Consumers",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Gift_Consumers_UserEmail",
                table: "Gift",
                column: "UserEmail",
                principalTable: "Consumers",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Consumers_TargetUserEmail",
                table: "Notification",
                column: "TargetUserEmail",
                principalTable: "Consumers",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PayPals_Consumers_ConsumerEmail",
                table: "PayPals",
                column: "ConsumerEmail",
                principalTable: "Consumers",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Admins_AdminEmail",
                table: "Posts",
                column: "AdminEmail",
                principalTable: "Admins",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Sellers_SellerEmail",
                table: "Posts",
                column: "SellerEmail",
                principalTable: "Sellers",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Admins_AdminEmail",
                table: "Products",
                column: "AdminEmail",
                principalTable: "Admins",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Consumers_ConsumerEmail",
                table: "Purchases",
                column: "ConsumerEmail",
                principalTable: "Consumers",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Sellers_PurchaseSellerEmail",
                table: "Purchases",
                column: "PurchaseSellerEmail",
                principalTable: "Sellers",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Wish_Consumers_UserEmail",
                table: "Wish",
                column: "UserEmail",
                principalTable: "Consumers",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
