using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventCalendarServer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EventId = table.Column<string>(type: "TEXT", nullable: true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    Day = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Created);
                });

            migrationBuilder.CreateTable(
                name: "EventsContents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    EventId = table.Column<string>(type: "TEXT", nullable: true),
                    EventsCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Comment = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventsContents_Events_EventsCreated",
                        column: x => x.EventsCreated,
                        principalTable: "Events",
                        principalColumn: "Created",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventsContents_EventsCreated",
                table: "EventsContents",
                column: "EventsCreated");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventsContents");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
