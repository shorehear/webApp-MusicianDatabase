using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AugustPractice.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collectives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CollectiveName = table.Column<string>(type: "TEXT", nullable: false),
                    CollectiveGenre = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collectives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CountryName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Musicians",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MusicianName = table.Column<string>(type: "TEXT", nullable: false),
                    MusicianBirthDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MusicianDeathDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CountryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CollectiveId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musicians", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Musicians_Collectives_CollectiveId",
                        column: x => x.CollectiveId,
                        principalTable: "Collectives",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Musicians_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AlbumTitle = table.Column<string>(type: "TEXT", nullable: false),
                    NumberOfTracks = table.Column<int>(type: "INTEGER", nullable: false),
                    ReleaseYear = table.Column<int>(type: "INTEGER", nullable: false),
                    CollectiveId = table.Column<Guid>(type: "TEXT", nullable: true),
                    MusicianId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albums_Collectives_CollectiveId",
                        column: x => x.CollectiveId,
                        principalTable: "Collectives",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Albums_Musicians_MusicianId",
                        column: x => x.MusicianId,
                        principalTable: "Musicians",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SongTitle = table.Column<string>(type: "TEXT", nullable: false),
                    AlbumId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Songs_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_CollectiveId",
                table: "Albums",
                column: "CollectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_MusicianId",
                table: "Albums",
                column: "MusicianId");

            migrationBuilder.CreateIndex(
                name: "IX_Musicians_CollectiveId",
                table: "Musicians",
                column: "CollectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Musicians_CountryId",
                table: "Musicians",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_AlbumId",
                table: "Songs",
                column: "AlbumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Musicians");

            migrationBuilder.DropTable(
                name: "Collectives");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
