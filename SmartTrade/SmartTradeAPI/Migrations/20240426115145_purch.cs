using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTradeAPI.Migrations
{
    /// <inheritdoc />
    public partial class purch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "ShippingPrice",
                table: "Purchases",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Purchases",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseOfferId",
                table: "Purchases",
                type: "int",
                nullable: true);

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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CartItem_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CartItem_User_ConsumerEmail",
                        column: x => x.ConsumerEmail,
                        principalTable: "User",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_PurchaseOfferId",
                table: "Purchases",
                column: "PurchaseOfferId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Offers_PurchaseOfferId",
                table: "Purchases",
                column: "PurchaseOfferId",
                principalTable: "Offers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Offers_PurchaseOfferId",
                table: "Purchases");

            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_PurchaseOfferId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "PurchaseOfferId",
                table: "Purchases");

            migrationBuilder.AlterColumn<int>(
                name: "ShippingPrice",
                table: "Purchases",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Purchases",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
