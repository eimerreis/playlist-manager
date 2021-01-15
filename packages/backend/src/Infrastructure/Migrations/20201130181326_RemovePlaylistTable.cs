using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class RemovePlaylistTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManagementJobs_Playlists_PlaylistId",
                schema: "PlaylistManager",
                table: "ManagementJobs");

            migrationBuilder.DropTable(
                name: "Playlists",
                schema: "PlaylistManager");

            migrationBuilder.DropIndex(
                name: "IX_ManagementJobs_PlaylistId",
                schema: "PlaylistManager",
                table: "ManagementJobs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Playlists",
                schema: "PlaylistManager",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManagementJobs_PlaylistId",
                schema: "PlaylistManager",
                table: "ManagementJobs",
                column: "PlaylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_ManagementJobs_Playlists_PlaylistId",
                schema: "PlaylistManager",
                table: "ManagementJobs",
                column: "PlaylistId",
                principalSchema: "PlaylistManager",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
