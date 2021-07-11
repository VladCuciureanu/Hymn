using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hymn.Infra.Data.Migrations
{
    public partial class Hymn_SongVersionArtistIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongVersions_Artists_ArtistId",
                table: "SongVersions");

            migrationBuilder.AlterColumn<Guid>(
                name: "ArtistId",
                table: "SongVersions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_SongVersions_Artists_ArtistId",
                table: "SongVersions",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongVersions_Artists_ArtistId",
                table: "SongVersions");

            migrationBuilder.AlterColumn<Guid>(
                name: "ArtistId",
                table: "SongVersions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SongVersions_Artists_ArtistId",
                table: "SongVersions",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
