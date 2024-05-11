using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aztu_Events.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class IsfeaturedAddedForConfrans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Confrans",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Confrans");
        }
    }
}
