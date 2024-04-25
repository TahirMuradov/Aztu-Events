using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aztu_Events.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class modelbulderAddedConfugire : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AudutorimTime_Audutoria_AudutoriumId",
                table: "AudutorimTime");

            migrationBuilder.DropForeignKey(
                name: "FK_AudutorimTime_Time_TimeId",
                table: "AudutorimTime");

            migrationBuilder.DropForeignKey(
                name: "FK_Confrans_Audutoria_AudutoriumId",
                table: "Confrans");

            migrationBuilder.DropIndex(
                name: "IX_Confrans_AudutoriumId",
                table: "Confrans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Time",
                table: "Time");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AudutorimTime",
                table: "AudutorimTime");

            migrationBuilder.DropColumn(
                name: "AudutoriumId",
                table: "Confrans");

            migrationBuilder.RenameTable(
                name: "Time",
                newName: "Times");

            migrationBuilder.RenameTable(
                name: "AudutorimTime",
                newName: "AudutorimTimes");

            migrationBuilder.RenameIndex(
                name: "IX_AudutorimTime_TimeId",
                table: "AudutorimTimes",
                newName: "IX_AudutorimTimes_TimeId");

            migrationBuilder.RenameIndex(
                name: "IX_AudutorimTime_AudutoriumId",
                table: "AudutorimTimes",
                newName: "IX_AudutorimTimes_AudutoriumId");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuditoriumId",
                table: "Confrans",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Times",
                table: "Times",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AudutorimTimes",
                table: "AudutorimTimes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Confrans_AuditoriumId",
                table: "Confrans",
                column: "AuditoriumId");

            migrationBuilder.AddForeignKey(
                name: "FK_AudutorimTimes_Audutoria_AudutoriumId",
                table: "AudutorimTimes",
                column: "AudutoriumId",
                principalTable: "Audutoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AudutorimTimes_Times_TimeId",
                table: "AudutorimTimes",
                column: "TimeId",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Confrans_Audutoria_AuditoriumId",
                table: "Confrans",
                column: "AuditoriumId",
                principalTable: "Audutoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AudutorimTimes_Audutoria_AudutoriumId",
                table: "AudutorimTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_AudutorimTimes_Times_TimeId",
                table: "AudutorimTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Confrans_Audutoria_AuditoriumId",
                table: "Confrans");

            migrationBuilder.DropIndex(
                name: "IX_Confrans_AuditoriumId",
                table: "Confrans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Times",
                table: "Times");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AudutorimTimes",
                table: "AudutorimTimes");

            migrationBuilder.RenameTable(
                name: "Times",
                newName: "Time");

            migrationBuilder.RenameTable(
                name: "AudutorimTimes",
                newName: "AudutorimTime");

            migrationBuilder.RenameIndex(
                name: "IX_AudutorimTimes_TimeId",
                table: "AudutorimTime",
                newName: "IX_AudutorimTime_TimeId");

            migrationBuilder.RenameIndex(
                name: "IX_AudutorimTimes_AudutoriumId",
                table: "AudutorimTime",
                newName: "IX_AudutorimTime_AudutoriumId");

            migrationBuilder.AlterColumn<string>(
                name: "AuditoriumId",
                table: "Confrans",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "AudutoriumId",
                table: "Confrans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Time",
                table: "Time",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AudutorimTime",
                table: "AudutorimTime",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Confrans_AudutoriumId",
                table: "Confrans",
                column: "AudutoriumId");

            migrationBuilder.AddForeignKey(
                name: "FK_AudutorimTime_Audutoria_AudutoriumId",
                table: "AudutorimTime",
                column: "AudutoriumId",
                principalTable: "Audutoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AudutorimTime_Time_TimeId",
                table: "AudutorimTime",
                column: "TimeId",
                principalTable: "Time",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Confrans_Audutoria_AudutoriumId",
                table: "Confrans",
                column: "AudutoriumId",
                principalTable: "Audutoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
