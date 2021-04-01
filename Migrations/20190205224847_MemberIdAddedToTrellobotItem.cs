using Microsoft.EntityFrameworkCore.Migrations;

namespace trellobot.Migrations
{
    public partial class MemberIdAddedToTrellobotItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MemberID",
                table: "TrellobotItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberID",
                table: "TrellobotItems");
        }
    }
}
