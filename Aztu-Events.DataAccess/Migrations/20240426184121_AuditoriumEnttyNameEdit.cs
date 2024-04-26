using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aztu_Events.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AuditoriumEnttyNameEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxPersonal",
                table: "Audutoria",
                newName: "AuditoryCapacity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuditoryCapacity",
                table: "Audutoria",
                newName: "MaxPersonal");
        }
    }
}
