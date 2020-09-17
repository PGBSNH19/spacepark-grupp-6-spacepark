using Microsoft.EntityFrameworkCore.Migrations;

namespace spaceparkapi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Spaceport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spaceport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Traveller",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traveller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spaceship",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Length = table.Column<double>(nullable: false),
                    TravellerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spaceship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spaceship_Traveller_TravellerId",
                        column: x => x.TravellerId,
                        principalTable: "Traveller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parkingspot",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpaceportId = table.Column<int>(nullable: true),
                    ParkedSpaceshipId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parkingspot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parkingspot_Spaceship_ParkedSpaceshipId",
                        column: x => x.ParkedSpaceshipId,
                        principalTable: "Spaceship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parkingspot_Spaceport_SpaceportId",
                        column: x => x.SpaceportId,
                        principalTable: "Spaceport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Spaceport",
                columns: new[] { "Id", "Name" },
                values: new object[] { 500, "Test Spaceport" });

            migrationBuilder.InsertData(
                table: "Traveller",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[] { 1, "Luke", "Skywalker" });

            migrationBuilder.InsertData(
                table: "Traveller",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[] { 2, "Darth", "Vader" });

            migrationBuilder.InsertData(
                table: "Parkingspot",
                columns: new[] { "Id", "ParkedSpaceshipId", "SpaceportId" },
                values: new object[,]
                {
                    { 1, null, 500 },
                    { 2, null, 500 }
                });

            migrationBuilder.InsertData(
                table: "Spaceship",
                columns: new[] { "Id", "Length", "TravellerId" },
                values: new object[,]
                {
                    { 1, 1337.2, 1 },
                    { 2, 1010.1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parkingspot_ParkedSpaceshipId",
                table: "Parkingspot",
                column: "ParkedSpaceshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Parkingspot_SpaceportId",
                table: "Parkingspot",
                column: "SpaceportId");

            migrationBuilder.CreateIndex(
                name: "IX_Spaceship_TravellerId",
                table: "Spaceship",
                column: "TravellerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parkingspot");

            migrationBuilder.DropTable(
                name: "Spaceship");

            migrationBuilder.DropTable(
                name: "Spaceport");

            migrationBuilder.DropTable(
                name: "Traveller");
        }
    }
}
