using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBEProject.Migrations
{
    public partial class teacherOtherPArameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Degree",
                table: "Teachers",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "Teachers",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Hobby",
                table: "Teachers",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Degree",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Hobby",
                table: "Teachers");

            migrationBuilder.AddColumn<string>(
                name: "FacebookAccount",
                table: "Teachers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pinterest",
                table: "Teachers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterAccount",
                table: "Teachers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VimeoAccount",
                table: "Teachers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
