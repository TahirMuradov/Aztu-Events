using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aztu_Events.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SpecialGuestEnttiyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Confrans");

            migrationBuilder.DropColumn(
                name: "StartedDate",
                table: "Confrans");

            migrationBuilder.DropColumn(
                name: "specialGuestsEmail",
                table: "Confrans");

            migrationBuilder.DropColumn(
                name: "specialGuestsName",
                table: "Confrans");

            migrationBuilder.AddColumn<bool>(
                name: "UpdateTime",
                table: "Times",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SpecialGuests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendEmail = table.Column<bool>(type: "bit", nullable: false),
                    ConfransId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialGuests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialGuests_Confrans_ConfransId",
                        column: x => x.ConfransId,
                        principalTable: "Confrans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpecialGuests_ConfransId",
                table: "SpecialGuests",
                column: "ConfransId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecialGuests");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "Times");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Confrans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartedDate",
                table: "Confrans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "specialGuestsEmail",
                table: "Confrans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "specialGuestsName",
                table: "Confrans",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
