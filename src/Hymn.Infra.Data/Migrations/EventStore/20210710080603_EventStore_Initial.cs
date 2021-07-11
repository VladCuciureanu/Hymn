using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hymn.Infra.Data.Migrations.EventStore
{
    public partial class EventStore_Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "StoredEvent",
                table => new
                {
                    Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                    Action = table.Column<string>("varchar(100)", nullable: true),
                    AggregateId = table.Column<Guid>("uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_StoredEvent", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "StoredEvent");
        }
    }
}