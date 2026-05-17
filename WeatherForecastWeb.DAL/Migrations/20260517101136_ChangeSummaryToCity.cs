using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherForecastWeb.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSummaryToCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Summary",
                table: "WeatherForecasts");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "WeatherForecasts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "WeatherForecasts");

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "WeatherForecasts",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
