using Microsoft.EntityFrameworkCore.Migrations;

namespace trellobot.Migrations
{
    public partial class AppKeyRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppKey",
                table: "TrellobotItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppKey",
                table: "TrellobotItems",
                nullable: true);
        }
    }
}
