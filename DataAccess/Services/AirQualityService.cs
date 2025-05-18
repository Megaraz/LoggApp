using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AppLogic.Models.Weather.AirQuality;
using AppLogic.Repositories;
using BusinessLogic.Models.Weather;
using DataAccess.Repositories;

namespace AppLogic.Services
{
    public static class AirQualityService
    {
        public static async Task<AirQuality> GetAirQualityDataAsync(string lat, string lon, string date)
        {
            var airQualityResultString = AirQualityRepository.GetAirQualityDataAsync(lat, lon, date);

            return JsonSerializer.Deserialize<AirQuality>(await airQualityResultString)!;

        }
    }
}
