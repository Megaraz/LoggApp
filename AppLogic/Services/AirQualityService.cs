using System.Text.Json;
using AppLogic.Models.DTOs;
using AppLogic.Models.Weather;
using AppLogic.Models.Weather.AirQuality;
using AppLogic.Repositories;
using AppLogic.Repositories.Interfaces;
using AppLogic.Services.Interfaces;

namespace AppLogic.Services
{
    public class AirQualityService : IAirQualityService
    {
        private readonly IAirQualityRepo _airQualityRepo;
        public AirQualityService(IAirQualityRepo airQualityRepo)
        {
            _airQualityRepo = airQualityRepo;
        }

        public async Task<AirQualityData> GetAirQualityDataAsync(string lat, string lon, string date)
        {
            var airQualityResultString = _airQualityRepo.GetAirQualityDataAsync(lat, lon, date);

            return JsonSerializer.Deserialize<AirQualityData>(await airQualityResultString)!;

        }

        public DTO_AllPollenData ConvertToPollenDTO(AirQualityData airQuality)
        {

            var block = airQuality.HourlyBlock ?? throw new ArgumentNullException(nameof(airQuality));
            var units = airQuality.HourlyUnits ?? throw new ArgumentNullException(nameof(airQuality));

            return new DTO_AllPollenData()
            {
                HourlyPollenData = block.Time
                .Select((time, i) => new HourlyPollenData()
                {
                    Time = time.Hour,

                    Birch = new Measurement<double?>
                    {
                        Value = block.BirchPollen.ElementAtOrDefault(i),
                        Unit = units.BirchPollen
                    },
                    Alder = new Measurement<double?>
                    {
                        Value = block.AlderPollen.ElementAtOrDefault(i),
                        Unit = units.AlderPollen
                    },
                    Grass = new Measurement<double?>
                    {
                        Value = block.GrassPollen.ElementAtOrDefault(i),
                        Unit = units.GrassPollen
                    },
                    Mugwort = new Measurement<double?>
                    {
                        Value = block.MugwortPollen.ElementAtOrDefault(i),
                        Unit = units.MugwortPollen
                    },
                    Ragweed = new Measurement<double?>
                    {
                        Value = block.RagweedPollen.ElementAtOrDefault(i),
                        Unit = units.RagweedPollen
                    }
                }).ToList()
            };
        }

        public DTO_AllAirQualityData ConvertToAQDTO(AirQualityData airQuality)
        {

            var block = airQuality.HourlyBlock ?? throw new ArgumentNullException(nameof(airQuality));
            var units = airQuality.HourlyUnits ?? throw new ArgumentNullException(nameof(airQuality));

            return new DTO_AllAirQualityData()
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
                        CO = new Measurement<double?>
                        {
                            Value = block.CarbonMonoxide.ElementAtOrDefault(i),
                            Unit = units.CarbonMonoxide
                        },
                        NO2 = new Measurement<double?>
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
            };

        }
    }
}
