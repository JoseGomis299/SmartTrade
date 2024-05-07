using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTradeAPI.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
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
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Door = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsumerEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastNames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Consumer_DNI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BillingAddressId = table.Column<int>(type: "int", nullable: true),
                    DNI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IBAN = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Email);
                    table.ForeignKey(
                        name: "FK_Users_Addresses_BillingAddressId",
                        column: x => x.BillingAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alerts_Users_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bizums",
                columns: table => new
                {
                    TelephonNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConsumerEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bizums", x => x.TelephonNumber);
                    table.ForeignKey(
                        name: "FK_Bizums_Users_ConsumerEmail",
                        column: x => x.ConsumerEmail,
                        principalTable: "Users",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    CardNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CVV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardHolder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsumerEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.CardNumber);
                    table.ForeignKey(
                        name: "FK_CreditCards_Users_ConsumerEmail",
                        column: x => x.ConsumerEmail,
                        principalTable: "Users",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "GiftLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    ConsumerEmail = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiftLists_Users_ConsumerEmail",
                        column: x => x.ConsumerEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayPals",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsumerEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayPals", x => x.Email);
                    table.ForeignKey(
                        name: "FK_PayPals_Users_ConsumerEmail",
                        column: x => x.ConsumerEmail,
                        principalTable: "Users",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Certification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EcologicPrint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HowToReducePrint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinimumAge = table.Column<int>(type: "int", nullable: false),
                    HowToUse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminEmail = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
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
                    Toy_Material = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Users_AdminEmail",
                        column: x => x.AdminEmail,
                        principalTable: "Users",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageSource = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Thumbnail = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        name: "FK_Posts_Users_AdminEmail",
                        column: x => x.AdminEmail,
                        principalTable: "Users",
                        principalColumn: "Email");
                    table.ForeignKey(
                        name: "FK_Posts_Users_SellerEmail",
                        column: x => x.SellerEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Visited = table.Column<bool>(type: "bit", nullable: false),
                    TargetUserEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TargetPostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Posts_TargetPostId",
                        column: x => x.TargetPostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notification_Users_TargetUserEmail",
                        column: x => x.TargetUserEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Offers_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wish",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wish", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wish_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wish_Users_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ConsumerEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItem_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItem_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CartItem_Users_ConsumerEmail",
                        column: x => x.ConsumerEmail,
                        principalTable: "Users",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "Gifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Notified = table.Column<bool>(type: "bit", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    OfferId = table.Column<int>(type: "int", nullable: true),
                    GiftListId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gifts_GiftLists_GiftListId",
                        column: x => x.GiftListId,
                        principalTable: "GiftLists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Gifts_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Gifts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseProductId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    ShippingPrice = table.Column<float>(type: "real", nullable: false),
                    PurchasePostId = table.Column<int>(type: "int", nullable: true),
                    PurchaseSellerEmail = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PurchaseOfferId = table.Column<int>(type: "int", nullable: true),
                    ConsumerEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchases_Offers_PurchaseOfferId",
                        column: x => x.PurchaseOfferId,
                        principalTable: "Offers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Purchases_Posts_PurchasePostId",
                        column: x => x.PurchasePostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Purchases_Products_PurchaseProductId",
                        column: x => x.PurchaseProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Purchases_Users_ConsumerEmail",
                        column: x => x.ConsumerEmail,
                        principalTable: "Users",
                        principalColumn: "Email");
                    table.ForeignKey(
                        name: "FK_Purchases_Users_PurchaseSellerEmail",
                        column: x => x.PurchaseSellerEmail,
                        principalTable: "Users",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ConsumerEmail",
                table: "Addresses",
                column: "ConsumerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_UserEmail",
                table: "Alerts",
                column: "UserEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Bizums_ConsumerEmail",
                table: "Bizums",
                column: "ConsumerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ConsumerEmail",
                table: "CartItem",
                column: "ConsumerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_OfferId",
                table: "CartItem",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_PostId",
                table: "CartItem",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_ConsumerEmail",
                table: "CreditCards",
                column: "ConsumerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_GiftLists_ConsumerEmail",
                table: "GiftLists",
                column: "ConsumerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_GiftListId",
                table: "Gifts",
                column: "GiftListId");

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_OfferId",
                table: "Gifts",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_PostId",
                table: "Gifts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_ProductId",
                table: "Image",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_TargetPostId",
                table: "Notification",
                column: "TargetPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_TargetUserEmail",
                table: "Notification",
                column: "TargetUserEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_PostId",
                table: "Offers",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_ProductId",
                table: "Offers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PayPals_ConsumerEmail",
                table: "PayPals",
                column: "ConsumerEmail");

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
                name: "IX_Purchases_ConsumerEmail",
                table: "Purchases",
                column: "ConsumerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_PurchaseOfferId",
                table: "Purchases",
                column: "PurchaseOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_PurchasePostId",
                table: "Purchases",
                column: "PurchasePostId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_PurchaseProductId",
                table: "Purchases",
                column: "PurchaseProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_PurchaseSellerEmail",
                table: "Purchases",
                column: "PurchaseSellerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BillingAddressId",
                table: "Users",
                column: "BillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Wish_PostId",
                table: "Wish",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Wish_UserEmail",
                table: "Wish",
                column: "UserEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Users_ConsumerEmail",
                table: "Addresses",
                column: "ConsumerEmail",
                principalTable: "Users",
                principalColumn: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Users_ConsumerEmail",
                table: "Addresses");

            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "Bizums");

            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "Gifts");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "PayPals");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Wish");

            migrationBuilder.DropTable(
                name: "GiftLists");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
