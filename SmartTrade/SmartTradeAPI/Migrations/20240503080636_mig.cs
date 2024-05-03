using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTradeAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_User_ConsumerEmail",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_User_UserEmail",
                table: "Alerts");

            migrationBuilder.DropForeignKey(
                name: "FK_Bizums_User_ConsumerEmail",
                table: "Bizums");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_User_ConsumerEmail",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_User_ConsumerEmail",
                table: "CreditCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_User_AdminEmail",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_User_SellerEmail",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_User_TargetUserEmail",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_PayPals_User_ConsumerEmail",
                table: "PayPals");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_User_AdminEmail",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_User_SellerEmail",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_User_AdminEmail",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_User_ConsumerEmail",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_User_PurchaseSellerEmail",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Addresses_BillingAddressId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_Wish_User_UserEmail",
                table: "Wish");

            migrationBuilder.DropIndex(
                name: "IX_Notification_AdminEmail",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_SellerEmail",
                table: "Notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_BillingAddressId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "AdminEmail",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "SellerEmail",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "BillingAddressId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Consumer_DNI",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
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
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastNames = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    DNI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BillingAddressId = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastNames = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Gift",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gift", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gift_Consumers_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "Consumers",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gift_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gift_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_BillingAddressId",
                table: "Consumers",
                column: "BillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Gift_OfferId",
                table: "Gift",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Gift_PostId",
                table: "Gift",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Gift_UserEmail",
                table: "Gift",
                column: "UserEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Bizums_Consumers_ConsumerEmail",
                table: "Bizums",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Consumers_ConsumerEmail",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_Consumers_UserEmail",
                table: "Alerts");

            migrationBuilder.DropForeignKey(
                name: "FK_Bizums_Consumers_ConsumerEmail",
                table: "Bizums");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Consumers_ConsumerEmail",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Consumers_ConsumerEmail",
                table: "CreditCards");

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

            migrationBuilder.DropForeignKey(
                name: "FK_Wish_Consumers_UserEmail",
                table: "Wish");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Gift");

            migrationBuilder.DropTable(
                name: "Consumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sellers",
                table: "Sellers");

            migrationBuilder.RenameTable(
                name: "Sellers",
                newName: "User");

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

            migrationBuilder.AddColumn<string>(
                name: "AdminEmail",
                table: "Notification",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerEmail",
                table: "Notification",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IBAN",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DNI",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "BillingAddressId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Consumer_DNI",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "User",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Wish_AdminEmail",
                table: "Wish",
                column: "AdminEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Wish_SellerEmail",
                table: "Wish",
                column: "SellerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_AdminEmail",
                table: "Notification",
                column: "AdminEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_SellerEmail",
                table: "Notification",
                column: "SellerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_User_BillingAddressId",
                table: "User",
                column: "BillingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_User_ConsumerEmail",
                table: "Addresses",
                column: "ConsumerEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_User_UserEmail",
                table: "Alerts",
                column: "UserEmail",
                principalTable: "User",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bizums_User_ConsumerEmail",
                table: "Bizums",
                column: "ConsumerEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_User_ConsumerEmail",
                table: "CartItem",
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
                name: "FK_Notification_User_AdminEmail",
                table: "Notification",
                column: "AdminEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_User_SellerEmail",
                table: "Notification",
                column: "SellerEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_User_TargetUserEmail",
                table: "Notification",
                column: "TargetUserEmail",
                principalTable: "User",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PayPals_User_ConsumerEmail",
                table: "PayPals",
                column: "ConsumerEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_User_AdminEmail",
                table: "Posts",
                column: "AdminEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_User_SellerEmail",
                table: "Posts",
                column: "SellerEmail",
                principalTable: "User",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_User_AdminEmail",
                table: "Products",
                column: "AdminEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_User_ConsumerEmail",
                table: "Purchases",
                column: "ConsumerEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_User_PurchaseSellerEmail",
                table: "Purchases",
                column: "PurchaseSellerEmail",
                principalTable: "User",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Addresses_BillingAddressId",
                table: "User",
                column: "BillingAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Wish_User_UserEmail",
                table: "Wish",
                column: "UserEmail",
                principalTable: "User",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
