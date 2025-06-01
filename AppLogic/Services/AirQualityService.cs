using System.Text.Json;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Entities.WeatherAndAQI;
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

    }
}
