using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTrade.Migrations
{
    /// <inheritdoc />
    public partial class ns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Age",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
