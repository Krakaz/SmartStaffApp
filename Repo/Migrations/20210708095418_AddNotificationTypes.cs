using Microsoft.EntityFrameworkCore.Migrations;

namespace Repo.Migrations
{
    public partial class AddNotificationTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationEmailNotificationType",
                columns: table => new
                {
                    NotificationEmailsId = table.Column<int>(type: "int", nullable: false),
                    NotificationTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationEmailNotificationType", x => new { x.NotificationEmailsId, x.NotificationTypesId });
                    table.ForeignKey(
                        name: "FK_NotificationEmailNotificationType_NotificationEmails_NotificationEmailsId",
                        column: x => x.NotificationEmailsId,
                        principalTable: "NotificationEmails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationEmailNotificationType_NotificationTypes_NotificationTypesId",
                        column: x => x.NotificationTypesId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationEmailNotificationType_NotificationTypesId",
                table: "NotificationEmailNotificationType",
                column: "NotificationTypesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationEmailNotificationType");

            migrationBuilder.DropTable(
                name: "NotificationTypes");
        }
    }
}
