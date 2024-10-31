using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChampionsLeague.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Restrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_TeamOne",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_TeamTwo",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Teams_TeamName",
                table: "Tables");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_TeamOne",
                table: "Games",
                column: "TeamOne",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_TeamTwo",
                table: "Games",
                column: "TeamTwo",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Teams_TeamName",
                table: "Tables",
                column: "TeamName",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_TeamOne",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_TeamTwo",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Teams_TeamName",
                table: "Tables");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_TeamOne",
                table: "Games",
                column: "TeamOne",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_TeamTwo",
                table: "Games",
                column: "TeamTwo",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Teams_TeamName",
                table: "Tables",
                column: "TeamName",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
