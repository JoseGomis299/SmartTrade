using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTradeLib.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Door = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostumerEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastNames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Costumer_DNI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BillingAddressId = table.Column<int>(type: "int", nullable: true),
                    DNI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IBAN = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Email);
                    table.ForeignKey(
                        name: "FK_User_Adresses_BillingAddressId",
                        column: x => x.BillingAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bizums",
                columns: table => new
                {
                    TelephonNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CostumerEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bizums", x => x.TelephonNumber);
                    table.ForeignKey(
                        name: "FK_Bizums_User_CostumerEmail",
                        column: x => x.CostumerEmail,
                        principalTable: "User",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    CardNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpirationDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CVV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardHolder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostumerEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.CardNumber);
                    table.ForeignKey(
                        name: "FK_CreditCards_User_CostumerEmail",
                        column: x => x.CostumerEmail,
                        principalTable: "User",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "PayPals",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostumerEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayPals", x => x.Email);
                    table.ForeignKey(
                        name: "FK_PayPals_User_CostumerEmail",
                        column: x => x.CostumerEmail,
                        principalTable: "User",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Certification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EcologicPrint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinimumAge = table.Column<int>(type: "int", nullable: false),
                    Validated = table.Column<bool>(type: "bit", nullable: false),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminEmail = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Proteins = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Carbohydrates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fats = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Allergens = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Toy_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Toy_Material = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_User_AdminEmail",
                        column: x => x.AdminEmail,
                        principalTable: "User",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CostumerEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alerts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alerts_User_CostumerEmail",
                        column: x => x.CostumerEmail,
                        principalTable: "User",
                        principalColumn: "Email");
                    table.ForeignKey(
                        name: "FK_Alerts_User_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "User",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Validated = table.Column<bool>(type: "bit", nullable: false),
                    SellerEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdminEmail = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Posts_User_AdminEmail",
                        column: x => x.AdminEmail,
                        principalTable: "User",
                        principalColumn: "Email");
                    table.ForeignKey(
                        name: "FK_Posts_User_SellerEmail",
                        column: x => x.SellerEmail,
                        principalTable: "User",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    ShippingCost = table.Column<float>(type: "real", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_CostumerEmail",
                table: "Addresses",
                column: "CostumerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_CostumerEmail",
                table: "Alerts",
                column: "CostumerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_ProductId",
                table: "Alerts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_UserEmail",
                table: "Alerts",
                column: "UserEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Bizums_CostumerEmail",
                table: "Bizums",
                column: "CostumerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_CostumerEmail",
                table: "CreditCards",
                column: "CostumerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_PostId",
                table: "Offers",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PayPals_CostumerEmail",
                table: "PayPals",
                column: "CostumerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AdminEmail",
                table: "Posts",
                column: "AdminEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ProductId",
                table: "Posts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_SellerEmail",
                table: "Posts",
                column: "SellerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Products_AdminEmail",
                table: "Products",
                column: "AdminEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductId",
                table: "Products",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_User_BillingAddressId",
                table: "User",
                column: "BillingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_User_CostumerEmail",
                table: "Addresses",
                column: "CostumerEmail",
                principalTable: "User",
                principalColumn: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_User_CostumerEmail",
                table: "Addresses");

            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "Bizums");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "PayPals");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
