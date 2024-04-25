using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTradeAPI.Migrations
{
    /// <inheritdoc />
    public partial class NombreDeLosCambios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_User_ConsumerEmail",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ConsumerEmail",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ConsumerEmail",
                table: "Posts");

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseProductId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    ShippingPrice = table.Column<int>(type: "int", nullable: false),
                    PurchasePostId = table.Column<int>(type: "int", nullable: true),
                    PurchaseSellerEmail = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ConsumerEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
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
                        name: "FK_Purchases_User_ConsumerEmail",
                        column: x => x.ConsumerEmail,
                        principalTable: "User",
                        principalColumn: "Email");
                    table.ForeignKey(
                        name: "FK_Purchases_User_PurchaseSellerEmail",
                        column: x => x.PurchaseSellerEmail,
                        principalTable: "User",
                        principalColumn: "Email");
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
                        name: "FK_Wish_User_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "User",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ConsumerEmail",
                table: "Purchases",
                column: "ConsumerEmail");

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
                name: "IX_Wish_PostId",
                table: "Wish",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Wish_UserEmail",
                table: "Wish",
                column: "UserEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Wish");

            migrationBuilder.AddColumn<string>(
                name: "ConsumerEmail",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ConsumerEmail",
                table: "Posts",
                column: "ConsumerEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_User_ConsumerEmail",
                table: "Posts",
                column: "ConsumerEmail",
                principalTable: "User",
                principalColumn: "Email");
        }
    }
}
