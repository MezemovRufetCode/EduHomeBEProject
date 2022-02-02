using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBEProject.Migrations
{
    public partial class TeacherRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherFaculty_Faculty_FacultyId",
                table: "TeacherFaculty");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherFaculty_Teachers_TeacherId",
                table: "TeacherFaculty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherFaculty",
                table: "TeacherFaculty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faculty",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "Hobby",
                table: "Teachers");

            migrationBuilder.RenameTable(
                name: "TeacherFaculty",
                newName: "TeacherFaculties");

            migrationBuilder.RenameTable(
                name: "Faculty",
                newName: "Faculties");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherFaculty_TeacherId",
                table: "TeacherFaculties",
                newName: "IX_TeacherFaculties_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherFaculty_FacultyId",
                table: "TeacherFaculties",
                newName: "IX_TeacherFaculties_FacultyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherFaculties",
                table: "TeacherFaculties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faculties",
                table: "Faculties",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Hobbies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hobbies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherHobbies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(nullable: false),
                    HobbyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherHobbies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherHobbies_Hobbies_HobbyId",
                        column: x => x.HobbyId,
                        principalTable: "Hobbies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherHobbies_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherHobbies_HobbyId",
                table: "TeacherHobbies",
                column: "HobbyId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherHobbies_TeacherId",
                table: "TeacherHobbies",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherFaculties_Faculties_FacultyId",
                table: "TeacherFaculties",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherFaculties_Teachers_TeacherId",
                table: "TeacherFaculties",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherFaculties_Faculties_FacultyId",
                table: "TeacherFaculties");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherFaculties_Teachers_TeacherId",
                table: "TeacherFaculties");

            migrationBuilder.DropTable(
                name: "TeacherHobbies");

            migrationBuilder.DropTable(
                name: "Hobbies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherFaculties",
                table: "TeacherFaculties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faculties",
                table: "Faculties");

            migrationBuilder.RenameTable(
                name: "TeacherFaculties",
                newName: "TeacherFaculty");

            migrationBuilder.RenameTable(
                name: "Faculties",
                newName: "Faculty");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherFaculties_TeacherId",
                table: "TeacherFaculty",
                newName: "IX_TeacherFaculty_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherFaculties_FacultyId",
                table: "TeacherFaculty",
                newName: "IX_TeacherFaculty_FacultyId");

            migrationBuilder.AddColumn<string>(
                name: "Hobby",
                table: "Teachers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherFaculty",
                table: "TeacherFaculty",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faculty",
                table: "Faculty",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherFaculty_Faculty_FacultyId",
                table: "TeacherFaculty",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherFaculty_Teachers_TeacherId",
                table: "TeacherFaculty",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
