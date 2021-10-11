using Microsoft.EntityFrameworkCore.Migrations;

namespace Repo.Migrations
{
    public partial class RenameColumnNotificationLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MyProperty",
                table: "NotificationLogs",
                newName: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "NotificationLogs",
                newName: "MyProperty");
        }
    }
}
