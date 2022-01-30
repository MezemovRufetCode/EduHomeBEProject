using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBEProject.Migrations
{
    public partial class ECategoryDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_ECategories_ECategoryId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "ECategories");

            migrationBuilder.DropIndex(
                name: "IX_Events_ECategoryId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ECategoryId",
                table: "Events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ECategoryId",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ECategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_ECategoryId",
                table: "Events",
                column: "ECategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_ECategories_ECategoryId",
                table: "Events",
                column: "ECategoryId",
                principalTable: "ECategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
