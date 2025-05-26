using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppLogic.Migrations
{
    /// <inheritdoc />
    public partial class Sleep_WellnessCheckIn_Exercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_DayCards_DayCardId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_AirQualityData_DayCards_DayCardId",
                table: "AirQualityData");

            migrationBuilder.DropForeignKey(
                name: "FK_DayCards_Users_UserId",
                table: "DayCards");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplementIngredients_Supplements_SupplementId",
                table: "SupplementIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplements_DayCards_DayCardId",
                table: "Supplements");

            migrationBuilder.DropForeignKey(
                name: "FK_WeatherData_DayCards_DayCardId",
                table: "WeatherData");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropIndex(
                name: "IX_WeatherData_DayCardId",
                table: "WeatherData");

            migrationBuilder.DropIndex(
                name: "IX_AirQualityData_DayCardId",
                table: "AirQualityData");

            migrationBuilder.AlterColumn<int>(
                name: "DayCardId",
                table: "WeatherData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DayCardId",
                table: "Supplements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SupplementId",
                table: "SupplementIngredients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "DayCards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DayCardId",
                table: "AirQualityData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DayCardId",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Sleep",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayCardId = table.Column<int>(type: "int", nullable: false),
                    SleepStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SleepEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                name: "IX_WeatherData_DayCardId",
                table: "WeatherData",
                column: "DayCardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AirQualityData_DayCardId",
                table: "AirQualityData",
                column: "DayCardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sleep_DayCardId",
                table: "Sleep",
                column: "DayCardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WellnessCheckIns_DayCardId",
                table: "WellnessCheckIns",
                column: "DayCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_DayCards_DayCardId",
                table: "Activities",
                column: "DayCardId",
                principalTable: "DayCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AirQualityData_DayCards_DayCardId",
                table: "AirQualityData",
                column: "DayCardId",
                principalTable: "DayCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DayCards_Users_UserId",
                table: "DayCards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplementIngredients_Supplements_SupplementId",
                table: "SupplementIngredients",
                column: "SupplementId",
                principalTable: "Supplements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplements_DayCards_DayCardId",
                table: "Supplements",
                column: "DayCardId",
                principalTable: "DayCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherData_DayCards_DayCardId",
                table: "WeatherData",
                column: "DayCardId",
                principalTable: "DayCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_DayCards_DayCardId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_AirQualityData_DayCards_DayCardId",
                table: "AirQualityData");

            migrationBuilder.DropForeignKey(
                name: "FK_DayCards_Users_UserId",
                table: "DayCards");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplementIngredients_Supplements_SupplementId",
                table: "SupplementIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplements_DayCards_DayCardId",
                table: "Supplements");

            migrationBuilder.DropForeignKey(
                name: "FK_WeatherData_DayCards_DayCardId",
                table: "WeatherData");

            migrationBuilder.DropTable(
                name: "Sleep");

            migrationBuilder.DropTable(
                name: "WellnessCheckIns");

            migrationBuilder.DropIndex(
                name: "IX_WeatherData_DayCardId",
                table: "WeatherData");

            migrationBuilder.DropIndex(
                name: "IX_AirQualityData_DayCardId",
                table: "AirQualityData");

            migrationBuilder.AlterColumn<int>(
                name: "DayCardId",
                table: "WeatherData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DayCardId",
                table: "Supplements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SupplementId",
                table: "SupplementIngredients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "DayCards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DayCardId",
                table: "AirQualityData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DayCardId",
                table: "Activities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayCardId = table.Column<int>(type: "int", nullable: true),
                    EstimatedKcal = table.Column<int>(type: "int", nullable: true),
                    EstimatedProteinInGrams = table.Column<int>(type: "int", nullable: true),
                    TimeOf = table.Column<TimeOnly>(type: "time", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_WeatherData_DayCardId",
                table: "WeatherData",
                column: "DayCardId",
                unique: true,
                filter: "[DayCardId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AirQualityData_DayCardId",
                table: "AirQualityData",
                column: "DayCardId",
                unique: true,
                filter: "[DayCardId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_DayCardId",
                table: "Foods",
                column: "DayCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_DayCardId",
                table: "Medications",
                column: "DayCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_DayCards_DayCardId",
                table: "Activities",
                column: "DayCardId",
                principalTable: "DayCards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AirQualityData_DayCards_DayCardId",
                table: "AirQualityData",
                column: "DayCardId",
                principalTable: "DayCards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DayCards_Users_UserId",
                table: "DayCards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplementIngredients_Supplements_SupplementId",
                table: "SupplementIngredients",
                column: "SupplementId",
                principalTable: "Supplements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Supplements_DayCards_DayCardId",
                table: "Supplements",
                column: "DayCardId",
                principalTable: "DayCards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherData_DayCards_DayCardId",
                table: "WeatherData",
                column: "DayCardId",
                principalTable: "DayCards",
                principalColumn: "Id");
        }
    }
}
