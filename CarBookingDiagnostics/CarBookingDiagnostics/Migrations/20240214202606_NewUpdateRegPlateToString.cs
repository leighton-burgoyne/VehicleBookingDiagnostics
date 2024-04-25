using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBookingDiagnostics.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRegPlateToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Vehicles");

            migrationBuilder.AddColumn<string>(
                name: "VehicleId",
                table: "Vehicles",
                type: "nvarchar(450)",
                nullable: false);

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Bookings");

            migrationBuilder.AddColumn<string>(
                name: "VehicleId",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Vehicles",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Bookings",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
