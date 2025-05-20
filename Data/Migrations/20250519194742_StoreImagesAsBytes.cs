using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomcheBGLTD.Data.Migrations
{
    /// <inheritdoc />
    public partial class StoreImagesAsBytes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "ListingImages");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "ListingImages",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "ListingImages",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "ListingImages");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "ListingImages");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "ListingImages",
                type: "nvarchar(260)",
                maxLength: 260,
                nullable: false,
                defaultValue: "");
        }
    }
}
