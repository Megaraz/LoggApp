using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AppLogic.Models.DTOs;
using AppLogic.Models.Weather.AirQuality;
using AppLogic.Repositories;
using BusinessLogic.Models.Weather;
using DataAccess.Repositories;

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


            return new DTO_AllAirQualities()
            {
                HourlyAirQualityData = block.Time
                    .Select((time, i) => new HourlyAirQualityData()
                    {
                        Time = time.Hour,
                        BirchPollen = block.BirchPollen.ElementAtOrDefault(i),
                        AlderPollen = block.AlderPollen.ElementAtOrDefault(i),
                        GrassPollen = block.GrassPollen.ElementAtOrDefault(i),
                        MugwortPollen = block.MugwortPollen.ElementAtOrDefault(i),
                        RagweedPollen = block.RagweedPollen.ElementAtOrDefault(i),
                        UVI = block.UVI.ElementAtOrDefault(i),
                        AQI = block.AQI.ElementAtOrDefault(i),
                        PM25 = block.PM25.ElementAtOrDefault(i),
                        Ozone = block.Ozone.ElementAtOrDefault(i),
                        CarbonMonoxide = block.CarbonMonoxide.ElementAtOrDefault(i),
                        NitrogenDioxide = block.NitrogenDioxide.ElementAtOrDefault(i),
                        Dust = block.Dust.ElementAtOrDefault(i)
                    }).
                    ToList()

            };

        }
    }
}
