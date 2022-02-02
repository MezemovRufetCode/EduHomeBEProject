using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBEProject.Migrations
{
    public partial class SocialsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacebookAccount",
                table: "Teachers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pinterest",
                table: "Teachers",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterAccount",
                table: "Teachers",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VimeoAccount",
                table: "Teachers",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookAccount",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Pinterest",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "TwitterAccount",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "VimeoAccount",
                table: "Teachers");
        }
    }
}
