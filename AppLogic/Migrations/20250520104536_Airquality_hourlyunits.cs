using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppLogic.Migrations
{
    /// <inheritdoc />
    public partial class Airquality_hourlyunits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HourlyUnits_AQI",
                table: "AirQualities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HourlyUnits_AlderPollen",
                table: "AirQualities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HourlyUnits_BirchPollen",
                table: "AirQualities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HourlyUnits_CarbonMonoxide",
                table: "AirQualities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HourlyUnits_Dust",
                table: "AirQualities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HourlyUnits_GrassPollen",
                table: "AirQualities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HourlyUnits_Marker",
                table: "AirQualities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HourlyUnits_MugwortPollen",
                table: "AirQualities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HourlyUnits_NitrogenDioxide",
                table: "AirQualities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HourlyUnits_Ozone",
                table: "AirQualities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HourlyUnits_PM25",
                table: "AirQualities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HourlyUnits_RagweedPollen",
                table: "AirQualities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HourlyUnits_UVI",
                table: "AirQualities",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HourlyUnits_AQI",
                table: "AirQualities");

            migrationBuilder.DropColumn(
                name: "HourlyUnits_AlderPollen",
                table: "AirQualities");

            migrationBuilder.DropColumn(
                name: "HourlyUnits_BirchPollen",
                table: "AirQualities");

            migrationBuilder.DropColumn(
                name: "HourlyUnits_CarbonMonoxide",
                table: "AirQualities");

            migrationBuilder.DropColumn(
                name: "HourlyUnits_Dust",
                table: "AirQualities");

            migrationBuilder.DropColumn(
                name: "HourlyUnits_GrassPollen",
                table: "AirQualities");

            migrationBuilder.DropColumn(
                name: "HourlyUnits_Marker",
                table: "AirQualities");

            migrationBuilder.DropColumn(
                name: "HourlyUnits_MugwortPollen",
                table: "AirQualities");

            migrationBuilder.DropColumn(
                name: "HourlyUnits_NitrogenDioxide",
                table: "AirQualities");

            migrationBuilder.DropColumn(
                name: "HourlyUnits_Ozone",
                table: "AirQualities");

            migrationBuilder.DropColumn(
                name: "HourlyUnits_PM25",
                table: "AirQualities");

            migrationBuilder.DropColumn(
                name: "HourlyUnits_RagweedPollen",
                table: "AirQualities");

            migrationBuilder.DropColumn(
                name: "HourlyUnits_UVI",
                table: "AirQualities");
        }
    }
}
