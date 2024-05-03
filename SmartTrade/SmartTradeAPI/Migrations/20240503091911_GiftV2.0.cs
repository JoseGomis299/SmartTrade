using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTradeAPI.Migrations
{
    /// <inheritdoc />
    public partial class GiftV20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Gift",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Gift",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Gift");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Gift");
        }
    }
}
