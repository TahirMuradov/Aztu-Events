using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aztu_Events.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ConfranceLaunguageEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfransContent",
                table: "Confrans");

            migrationBuilder.DropColumn(
                name: "ConfransName",
                table: "Confrans");

            migrationBuilder.CreateTable(
                name: "ConfranceLaunguages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfransName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfransContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LangCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfransId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfranceLaunguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfranceLaunguages_Confrans_ConfransId",
                        column: x => x.ConfransId,
                        principalTable: "Confrans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfranceLaunguages_ConfransId",
                table: "ConfranceLaunguages",
                column: "ConfransId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfranceLaunguages");

            migrationBuilder.AddColumn<string>(
                name: "ConfransContent",
                table: "Confrans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ConfransName",
                table: "Confrans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
