using System.Globalization;
using AppLogic.Models;
using AppLogic.Models.DTOs;
using AppLogic.Repositories;
using AppLogic.Repositories.Interfaces;
using AppLogic.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace AppLogic.Services
{
    public class DayCardService : IDayCardService
    {

        private readonly IDayCardRepo _dayCardRepo;
        private readonly IWeatherService _weatherService;
        private readonly IAirQualityService _airQualityService;
        private readonly ICaffeineDrinkService _caffeineDrinkService;

        public DayCardService(IDayCardRepo dayCardRepo, IWeatherService weatherService, IAirQualityService airQualityService, ICaffeineDrinkService caffeineDrinkService)
        {
            _dayCardRepo = dayCardRepo;
            _weatherService = weatherService;
            _airQualityService = airQualityService;
            _caffeineDrinkService = caffeineDrinkService;
        }

        public async Task<DTO_SpecificDayCard> CreateNewDayCardAsync(int userId, DayCardInputModel dayCardInputModel)
        {
            string lat = dayCardInputModel.Lat?.ToString(CultureInfo.InvariantCulture)!;
            string lon = dayCardInputModel.Lon?.ToString(CultureInfo.InvariantCulture)!;
            string date = dayCardInputModel.Date?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)!;

            var TASK_weatherData = _weatherService.GetWeatherDataAsync(lat, lon, date)!;
            var TASK_airQualityData = _airQualityService.GetAirQualityDataAsync(lat, lon, date);

            DayCard newDayCard = new DayCard()
            {
                Date = DateOnly.Parse(date),
                UserId = userId,

            };

            newDayCard.WeatherData = await TASK_weatherData;
            newDayCard.AirQualityData = await TASK_airQualityData;

            newDayCard = await _dayCardRepo.CreateAsync(newDayCard);

            return new DTO_SpecificDayCard
            {
                DayCardId = newDayCard.Id,
                UserId = newDayCard.UserId,
                Date = newDayCard.Date,
                CaffeineDrinksSummary = _caffeineDrinkService.ConvertToSummaryDTO(newDayCard.CaffeineDrinks!.ToList()),
                AirQualitySummary = _airQualityService.ConvertToAQDTO(newDayCard.AirQualityData!),
                PollenSummary = _airQualityService.ConvertToPollenDTO(newDayCard.AirQualityData!),
                WeatherSummary = _weatherService.ConvertToDTO(newDayCard.WeatherData!)
            };
        }





        public async Task<List<DTO_AllDayCard>?> ReadAllDayCardsAsync(int userId)
        {
            List<DayCard>? dayCards = await _dayCardRepo.GetAllDayCardsAsync(userId);

            if (dayCards == null) return null;

            List<DTO_AllDayCard> DTO_AllDayCards = new List<DTO_AllDayCard>();

            foreach (var dayCard in dayCards)
            {
                DTO_AllDayCards.Add
                    (
                        new DTO_AllDayCard()
                        {
                            DayCardId = dayCard.Id,
                            UserId = dayCard.UserId,
                            Date = dayCard.Date,
                            Entries = (dayCard.Activities!.Count + dayCard.CaffeineDrinks!.Count + dayCard.Supplements!.Count)

                        }
                    );
            }

            return DTO_AllDayCards.OrderBy(x => x.Date).ToList();
            
        }

        public async Task<DTO_SpecificDayCard?> ReadSingleDayCardAsync(int id, int userId)
        {
            DayCard? dayCard = await _dayCardRepo.GetDayCardById(id, userId);

            return new DTO_SpecificDayCard
            {
                DayCardId = dayCard!.Id,
                UserId = dayCard.UserId!,
                Date = dayCard.Date,
                CaffeineDrinksSummary = _caffeineDrinkService.ConvertToSummaryDTO(dayCard.CaffeineDrinks!.ToList()),
                AirQualitySummary = _airQualityService.ConvertToAQDTO(dayCard.AirQualityData!),
                PollenSummary = _airQualityService.ConvertToPollenDTO(dayCard.AirQualityData!),
                WeatherSummary = _weatherService.ConvertToDTO(dayCard.WeatherData!)
            };

        }

        public async Task<DTO_SpecificDayCard?> ReadSingleDayCardAsync(DateOnly date, int userId)
        {
            DayCard? dayCard = await _dayCardRepo.GetDayCardByDate(date, userId);

            return new DTO_SpecificDayCard
            {
                DayCardId = dayCard!.Id,
                UserId = dayCard.UserId,
                Date = dayCard.Date,
                CaffeineDrinksSummary = _caffeineDrinkService.ConvertToSummaryDTO(dayCard.CaffeineDrinks!.ToList()),
                AirQualitySummary = _airQualityService.ConvertToAQDTO(dayCard.AirQualityData!),
                PollenSummary = _airQualityService.ConvertToPollenDTO(dayCard.AirQualityData!),
                WeatherSummary = _weatherService.ConvertToDTO(dayCard.WeatherData!)
            };
        }

    }
}
