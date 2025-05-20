using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomcheBGLTD.Data.Migrations
{
    /// <inheritdoc />
    public partial class RevertNeighborhoods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Neighborhoods_NeighborhoodId",
                table: "Listings");

            migrationBuilder.DropTable(
                name: "Neighborhoods");

            migrationBuilder.DropIndex(
                name: "IX_Listings_NeighborhoodId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "NeighborhoodId",
                table: "Listings");

            migrationBuilder.AlterColumn<string>(
                name: "Address2",
                table: "Listings",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address1",
                table: "Listings",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Address2",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address1",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NeighborhoodId",
                table: "Listings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Neighborhoods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
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
                name: "IX_Listings_NeighborhoodId",
                table: "Listings",
                column: "NeighborhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Neighborhoods_CityId_Name",
                table: "Neighborhoods",
                columns: new[] { "CityId", "Name" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Neighborhoods_NeighborhoodId",
                table: "Listings",
                column: "NeighborhoodId",
                principalTable: "Neighborhoods",
                principalColumn: "Id");
        }
    }
}
