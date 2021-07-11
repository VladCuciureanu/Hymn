using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hymn.Infra.Data.Migrations
{
    public partial class Hymn_SongVersionAlbumId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AlbumId",
                table: "SongVersions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SongVersions_AlbumId",
                table: "SongVersions",
                column: "AlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_SongVersions_Albums_AlbumId",
                table: "SongVersions",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongVersions_Albums_AlbumId",
                table: "SongVersions");

            migrationBuilder.DropIndex(
                name: "IX_SongVersions_AlbumId",
                table: "SongVersions");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "SongVersions");
        }
    }
}
