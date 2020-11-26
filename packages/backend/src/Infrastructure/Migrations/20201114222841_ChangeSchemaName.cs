using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangeSchemaName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "PlaylistManager");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "PlaylistManager");

            migrationBuilder.RenameTable(
                name: "Playlist",
                newName: "Playlist",
                newSchema: "PlaylistManager");

            migrationBuilder.RenameTable(
                name: "ManagementJobs",
                newName: "ManagementJobs",
                newSchema: "PlaylistManager");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Users",
                schema: "PlaylistManager",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Playlist",
                schema: "PlaylistManager",
                newName: "Playlist");

            migrationBuilder.RenameTable(
                name: "ManagementJobs",
                schema: "PlaylistManager",
                newName: "ManagementJobs");
        }
    }
}
