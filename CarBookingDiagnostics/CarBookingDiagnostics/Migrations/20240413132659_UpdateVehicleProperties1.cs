using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBookingDiagnostics.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVehicleProperties1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EuroStatus",
                table: "Vehicles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EuroStatus",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
