using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBookingDiagnostics.Migrations
{
    /// <inheritdoc />
    public partial class BookingChanges1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            // Drop the BookingId column
            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Bookings");

            // Recreate the BookingId column with the identity property
            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            // Add a new primary key constraint
            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "BookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            // Drop the BookingId column
            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Bookings");

            // Recreate the BookingId column as a string
            migrationBuilder.AddColumn<string>(
                name: "BookingId",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            // Add a new primary key constraint
            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "BookingId");
        }
    }
}
