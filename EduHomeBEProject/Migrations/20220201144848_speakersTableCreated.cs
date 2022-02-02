using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBEProject.Migrations
{
    public partial class speakersTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSpeakers_Events_EventId",
                table: "EventSpeakers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "EventSpeakers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "EventSpeakers");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "EventSpeakers");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "EventSpeakers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpeakerId",
                table: "EventSpeakers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Speakers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventSpeakers_SpeakerId",
                table: "EventSpeakers",
                column: "SpeakerId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpeakers_Events_EventId",
                table: "EventSpeakers",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpeakers_Speakers_SpeakerId",
                table: "EventSpeakers",
                column: "SpeakerId",
                principalTable: "Speakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSpeakers_Events_EventId",
                table: "EventSpeakers");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpeakers_Speakers_SpeakerId",
                table: "EventSpeakers");

            migrationBuilder.DropTable(
                name: "Speakers");

            migrationBuilder.DropIndex(
                name: "IX_EventSpeakers_SpeakerId",
                table: "EventSpeakers");

            migrationBuilder.DropColumn(
                name: "SpeakerId",
                table: "EventSpeakers");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "EventSpeakers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "EventSpeakers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EventSpeakers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "EventSpeakers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpeakers_Events_EventId",
                table: "EventSpeakers",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
