using Microsoft.EntityFrameworkCore.Migrations;

namespace trellobot.Migrations
{
    public partial class MemberTableRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "fullName",
                table: "TrellobotMembers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "initials",
                table: "TrellobotMembers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fullName",
                table: "TrellobotMembers");

            migrationBuilder.DropColumn(
                name: "initials",
                table: "TrellobotMembers");
        }
    }
}
