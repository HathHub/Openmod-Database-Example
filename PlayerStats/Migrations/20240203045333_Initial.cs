using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayerStats.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerStats_Players",
                columns: table => new
                {
                    PlayerID = table.Column<ulong>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PlayerName = table.Column<string>(nullable: false),
                    Zombies = table.Column<int>(nullable: false, defaultValue: 0),
                    Messages = table.Column<int>(nullable: false, defaultValue: 0),
                    Deaths = table.Column<int>(nullable: false, defaultValue: 0),
                    Headshots = table.Column<int>(nullable: false, defaultValue: 0),
                    Kills = table.Column<int>(nullable: false, defaultValue: 0),
                    MegaZombies = table.Column<int>(nullable: false, defaultValue: 0),
                    Resources = table.Column<int>(nullable: false, defaultValue: 0),
                    Harvests = table.Column<int>(nullable: false, defaultValue: 0),
                    Fish = table.Column<int>(nullable: false, defaultValue: 0),
                    Animals = table.Column<int>(nullable: false, defaultValue: 0),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStats_Players", x => x.PlayerID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerStats_Players");
        }
    }
}
