using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBEProject.Migrations
{
    public partial class BlogCommentsFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bComment_AspNetUsers_AppUserId",
                table: "bComment");

            migrationBuilder.DropForeignKey(
                name: "FK_bComment_Blogs_BlogId",
                table: "bComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bComment",
                table: "bComment");

            migrationBuilder.RenameTable(
                name: "bComment",
                newName: "bComments");

            migrationBuilder.RenameIndex(
                name: "IX_bComment_BlogId",
                table: "bComments",
                newName: "IX_bComments_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_bComment_AppUserId",
                table: "bComments",
                newName: "IX_bComments_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bComments",
                table: "bComments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_bComments_AspNetUsers_AppUserId",
                table: "bComments",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_bComments_Blogs_BlogId",
                table: "bComments",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bComments_AspNetUsers_AppUserId",
                table: "bComments");

            migrationBuilder.DropForeignKey(
                name: "FK_bComments_Blogs_BlogId",
                table: "bComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bComments",
                table: "bComments");

            migrationBuilder.RenameTable(
                name: "bComments",
                newName: "bComment");

            migrationBuilder.RenameIndex(
                name: "IX_bComments_BlogId",
                table: "bComment",
                newName: "IX_bComment_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_bComments_AppUserId",
                table: "bComment",
                newName: "IX_bComment_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bComment",
                table: "bComment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_bComment_AspNetUsers_AppUserId",
                table: "bComment",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_bComment_Blogs_BlogId",
                table: "bComment",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
