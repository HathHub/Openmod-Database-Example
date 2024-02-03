using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenmodDatabaseExample.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OpenmodDatabaseExample_Servers",
                table: "OpenmodDatabaseExample_Servers");

            migrationBuilder.RenameTable(
                name: "OpenmodDatabaseExample_Servers",
                newName: "OpenmodDatabaseExample_Players");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpenmodDatabaseExample_Players",
                table: "OpenmodDatabaseExample_Players",
                column: "SteamID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OpenmodDatabaseExample_Players",
                table: "OpenmodDatabaseExample_Players");

            migrationBuilder.RenameTable(
                name: "OpenmodDatabaseExample_Players",
                newName: "OpenmodDatabaseExample_Servers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpenmodDatabaseExample_Servers",
                table: "OpenmodDatabaseExample_Servers",
                column: "SteamID");
        }
    }
}
