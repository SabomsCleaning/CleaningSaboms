using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleaningSaboms.Migrations
{
    /// <inheritdoc />
    public partial class AddtimetoBookingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ActualStartTime",
                table: "Booking",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActuelEndTime",
                table: "Booking",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduleEndTime",
                table: "Booking",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduleStartTime",
                table: "Booking",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualStartTime",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ActuelEndTime",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ScheduleEndTime",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ScheduleStartTime",
                table: "Booking");
        }
    }
}
