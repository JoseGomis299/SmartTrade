using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTrade.Migrations
{
    /// <inheritdoc />
    public partial class notificationss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                        name: "FK_Notification_User_TargetUserEmail",
                        column: x => x.TargetUserEmail,
                        principalTable: "User",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_TargetPostId",
                table: "Notification",
                column: "TargetPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_TargetUserEmail",
                table: "Notification",
                column: "TargetUserEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification");
        }
    }
}
