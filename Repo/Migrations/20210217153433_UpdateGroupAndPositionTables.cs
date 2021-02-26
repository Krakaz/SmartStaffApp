using Microsoft.EntityFrameworkCore.Migrations;

namespace Repo.Migrations
{
    public partial class UpdateGroupAndPositionTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Staffs_StaffId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Staffs_StaffId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_StaffId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Groups_StaffId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "Groups");

            migrationBuilder.CreateTable(
                name: "GroupStaff",
                columns: table => new
                {
                    GroupsId = table.Column<int>(type: "int", nullable: false),
                    StaffsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupStaff", x => new { x.GroupsId, x.StaffsId });
                    table.ForeignKey(
                        name: "FK_GroupStaff_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupStaff_Staffs_StaffsId",
                        column: x => x.StaffsId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PositionStaff",
                columns: table => new
                {
                    PositionsId = table.Column<int>(type: "int", nullable: false),
                    StaffsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionStaff", x => new { x.PositionsId, x.StaffsId });
                    table.ForeignKey(
                        name: "FK_PositionStaff_Positions_PositionsId",
                        column: x => x.PositionsId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PositionStaff_Staffs_StaffsId",
                        column: x => x.StaffsId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupStaff_StaffsId",
                table: "GroupStaff",
                column: "StaffsId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionStaff_StaffsId",
                table: "PositionStaff",
                column: "StaffsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupStaff");

            migrationBuilder.DropTable(
                name: "PositionStaff");

            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "Positions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "Groups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_StaffId",
                table: "Positions",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_StaffId",
                table: "Groups",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Staffs_StaffId",
                table: "Groups",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Staffs_StaffId",
                table: "Positions",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
