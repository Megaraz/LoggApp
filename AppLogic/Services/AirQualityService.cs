using System.Text.Json;
using AppLogic.Migrations;
using AppLogic.Models.DTOs;
using AppLogic.Models.Weather;
using AppLogic.Models.Weather.AirQuality;
using AppLogic.Repositories;

namespace AppLogic.Services
{
    public static class AirQualityService
    {
        public static async Task<AirQualityData> GetAirQualityDataAsync(string lat, string lon, string date)
        {
            var airQualityResultString = AirQualityRepository.GetAirQualityDataAsync(lat, lon, date);

            return JsonSerializer.Deserialize<AirQualityData>(await airQualityResultString)!;

        }

        public static DTO_AllAirQualities ConvertToDTO(AirQualityData airQuality)
        {

            var block = airQuality.HourlyBlock ?? throw new ArgumentNullException(nameof(airQuality));
            var units = airQuality.HourlyUnits ?? throw new ArgumentNullException(nameof(airQuality));

            return new DTO_AllAirQualities()
            {
                HourlyAirQualityData = block.Time
                    .Select((time, i) => new HourlyAirQualityData()
                    {
                        Time = time.Hour,

                        UVI = new Measurement<double?>
                        {
                            Value = block.UVI.ElementAtOrDefault(i),
                            Unit = units.UVI
                        },
                        AQI = new Measurement<double?>
                        {
                            Value = block.AQI.ElementAtOrDefault(i),
                            Unit = units.AQI
                        },
                        PM25 = new Measurement<double?>
                        {
                            Value = block.PM25.ElementAtOrDefault(i),
                            Unit = units.PM25
                        },
                        Ozone = new Measurement<double?>
                        {
                            Value = block.Ozone.ElementAtOrDefault(i),
                            Unit = units.Ozone
                        },
                        CarbonMonoxide = new Measurement<double?>
                        {
                            Value = block.CarbonMonoxide.ElementAtOrDefault(i),
                            Unit = units.CarbonMonoxide
                        },
                        NitrogenDioxide = new Measurement<double?>
                        {
                            Value = block.NitrogenDioxide.ElementAtOrDefault(i),
                            Unit = units.NitrogenDioxide
                        },
                        Dust = new Measurement<double?>
                        {
                            Value = block.Dust.ElementAtOrDefault(i),
                            Unit = units.Dust
                        }

                    }).
                    ToList(),

                HourlyPollenData = block.Time
                .Select((time, i) => new HourlyPollenData()
                {
                    Time = time.Hour,

                    BirchPollen = new Measurement<double?>
                    {
                        Value = block.BirchPollen.ElementAtOrDefault(i),
                        Unit = units.BirchPollen
                    },
                    AlderPollen = new Measurement<double?>
                    {
                        Value = block.AlderPollen.ElementAtOrDefault(i),
                        Unit = units.AlderPollen
                    },
                    GrassPollen = new Measurement<double?>
                    {
                        Value = block.GrassPollen.ElementAtOrDefault(i),
                        Unit = units.GrassPollen
                    },
                    MugwortPollen = new Measurement<double?>
                    {
                        Value = block.MugwortPollen.ElementAtOrDefault(i),
                        Unit = units.MugwortPollen
                    },
                    RagweedPollen = new Measurement<double?>
                    {
                        Value = block.RagweedPollen.ElementAtOrDefault(i),
                        Unit = units.RagweedPollen
                    }
                }).ToList()



            };

        }
    }
}
