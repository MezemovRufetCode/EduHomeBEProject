using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBEProject.Migrations
{
    public partial class EventComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "EComments",
                maxLength: 600,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "EComments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "EComments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EComments_AppUserId",
                table: "EComments",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EComments_AspNetUsers_AppUserId",
                table: "EComments",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EComments_AspNetUsers_AppUserId",
                table: "EComments");

            migrationBuilder.DropIndex(
                name: "IX_EComments_AppUserId",
                table: "EComments");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "EComments");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "EComments");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "EComments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 600);
        }
    }
}
