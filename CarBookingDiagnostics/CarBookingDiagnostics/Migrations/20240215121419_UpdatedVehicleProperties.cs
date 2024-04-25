using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBookingDiagnostics.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedVehicleProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstRegistered",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "Transmission",
                table: "Vehicles",
                newName: "YearOfManufacture");

            migrationBuilder.AddColumn<string>(
                name: "EuroStatus",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MOTExpiryDate",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MonthOfFirstRegistration",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EuroStatus",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "MOTExpiryDate",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "MonthOfFirstRegistration",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "YearOfManufacture",
                table: "Vehicles",
                newName: "Transmission");

            migrationBuilder.AddColumn<DateOnly>(
                name: "FirstRegistered",
                table: "Vehicles",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
