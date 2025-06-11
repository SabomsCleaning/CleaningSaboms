using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleaningSaboms.Migrations
{
    /// <inheritdoc />
    public partial class FixTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActuelEndTime",
                table: "Booking",
                newName: "ActualEndTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActualEndTime",
                table: "Booking",
                newName: "ActuelEndTime");
        }
    }
}
