using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P2I_backend.Migrations
{
    /// <inheritdoc />
    public partial class cc4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Moderator",
                table: "UsersInGames");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Moderator",
                table: "UsersInGames",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
