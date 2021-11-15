using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventCalendarServer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventsContents",
                columns: table => new
                {
                    CommentCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Comment = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsContents", x => x.CommentCreated);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    Day = table.Column<int>(type: "INTEGER", nullable: false),
                    CommentCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EventsContentsCommentCreated = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Created);
                    table.ForeignKey(
                        name: "FK_Events_EventsContents_EventsContentsCommentCreated",
                        column: x => x.EventsContentsCommentCreated,
                        principalTable: "EventsContents",
                        principalColumn: "CommentCreated",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventsContentsCommentCreated",
                table: "Events",
                column: "EventsContentsCommentCreated");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "EventsContents");
        }
    }
}
