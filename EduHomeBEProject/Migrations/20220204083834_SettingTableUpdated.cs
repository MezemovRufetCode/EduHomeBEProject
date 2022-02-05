using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBEProject.Migrations
{
    public partial class SettingTableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FAdress",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "WelcomeImage",
                table: "Settings");

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Settings",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Settings");

            migrationBuilder.AddColumn<string>(
                name: "FAdress",
                table: "Settings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WelcomeImage",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
