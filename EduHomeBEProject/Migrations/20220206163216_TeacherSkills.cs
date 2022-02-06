using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBEProject.Migrations
{
    public partial class TeacherSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Feature1",
                table: "Teachers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Feature2",
                table: "Teachers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Feature3",
                table: "Teachers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Feature4",
                table: "Teachers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FeatureVal1",
                table: "Teachers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FeatureVal2",
                table: "Teachers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FeatureVal3",
                table: "Teachers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FeatureVal4",
                table: "Teachers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Feature1",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Feature2",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Feature3",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Feature4",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "FeatureVal1",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "FeatureVal2",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "FeatureVal3",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "FeatureVal4",
                table: "Teachers");
        }
    }
}
