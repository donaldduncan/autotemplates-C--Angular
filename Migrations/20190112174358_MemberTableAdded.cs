using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace trellobot.Migrations
{
    public partial class MemberTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "TrellobotItems");

            migrationBuilder.AddColumn<int>(
                name: "MemberID",
                table: "TrellobotItems",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TrellobotMembers",
                columns: table => new
                {
                    MemberID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Token = table.Column<string>(nullable: true),
                    MemberName = table.Column<string>(nullable: true),
                    TrelloID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrellobotMembers", x => x.MemberID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrellobotItems_MemberID",
                table: "TrellobotItems",
                column: "MemberID");

            migrationBuilder.AddForeignKey(
                name: "FK_TrellobotItems_TrellobotMembers_MemberID",
                table: "TrellobotItems",
                column: "MemberID",
                principalTable: "TrellobotMembers",
                principalColumn: "MemberID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrellobotItems_TrellobotMembers_MemberID",
                table: "TrellobotItems");

            migrationBuilder.DropTable(
                name: "TrellobotMembers");

            migrationBuilder.DropIndex(
                name: "IX_TrellobotItems_MemberID",
                table: "TrellobotItems");

            migrationBuilder.DropColumn(
                name: "MemberID",
                table: "TrellobotItems");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "TrellobotItems",
                nullable: true);
        }
    }
}
