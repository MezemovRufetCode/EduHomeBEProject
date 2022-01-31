using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBEProject.Migrations
{
    public partial class tagdeletedfullyfromevent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseFeature_Courses_CourseId",
                table: "CourseFeature");

            migrationBuilder.DropTable(
                name: "EventTags");

            migrationBuilder.DropTable(
                name: "ETags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseFeature",
                table: "CourseFeature");

            migrationBuilder.RenameTable(
                name: "CourseFeature",
                newName: "CourseFeatures");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFeature_CourseId",
                table: "CourseFeatures",
                newName: "IX_CourseFeatures_CourseId");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "CourseFeatures",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseFeatures",
                table: "CourseFeatures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFeatures_Courses_CourseId",
                table: "CourseFeatures",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseFeatures_Courses_CourseId",
                table: "CourseFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseFeatures",
                table: "CourseFeatures");

            migrationBuilder.RenameTable(
                name: "CourseFeatures",
                newName: "CourseFeature");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFeatures_CourseId",
                table: "CourseFeature",
                newName: "IX_CourseFeature_CourseId");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "CourseFeature",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseFeature",
                table: "CourseFeature",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ETags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ETags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ETagId = table.Column<int>(type: "int", nullable: true),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventTags_ETags_ETagId",
                        column: x => x.ETagId,
                        principalTable: "ETags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventTags_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventTags_ETagId",
                table: "EventTags",
                column: "ETagId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTags_EventId",
                table: "EventTags",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTags_TagId",
                table: "EventTags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFeature_Courses_CourseId",
                table: "CourseFeature",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
