using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppLogic.Migrations
{
    /// <inheritdoc />
    public partial class Init_test : Migration
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
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayCards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayCardId = table.Column<int>(type: "int", nullable: true),
                    TimeOf = table.Column<TimeOnly>(type: "time", nullable: true),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_DayCards_DayCardId",
                        column: x => x.DayCardId,
                        principalTable: "DayCards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CaffeineDrinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayCardId = table.Column<int>(type: "int", nullable: true),
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayCardId = table.Column<int>(type: "int", nullable: true),
                    TimeOf = table.Column<TimeOnly>(type: "time", nullable: true),
                    EstimatedKcal = table.Column<int>(type: "int", nullable: true),
                    EstimatedProteinInGrams = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foods_DayCards_DayCardId",
                        column: x => x.DayCardId,
                        principalTable: "DayCards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayCardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medications_DayCards_DayCardId",
                        column: x => x.DayCardId,
                        principalTable: "DayCards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Supplements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayCardId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeOf = table.Column<TimeOnly>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supplements_DayCards_DayCardId",
                        column: x => x.DayCardId,
                        principalTable: "DayCards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WeatherData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayCardId = table.Column<int>(type: "int", nullable: true),
                    TimeOf = table.Column<TimeOnly>(type: "time", nullable: true),
                    Units = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: true),
                    Lon = table.Column<double>(type: "float", nullable: true),
                    Temperature_Marker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temperature_Min = table.Column<double>(type: "float", nullable: true),
                    Temperature_Max = table.Column<double>(type: "float", nullable: true),
                    Temperature_Afternoon = table.Column<double>(type: "float", nullable: true),
                    Temperature_Night = table.Column<double>(type: "float", nullable: true),
                    Temperature_Evening = table.Column<double>(type: "float", nullable: true),
                    Temperature_Morning = table.Column<double>(type: "float", nullable: true),
                    Pressure_Marker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pressure_Afternoon = table.Column<int>(type: "int", nullable: true),
                    Humidity_Marker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Humidity_Afternoon = table.Column<int>(type: "int", nullable: true),
                    Precipitation_Marker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Precipitation_Total = table.Column<double>(type: "float", nullable: true),
                    CloudCover_Marker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CloudCover_Afternoon = table.Column<int>(type: "int", nullable: true),
                    AQI = table.Column<int>(type: "int", nullable: true),
                    UVI = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherData_DayCards_DayCardId",
                        column: x => x.DayCardId,
                        principalTable: "DayCards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ExerciseType = table.Column<int>(type: "int", nullable: true),
                    PerceivedIntensity = table.Column<int>(type: "int", nullable: true),
                    TrainingLoad = table.Column<int>(type: "int", nullable: true),
                    AvgHeartRate = table.Column<int>(type: "int", nullable: true),
                    Intensity = table.Column<int>(type: "int", nullable: true),
                    ActiveKcalBurned = table.Column<int>(type: "int", nullable: true),
                    Distance = table.Column<int>(type: "int", nullable: true),
                    AvgKmTempo = table.Column<int>(type: "int", nullable: true),
                    Steps = table.Column<int>(type: "int", nullable: true),
                    AvgStepLength = table.Column<int>(type: "int", nullable: true),
                    AvgStepPerMin = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_Activities_Id",
                        column: x => x.Id,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplementIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplementId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DosageInMg = table.Column<int>(type: "int", nullable: true),
                    PercentageOfDRI = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplementIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplementIngredients_Supplements_SupplementId",
                        column: x => x.SupplementId,
                        principalTable: "Supplements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_DayCardId",
                table: "Activities",
                column: "DayCardId");

            migrationBuilder.CreateIndex(
                name: "IX_CaffeineDrinks_DayCardId",
                table: "CaffeineDrinks",
                column: "DayCardId");

            migrationBuilder.CreateIndex(
                name: "IX_DayCards_UserId",
                table: "DayCards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_DayCardId",
                table: "Foods",
                column: "DayCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_DayCardId",
                table: "Medications",
                column: "DayCardId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplementIngredients_SupplementId",
                table: "SupplementIngredients",
                column: "SupplementId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplements_DayCardId",
                table: "Supplements",
                column: "DayCardId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherData_DayCardId",
                table: "WeatherData",
                column: "DayCardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaffeineDrinks");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "SupplementIngredients");

            migrationBuilder.DropTable(
                name: "WeatherData");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Supplements");

            migrationBuilder.DropTable(
                name: "DayCards");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
