using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppLogic.Migrations
{
    /// <inheritdoc />
    public partial class Delete_On_Cascade_And_Fixed_FK_Nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirQualityData_DayCards_DayCardId",
                table: "AirQualityData");

            migrationBuilder.DropForeignKey(
                name: "FK_CaffeineDrinks_DayCards_DayCardId",
                table: "CaffeineDrinks");

            migrationBuilder.DropForeignKey(
                name: "FK_WeatherData_DayCards_DayCardId",
                table: "WeatherData");

            migrationBuilder.DropIndex(
                name: "IX_WeatherData_DayCardId",
                table: "WeatherData");

            migrationBuilder.DropIndex(
                name: "IX_AirQualityData_DayCardId",
                table: "AirQualityData");

            migrationBuilder.DropColumn(
                name: "TimeOf",
                table: "WeatherData");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TimeOf",
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
                table: "CaffeineDrinks",
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
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AirQualityData_DayCards_DayCardId",
                table: "AirQualityData",
                column: "DayCardId",
                principalTable: "DayCards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CaffeineDrinks_DayCards_DayCardId",
                table: "CaffeineDrinks",
                column: "DayCardId",
                principalTable: "DayCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherData_DayCards_DayCardId",
                table: "WeatherData",
                column: "DayCardId",
                principalTable: "DayCards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirQualityData_DayCards_DayCardId",
                table: "AirQualityData");

            migrationBuilder.DropForeignKey(
                name: "FK_CaffeineDrinks_DayCards_DayCardId",
                table: "CaffeineDrinks");

            migrationBuilder.DropForeignKey(
                name: "FK_WeatherData_DayCards_DayCardId",
                table: "WeatherData");

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

            migrationBuilder.AddColumn<TimeOnly>(
                name: "TimeOf",
                table: "WeatherData",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DayCardId",
                table: "CaffeineDrinks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DayCardId",
                table: "AirQualityData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "TimeOf",
                table: "AirQualityData",
                type: "time",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_AirQualityData_DayCards_DayCardId",
                table: "AirQualityData",
                column: "DayCardId",
                principalTable: "DayCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CaffeineDrinks_DayCards_DayCardId",
                table: "CaffeineDrinks",
                column: "DayCardId",
                principalTable: "DayCards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherData_DayCards_DayCardId",
                table: "WeatherData",
                column: "DayCardId",
                principalTable: "DayCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
