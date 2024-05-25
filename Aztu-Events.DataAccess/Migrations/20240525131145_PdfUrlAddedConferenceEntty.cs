using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aztu_Events.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PdfUrlAddedConferenceEntty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlertSeen",
                table: "Confrans");

            migrationBuilder.DropColumn(
                name: "AlertSeenForUser",
                table: "Confrans");

            migrationBuilder.AddColumn<string>(
                name: "PdfUrl",
                table: "Confrans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PdfUrl",
                table: "Confrans");

            migrationBuilder.AddColumn<bool>(
                name: "AlertSeen",
                table: "Confrans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AlertSeenForUser",
                table: "Confrans",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
