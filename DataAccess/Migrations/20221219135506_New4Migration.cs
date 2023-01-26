using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class New4Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_RefreshTokens_LatestRefreshTokenId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_LatestRefreshTokenId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LatestRefreshTokenId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LatestRefreshTokenId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RefreshTokenId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_LatestRefreshTokenId",
                table: "Users",
                column: "LatestRefreshTokenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_RefreshTokens_LatestRefreshTokenId",
                table: "Users",
                column: "LatestRefreshTokenId",
                principalTable: "RefreshTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
