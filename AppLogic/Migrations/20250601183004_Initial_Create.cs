using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppLogic.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<double>(type: "float", nullable: true),
                    Lon = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DayCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayCards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AirQualityData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayCardId = table.Column<int>(type: "int", nullable: true),
                    AQI_AISummary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pollen_AISummary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<double>(type: "float", nullable: true),
                    Lon = table.Column<double>(type: "float", nullable: true),
                    GenerationTime_ms = table.Column<double>(type: "float", nullable: false),
                    HourlyUnits_Marker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_AlderPollen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_BirchPollen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_GrassPollen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_MugwortPollen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_RagweedPollen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_UVI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_AQI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_PM25 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_Ozone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_CO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_NO2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_Dust = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_Marker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_AlderPollen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_BirchPollen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_GrassPollen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_MugwortPollen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_RagweedPollen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_UVI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_AQI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_PM25 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_Ozone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_CarbonMonoxide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_NitrogenDioxide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_Dust = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirQualityData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirQualityData_DayCards_DayCardId",
                        column: x => x.DayCardId,
                        principalTable: "DayCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CaffeineDrinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayCardId = table.Column<int>(type: "int", nullable: false),
                    TimeOf = table.Column<TimeOnly>(type: "time", nullable: true),
                    EstimatedMgCaffeine = table.Column<int>(type: "int", nullable: true),
                    TypeOfDrink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaffeineDrinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaffeineDrinks_DayCards_DayCardId",
                        column: x => x.DayCardId,
                        principalTable: "DayCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseType = table.Column<int>(type: "int", nullable: true),
                    PerceivedIntensity = table.Column<int>(type: "int", nullable: true),
                    TrainingLoad = table.Column<int>(type: "int", nullable: true),
                    AvgHeartRate = table.Column<int>(type: "int", nullable: true),
                    ActiveKcalBurned = table.Column<int>(type: "int", nullable: true),
                    DistanceInKm = table.Column<double>(type: "float", nullable: true),
                    AvgKmTempo = table.Column<TimeSpan>(type: "time", nullable: true),
                    Steps = table.Column<int>(type: "int", nullable: true),
                    AvgStepLengthInCm = table.Column<int>(type: "int", nullable: true),
                    AvgStepPerMin = table.Column<int>(type: "int", nullable: true),
                    DayCardId = table.Column<int>(type: "int", nullable: false),
                    TimeOf = table.Column<TimeOnly>(type: "time", nullable: true),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_DayCards_DayCardId",
                        column: x => x.DayCardId,
                        principalTable: "DayCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sleep",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayCardId = table.Column<int>(type: "int", nullable: false),
                    SleepStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SleepEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalSleepTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    DeepSleepDuration = table.Column<TimeSpan>(type: "time", nullable: true),
                    LightSleepDuration = table.Column<TimeSpan>(type: "time", nullable: true),
                    RemSleepDuration = table.Column<TimeSpan>(type: "time", nullable: true),
                    SleepScore = table.Column<int>(type: "int", nullable: true),
                    TimesWokenUp = table.Column<int>(type: "int", nullable: true),
                    AvgBPM = table.Column<int>(type: "int", nullable: true),
                    Avg02 = table.Column<int>(type: "int", nullable: true),
                    AvgBreathsPerMin = table.Column<int>(type: "int", nullable: true),
                    PerceivedSleepQuality = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sleep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sleep_DayCards_DayCardId",
                        column: x => x.DayCardId,
                        principalTable: "DayCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeatherData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayCardId = table.Column<int>(type: "int", nullable: true),
                    AISummary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<double>(type: "float", nullable: true),
                    Lon = table.Column<double>(type: "float", nullable: true),
                    GenerationTimeMs = table.Column<double>(type: "float", nullable: true),
                    UtcOffsetSeconds = table.Column<int>(type: "int", nullable: true),
                    Timezone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimezoneAbbreviation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_Marker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_Temperature2m = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_ApparentTemperature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_RelativeHumidity2m = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_DewPoint2m = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_Precipitation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_Rain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_CloudCover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_UvIndex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_WindSpeed10m = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_PressureMsl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyBlock_IsDay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_Marker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_Temperature2m = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_ApparentTemperature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_RelativeHumidity2m = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_DewPoint2m = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_Precipitation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_Rain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_CloudCover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_UvIndex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_WindSpeed10m = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_PressureMsl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyUnits_IsDay = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherData_DayCards_DayCardId",
                        column: x => x.DayCardId,
                        principalTable: "DayCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "WellnessCheckIns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayCardId = table.Column<int>(type: "int", nullable: false),
                    TimeOf = table.Column<TimeOnly>(type: "time", nullable: true),
                    EnergyLevel = table.Column<int>(type: "int", nullable: true),
                    MoodLevel = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WellnessCheckIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WellnessCheckIns_DayCards_DayCardId",
                        column: x => x.DayCardId,
                        principalTable: "DayCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirQualityData_DayCardId",
                table: "AirQualityData",
                column: "DayCardId",
                unique: true,
                filter: "[DayCardId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CaffeineDrinks_DayCardId",
                table: "CaffeineDrinks",
                column: "DayCardId");

            migrationBuilder.CreateIndex(
                name: "IX_DayCards_UserId",
                table: "DayCards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_DayCardId",
                table: "Exercises",
                column: "DayCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Sleep_DayCardId",
                table: "Sleep",
                column: "DayCardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WeatherData_DayCardId",
                table: "WeatherData",
                column: "DayCardId",
                unique: true,
                filter: "[DayCardId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WellnessCheckIns_DayCardId",
                table: "WellnessCheckIns",
                column: "DayCardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirQualityData");

            migrationBuilder.DropTable(
                name: "CaffeineDrinks");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Sleep");

            migrationBuilder.DropTable(
                name: "WeatherData");

            migrationBuilder.DropTable(
                name: "WellnessCheckIns");

            migrationBuilder.DropTable(
                name: "DayCards");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
