using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class RenamePlaylistTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManagementJobs_Playlist_PlaylistId",
                schema: "PlaylistManager",
                table: "ManagementJobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Playlist",
                schema: "PlaylistManager",
                table: "Playlist");

            migrationBuilder.RenameTable(
                name: "Playlist",
                schema: "PlaylistManager",
                newName: "Playlists",
                newSchema: "PlaylistManager");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Playlists",
                schema: "PlaylistManager",
                table: "Playlists",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ManagementJobs_Playlists_PlaylistId",
                schema: "PlaylistManager",
                table: "ManagementJobs",
                column: "PlaylistId",
                principalSchema: "PlaylistManager",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManagementJobs_Playlists_PlaylistId",
                schema: "PlaylistManager",
                table: "ManagementJobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Playlists",
                schema: "PlaylistManager",
                table: "Playlists");

            migrationBuilder.RenameTable(
                name: "Playlists",
                schema: "PlaylistManager",
                newName: "Playlist",
                newSchema: "PlaylistManager");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Playlist",
                schema: "PlaylistManager",
                table: "Playlist",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ManagementJobs_Playlist_PlaylistId",
                schema: "PlaylistManager",
                table: "ManagementJobs",
                column: "PlaylistId",
                principalSchema: "PlaylistManager",
                principalTable: "Playlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
