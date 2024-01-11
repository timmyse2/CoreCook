using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core_CodeFirst.Migrations
{
    public partial class DB_Core : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Makers",
                columns: table => new
                {
                    MakerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makers", x => x.MakerId);
                });

            migrationBuilder.CreateTable(
                name: "Comics",
                columns: table => new
                {
                    ComicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ComicName = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MakerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comics", x => x.ComicId);
                    table.ForeignKey(
                        name: "FK_Comics_Makers_MakerId",
                        column: x => x.MakerId,
                        principalTable: "Makers",
                        principalColumn: "MakerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comics_MakerId",
                table: "Comics",
                column: "MakerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comics");

            migrationBuilder.DropTable(
                name: "Makers");
        }
    }
}
