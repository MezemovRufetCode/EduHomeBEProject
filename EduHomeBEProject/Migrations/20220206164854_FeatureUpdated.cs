using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBEProject.Migrations
{
    public partial class FeatureUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Feature5",
                table: "Teachers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Feature6",
                table: "Teachers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FeatureVal5",
                table: "Teachers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FeatureVal6",
                table: "Teachers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Feature5",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Feature6",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "FeatureVal5",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "FeatureVal6",
                table: "Teachers");
        }
    }
}
