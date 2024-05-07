using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTradeAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedGiftAndGiftList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gift_Offers_OfferId",
                table: "Gift");

            migrationBuilder.DropForeignKey(
                name: "FK_Gift_Posts_PostId",
                table: "Gift");

            migrationBuilder.DropForeignKey(
                name: "FK_Gift_Users_UserEmail",
                table: "Gift");

            migrationBuilder.DropIndex(
                name: "IX_Gift_UserEmail",
                table: "Gift");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Gift");

            migrationBuilder.DropColumn(
                name: "ListName",
                table: "Gift");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Gift");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Gift",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Gift",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                table: "Gift",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ConsumerEmail",
                table: "Gift",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GiftListId",
                table: "Gift",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GiftList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    ConsumerEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiftList_Users_ConsumerEmail",
                        column: x => x.ConsumerEmail,
                        principalTable: "Users",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gift_ConsumerEmail",
                table: "Gift",
                column: "ConsumerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Gift_GiftListId",
                table: "Gift",
                column: "GiftListId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftList_ConsumerEmail",
                table: "GiftList",
                column: "ConsumerEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Gift_GiftList_GiftListId",
                table: "Gift",
                column: "GiftListId",
                principalTable: "GiftList",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gift_Offers_OfferId",
                table: "Gift",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gift_Posts_PostId",
                table: "Gift",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gift_Users_ConsumerEmail",
                table: "Gift",
                column: "ConsumerEmail",
                principalTable: "Users",
                principalColumn: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gift_GiftList_GiftListId",
                table: "Gift");

            migrationBuilder.DropForeignKey(
                name: "FK_Gift_Offers_OfferId",
                table: "Gift");

            migrationBuilder.DropForeignKey(
                name: "FK_Gift_Posts_PostId",
                table: "Gift");

            migrationBuilder.DropForeignKey(
                name: "FK_Gift_Users_ConsumerEmail",
                table: "Gift");

            migrationBuilder.DropTable(
                name: "GiftList");

            migrationBuilder.DropIndex(
                name: "IX_Gift_ConsumerEmail",
                table: "Gift");

            migrationBuilder.DropIndex(
                name: "IX_Gift_GiftListId",
                table: "Gift");

            migrationBuilder.DropColumn(
                name: "ConsumerEmail",
                table: "Gift");

            migrationBuilder.DropColumn(
                name: "GiftListId",
                table: "Gift");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Gift",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Gift",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                table: "Gift",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Gift",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "ListName",
                table: "Gift",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Gift",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Gift_UserEmail",
                table: "Gift",
                column: "UserEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Gift_Offers_OfferId",
                table: "Gift",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gift_Posts_PostId",
                table: "Gift",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gift_Users_UserEmail",
                table: "Gift",
                column: "UserEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
