using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangePrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManagementJobs_Playlists_PlaylistId",
                schema: "PlaylistManager",
                table: "ManagementJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_ManagementJobs_Users_UserId",
                schema: "PlaylistManager",
                table: "ManagementJobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ManagementJobs",
                schema: "PlaylistManager",
                table: "ManagementJobs");

            migrationBuilder.DropIndex(
                name: "IX_ManagementJobs_UserId",
                schema: "PlaylistManager",
                table: "ManagementJobs");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "PlaylistManager",
                table: "ManagementJobs",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PlaylistId",
                schema: "PlaylistManager",
                table: "ManagementJobs",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ManagementJobs",
                schema: "PlaylistManager",
                table: "ManagementJobs",
                columns: new[] { "UserId", "PlaylistId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ManagementJobs_Playlists_PlaylistId",
                schema: "PlaylistManager",
                table: "ManagementJobs",
                column: "PlaylistId",
                principalSchema: "PlaylistManager",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ManagementJobs_Users_UserId",
                schema: "PlaylistManager",
                table: "ManagementJobs",
                column: "UserId",
                principalSchema: "PlaylistManager",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManagementJobs_Playlists_PlaylistId",
                schema: "PlaylistManager",
                table: "ManagementJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_ManagementJobs_Users_UserId",
                schema: "PlaylistManager",
                table: "ManagementJobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ManagementJobs",
                schema: "PlaylistManager",
                table: "ManagementJobs");

            migrationBuilder.AlterColumn<string>(
                name: "PlaylistId",
                schema: "PlaylistManager",
                table: "ManagementJobs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "PlaylistManager",
                table: "ManagementJobs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ManagementJobs",
                schema: "PlaylistManager",
                table: "ManagementJobs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ManagementJobs_UserId",
                schema: "PlaylistManager",
                table: "ManagementJobs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ManagementJobs_Playlists_PlaylistId",
                schema: "PlaylistManager",
                table: "ManagementJobs",
                column: "PlaylistId",
                principalSchema: "PlaylistManager",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ManagementJobs_Users_UserId",
                schema: "PlaylistManager",
                table: "ManagementJobs",
                column: "UserId",
                principalSchema: "PlaylistManager",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
