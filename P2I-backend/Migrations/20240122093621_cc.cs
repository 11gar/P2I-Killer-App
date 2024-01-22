using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P2I_backend.Migrations
{
    /// <inheritdoc />
    public partial class cc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Games_GameId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInGames_Games_GameId",
                table: "UsersInGames");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInGames_UsersInGames_CibleId",
                table: "UsersInGames");

            migrationBuilder.DropIndex(
                name: "IX_UsersInGames_CibleId",
                table: "UsersInGames");

            migrationBuilder.DropIndex(
                name: "IX_UsersInGames_GameId",
                table: "UsersInGames");

            migrationBuilder.DropIndex(
                name: "IX_Users_GameId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CibleId",
                table: "UsersInGames");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "UsersInGames");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CibleId",
                table: "UsersInGames",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "UsersInGames",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersInGames_CibleId",
                table: "UsersInGames",
                column: "CibleId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInGames_GameId",
                table: "UsersInGames",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GameId",
                table: "Users",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Games_GameId",
                table: "Users",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInGames_Games_GameId",
                table: "UsersInGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInGames_UsersInGames_CibleId",
                table: "UsersInGames",
                column: "CibleId",
                principalTable: "UsersInGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
