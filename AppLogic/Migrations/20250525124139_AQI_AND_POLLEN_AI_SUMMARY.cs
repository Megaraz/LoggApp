using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppLogic.Migrations
{
    /// <inheritdoc />
    public partial class AQI_AND_POLLEN_AI_SUMMARY : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AISummary",
                table: "AirQualityData",
                newName: "Pollen_AISummary");

            migrationBuilder.AddColumn<string>(
                name: "AQI_AISummary",
                table: "AirQualityData",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AQI_AISummary",
                table: "AirQualityData");

            migrationBuilder.RenameColumn(
                name: "Pollen_AISummary",
                table: "AirQualityData",
                newName: "AISummary");
        }
    }
}
