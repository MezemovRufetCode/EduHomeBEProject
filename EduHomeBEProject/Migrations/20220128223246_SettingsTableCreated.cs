using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBEProject.Migrations
{
    public partial class SettingsTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopHeaderAnnounce = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(maxLength: 200, nullable: false),
                    CourseSecTitle = table.Column<string>(nullable: true),
                    EventSecTitle = table.Column<string>(maxLength: 200, nullable: true),
                    BlogSecTitle = table.Column<string>(maxLength: 200, nullable: true),
                    SubscribeSecTitle = table.Column<string>(maxLength: 200, nullable: true),
                    FooterLogo = table.Column<string>(maxLength: 200, nullable: false),
                    FacebookLink = table.Column<string>(maxLength: 200, nullable: true),
                    PinterestLink = table.Column<string>(maxLength: 200, nullable: true),
                    TwitterLink = table.Column<string>(maxLength: 200, nullable: true),
                    VimeoLink = table.Column<string>(maxLength: 200, nullable: true),
                    FAdress = table.Column<string>(maxLength: 200, nullable: true),
                    FContact = table.Column<string>(maxLength: 100, nullable: true),
                    SContact = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
