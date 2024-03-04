using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P2I_backend.Migrations
{
    /// <inheritdoc />
    public partial class equipecolor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Couleur",
                table: "Equipes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Couleur",
                table: "Equipes");
        }
    }
}
