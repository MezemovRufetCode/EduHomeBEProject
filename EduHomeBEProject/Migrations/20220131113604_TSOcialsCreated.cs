using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBEProject.Migrations
{
    public partial class TSOcialsCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TSocials_Teachers_TeacherId",
                table: "TSocials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TSocials",
                table: "TSocials");

            migrationBuilder.RenameTable(
                name: "TSocials",
                newName: "Socials");

            migrationBuilder.RenameIndex(
                name: "IX_TSocials_TeacherId",
                table: "Socials",
                newName: "IX_Socials_TeacherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Socials",
                table: "Socials",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Socials_Teachers_TeacherId",
                table: "Socials",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Socials_Teachers_TeacherId",
                table: "Socials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Socials",
                table: "Socials");

            migrationBuilder.RenameTable(
                name: "Socials",
                newName: "TSocials");

            migrationBuilder.RenameIndex(
                name: "IX_Socials_TeacherId",
                table: "TSocials",
                newName: "IX_TSocials_TeacherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TSocials",
                table: "TSocials",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TSocials_Teachers_TeacherId",
                table: "TSocials",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
