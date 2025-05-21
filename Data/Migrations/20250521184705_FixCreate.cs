using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomcheBGLTD.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Construction",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "FeatureList",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Listings");

            migrationBuilder.RenameColumn(
                name: "PropertyType",
                table: "Listings",
                newName: "PropertyTypeId");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExtraInfo",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address1",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Listings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ConstructionTypeId",
                table: "Listings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Listings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConstructionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeatureTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListingFeatures",
                columns: table => new
                {
                    ListingId = table.Column<int>(type: "int", nullable: false),
                    FeatureTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingFeatures", x => new { x.ListingId, x.FeatureTypeId });
                    table.ForeignKey(
                        name: "FK_ListingFeatures_FeatureTypes_FeatureTypeId",
                        column: x => x.FeatureTypeId,
                        principalTable: "FeatureTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListingFeatures_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CityId",
                table: "Listings",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_ConstructionTypeId",
                table: "Listings",
                column: "ConstructionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CurrencyId",
                table: "Listings",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_PropertyTypeId",
                table: "Listings",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ListingFeatures_FeatureTypeId",
                table: "ListingFeatures",
                column: "FeatureTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Cities_CityId",
                table: "Listings",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_ConstructionTypes_ConstructionTypeId",
                table: "Listings",
                column: "ConstructionTypeId",
                principalTable: "ConstructionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Currencies_CurrencyId",
                table: "Listings",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_PropertyTypes_PropertyTypeId",
                table: "Listings",
                column: "PropertyTypeId",
                principalTable: "PropertyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Cities_CityId",
                table: "Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_ConstructionTypes_ConstructionTypeId",
                table: "Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Currencies_CurrencyId",
                table: "Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_PropertyTypes_PropertyTypeId",
                table: "Listings");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "ConstructionTypes");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "ListingFeatures");

            migrationBuilder.DropTable(
                name: "PropertyTypes");

            migrationBuilder.DropTable(
                name: "FeatureTypes");

            migrationBuilder.DropIndex(
                name: "IX_Listings_CityId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_ConstructionTypeId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_CurrencyId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_PropertyTypeId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "ConstructionTypeId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Listings");

            migrationBuilder.RenameColumn(
                name: "PropertyTypeId",
                table: "Listings",
                newName: "PropertyType");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ExtraInfo",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address1",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Construction",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FeatureList",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
