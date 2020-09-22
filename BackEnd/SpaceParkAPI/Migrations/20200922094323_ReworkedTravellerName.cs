using Microsoft.EntityFrameworkCore.Migrations;

namespace spaceparkapi.Migrations
{
    public partial class ReworkedTravellerName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Traveller");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Traveller");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Traveller",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Spaceship",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Traveller");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Spaceship");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Traveller",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Traveller",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Traveller",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Luke", "Skywalker" });

            migrationBuilder.UpdateData(
                table: "Traveller",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Darth", "Vader" });
        }
    }
}
