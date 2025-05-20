using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomcheBGLTD.Data.Migrations
{
    /// <inheritdoc />
    public partial class CitiesNeighborhoods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Listings");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Listings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NeighborhoodId",
                table: "Listings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Neighborhoods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Neighborhoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Neighborhoods_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CityId",
                table: "Listings",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_NeighborhoodId",
                table: "Listings",
                column: "NeighborhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Neighborhoods_CityId_Name",
                table: "Neighborhoods",
                columns: new[] { "CityId", "Name" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Cities_CityId",
                table: "Listings",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Neighborhoods_NeighborhoodId",
                table: "Listings",
                column: "NeighborhoodId",
                principalTable: "Neighborhoods",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Cities_CityId",
                table: "Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Neighborhoods_NeighborhoodId",
                table: "Listings");

            migrationBuilder.DropTable(
                name: "Neighborhoods");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Listings_CityId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_NeighborhoodId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "NeighborhoodId",
                table: "Listings");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
