using Microsoft.EntityFrameworkCore.Migrations;

namespace Repo.Migrations
{
    public partial class AddTargetToDirection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasRO",
                table: "Positions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTarget",
                table: "Positions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasRO",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "IsTarget",
                table: "Positions");
        }
    }
}
