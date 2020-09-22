using Microsoft.EntityFrameworkCore.Migrations;

namespace spaceparkapi.Migrations
{
    public partial class UpdatedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Traveller",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Luke Skywalker");

            migrationBuilder.UpdateData(
                table: "Traveller",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Darth Vader");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Traveller",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: null);

            migrationBuilder.UpdateData(
                table: "Traveller",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: null);
        }
    }
}
