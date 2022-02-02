using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBEProject.Migrations
{
    public partial class TeacherSocailsChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseFeatures");

            migrationBuilder.AddColumn<string>(
                name: "FacebookAccount",
                table: "Teachers",
                maxLength: 200,
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

            migrationBuilder.CreateTable(
                name: "CourseFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassDuration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    SkillLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    studentCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseFeatures_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseFeatures_CourseId",
                table: "CourseFeatures",
                column: "CourseId");
        }
    }
}
