using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aztu_Events.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TimeEntityEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AudutorimTimes");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Times");

            migrationBuilder.AddColumn<Guid>(
                name: "AuditoriumId",
                table: "Times",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ConfransId",
                table: "Times",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Times",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "EndTime",
                table: "Times",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "StartedTime",
                table: "Times",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<Guid>(
                name: "TimeId",
                table: "Confrans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Times_AuditoriumId",
                table: "Times",
                column: "AuditoriumId");

            migrationBuilder.CreateIndex(
                name: "IX_Confrans_TimeId",
                table: "Confrans",
                column: "TimeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Confrans_Times_TimeId",
                table: "Confrans",
                column: "TimeId",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Times_Audutoria_AuditoriumId",
                table: "Times",
                column: "AuditoriumId",
                principalTable: "Audutoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Confrans_Times_TimeId",
                table: "Confrans");

            migrationBuilder.DropForeignKey(
                name: "FK_Times_Audutoria_AuditoriumId",
                table: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Times_AuditoriumId",
                table: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Confrans_TimeId",
                table: "Confrans");

            migrationBuilder.DropColumn(
                name: "AuditoriumId",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "ConfransId",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "StartedTime",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "TimeId",
                table: "Confrans");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Times",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "AudutorimTimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AudutoriumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudutorimTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AudutorimTimes_Audutoria_AudutoriumId",
                        column: x => x.AudutoriumId,
                        principalTable: "Audutoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AudutorimTimes_Times_TimeId",
                        column: x => x.TimeId,
                        principalTable: "Times",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AudutorimTimes_AudutoriumId",
                table: "AudutorimTimes",
                column: "AudutoriumId");

            migrationBuilder.CreateIndex(
                name: "IX_AudutorimTimes_TimeId",
                table: "AudutorimTimes",
                column: "TimeId");
        }
    }
}
