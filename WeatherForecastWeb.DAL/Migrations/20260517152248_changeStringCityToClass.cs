using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WeatherForecastWeb.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changeStringCityToClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "WeatherForecasts");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "WeatherForecasts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecasts_CityId",
                table: "WeatherForecasts",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherForecasts_City_CityId",
                table: "WeatherForecasts",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherForecasts_City_CityId",
                table: "WeatherForecasts");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropIndex(
                name: "IX_WeatherForecasts_CityId",
                table: "WeatherForecasts");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "WeatherForecasts");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "WeatherForecasts",
                type: "text",
                nullable: true);
        }
    }
}
