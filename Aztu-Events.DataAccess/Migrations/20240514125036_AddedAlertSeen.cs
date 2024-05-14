using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aztu_Events.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedAlertSeen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AlertSeen",
                table: "Confrans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AlertSeen",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlertSeen",
                table: "Confrans");

            migrationBuilder.DropColumn(
                name: "AlertSeen",
                table: "Comments");
        }
    }
}
