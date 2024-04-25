using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTradeAPI.Migrations
{
    /// <inheritdoc />
    public partial class WishListConsumer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConsumerEmail",
                table: "Posts",
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

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ConsumerEmail",
                table: "Posts",
                column: "ConsumerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_AdminEmail",
                table: "Notification",
                column: "AdminEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_SellerEmail",
                table: "Notification",
                column: "SellerEmail");

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
                name: "FK_Posts_User_ConsumerEmail",
                table: "Posts",
                column: "ConsumerEmail",
                principalTable: "User",
                principalColumn: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_User_AdminEmail",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_User_SellerEmail",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_User_ConsumerEmail",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ConsumerEmail",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Notification_AdminEmail",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_SellerEmail",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "ConsumerEmail",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AdminEmail",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "SellerEmail",
                table: "Notification");
        }
    }
}
