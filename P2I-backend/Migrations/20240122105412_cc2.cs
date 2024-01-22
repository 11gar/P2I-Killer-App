using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P2I_backend.Migrations
{
    /// <inheritdoc />
    public partial class cc2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cibles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdGame = table.Column<int>(type: "INTEGER", nullable: false),
                    IdKiller = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCible = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cibles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Moderators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdGame = table.Column<int>(type: "INTEGER", nullable: false),
                    IdModerator = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moderators", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cibles");

            migrationBuilder.DropTable(
                name: "Moderators");
        }
    }
}
