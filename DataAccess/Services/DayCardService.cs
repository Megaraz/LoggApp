using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AppLogic.DTOs;
using AppLogic.Models;
using AppLogic.Models.Weather.AirQuality;
using AppLogic.Repositories;
using BusinessLogic.Models;
using BusinessLogic.Models.Weather;
using DataAccess.Repositories;

namespace AppLogic.Services
{
    public static class DayCardService
    {

        public static async Task CreateNewDayCardAsync(DayCardInputModel dayCardInputModel)
        {
            DayCard newDayCard = new DayCard()
            {
                Date = (DateOnly)dayCardInputModel.Date!,
                UserId = dayCardInputModel.UserId,

            };

            string lat = dayCardInputModel.Lat?.ToString(CultureInfo.InvariantCulture)!;
            string lon = dayCardInputModel.Lon?.ToString(CultureInfo.InvariantCulture)!;
            string date = dayCardInputModel.Date?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)!;


            //Task<string> Task_WeatherResultString = new WeatherRepo().GetWeatherDataAsync(lat, lon, date);
            //Task<string> Task_AirQualityResultString = new AirQualityRepository().GetAirQualityDataAsync(lat, lon, date);

            var weatherResultString = await new WeatherRepo().GetWeatherDataAsync(lat, lon, date);
            var airQualityResultString = await new AirQualityRepository().GetAirQualityDataAsync(lat, lon, date);

            newDayCard.WeatherData = new List<WeatherData>()
            {
                JsonSerializer.Deserialize<WeatherData>(weatherResultString)!

            };
            newDayCard.AirQualities = new List<AirQuality>()
            {
                JsonSerializer.Deserialize<AirQuality>(airQualityResultString)!

            };

            DayCardRepo.Create(newDayCard);
        }

        public static async Task<List<AllDayCardsMenuDto>> ReadAllDayCardsAsync(int userId)
        {
            var dayCardsTask = await DayCardRepo.ReadAllAsync(userId);
            return dayCardsTask.OrderBy(x => x.DayCardId).ToList();
        }

        public static async Task<SpecificDayCardMenuDto>? ReadSingleDayCardAsync(int id, int userId)
        {
            var dayCardTask = await DayCardRepo.ReadSingleAsync(id, userId);
            return dayCardTask!;
        }

        public static async Task<SpecificDayCardMenuDto>? ReadSingleDayCardAsync(DateOnly date, int userId)
        {
            var dayCardTask = await DayCardRepo.ReadSingleAsync(date, userId);
            return dayCardTask!;
        }
    }
}
