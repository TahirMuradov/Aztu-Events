using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aztu_Events.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UserAddedConfrans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Confrans_Audutoria_AuditoriumId",
                table: "Confrans");

            migrationBuilder.RenameColumn(
                name: "DurationofContinuation",
                table: "Confrans",
                newName: "EndDate");

            migrationBuilder.AddForeignKey(
                name: "FK_Confrans_Audutoria_AuditoriumId",
                table: "Confrans",
                column: "AuditoriumId",
                principalTable: "Audutoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Confrans_Audutoria_AuditoriumId",
                table: "Confrans");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Confrans",
                newName: "DurationofContinuation");

            migrationBuilder.AddForeignKey(
                name: "FK_Confrans_Audutoria_AuditoriumId",
                table: "Confrans",
                column: "AuditoriumId",
                principalTable: "Audutoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
