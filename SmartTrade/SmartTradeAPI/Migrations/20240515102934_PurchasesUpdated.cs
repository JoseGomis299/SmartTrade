using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTradeAPI.Migrations
{
    /// <inheritdoc />
    public partial class PurchasesUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Offers_PurchaseOfferId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Posts_PurchasePostId",
                table: "Purchases");

            migrationBuilder.RenameColumn(
                name: "PurchasePostId",
                table: "Purchases",
                newName: "PostId");

            migrationBuilder.RenameColumn(
                name: "PurchaseOfferId",
                table: "Purchases",
                newName: "OfferId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_PurchasePostId",
                table: "Purchases",
                newName: "IX_Purchases_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_PurchaseOfferId",
                table: "Purchases",
                newName: "IX_Purchases_OfferId");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpectedDate",
                table: "Purchases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Purchases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Offers_OfferId",
                table: "Purchases",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Posts_PostId",
                table: "Purchases",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Offers_OfferId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Posts_PostId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "ExpectedDate",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Purchases");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Purchases",
                newName: "PurchasePostId");

            migrationBuilder.RenameColumn(
                name: "OfferId",
                table: "Purchases",
                newName: "PurchaseOfferId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_PostId",
                table: "Purchases",
                newName: "IX_Purchases_PurchasePostId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_OfferId",
                table: "Purchases",
                newName: "IX_Purchases_PurchaseOfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Offers_PurchaseOfferId",
                table: "Purchases",
                column: "PurchaseOfferId",
                principalTable: "Offers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Posts_PurchasePostId",
                table: "Purchases",
                column: "PurchasePostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
