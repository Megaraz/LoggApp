using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppLogic.Migrations
{
    /// <inheritdoc />
    public partial class AI_Summary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HourlyUnits_NitrogenDioxide",
                table: "AirQualityData",
                newName: "HourlyUnits_NO2");

            migrationBuilder.RenameColumn(
                name: "HourlyUnits_CarbonMonoxide",
                table: "AirQualityData",
                newName: "HourlyUnits_CO");

            migrationBuilder.AddColumn<string>(
                name: "AISummary",
                table: "WeatherData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AISummary",
                table: "AirQualityData",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AISummary",
                table: "WeatherData");

            migrationBuilder.DropColumn(
                name: "AISummary",
                table: "AirQualityData");

            migrationBuilder.RenameColumn(
                name: "HourlyUnits_NO2",
                table: "AirQualityData",
                newName: "HourlyUnits_NitrogenDioxide");

            migrationBuilder.RenameColumn(
                name: "HourlyUnits_CO",
                table: "AirQualityData",
                newName: "HourlyUnits_CarbonMonoxide");
        }
    }
}
