using Microsoft.EntityFrameworkCore.Migrations;

namespace spaceparkapi.Migrations
{
    public partial class UpdatedParkingspot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Parkingspot",
                keyColumn: "Id",
                keyValue: 1,
                column: "ParkedSpaceshipId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Parkingspot",
                keyColumn: "Id",
                keyValue: 2,
                column: "ParkedSpaceshipId",
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Parkingspot",
                keyColumn: "Id",
                keyValue: 1,
                column: "ParkedSpaceshipId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Parkingspot",
                keyColumn: "Id",
                keyValue: 2,
                column: "ParkedSpaceshipId",
                value: null);
        }
    }
}
