using System.Globalization;
using AppLogic.Models;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;
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
        private readonly OpenAiResponseClient _aiClient;

        public DayCardService(IDayCardRepo dayCardRepo, IWeatherService weatherService, IAirQualityService airQualityService, ICaffeineDrinkService caffeineDrinkService, OpenAiResponseClient aiClient)
        {
            _dayCardRepo = dayCardRepo;
            _weatherService = weatherService;
            _airQualityService = airQualityService;
            _caffeineDrinkService = caffeineDrinkService;
            _aiClient = aiClient;
        }

        public async Task<DayCardDetailed> CreateNewDayCardAsync(int userId, DayCardInputModel dayCardInputModel)
        {
            string lat = dayCardInputModel.Lat?.ToString(CultureInfo.InvariantCulture)!;
            string lon = dayCardInputModel.Lon?.ToString(CultureInfo.InvariantCulture)!;
            string date = dayCardInputModel.Date?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)!;

            DayCard newDayCard = new DayCard()
            {
                Date = DateOnly.Parse(date),
                UserId = userId,

            };

            var weather = await _weatherService.GetWeatherDataAsync(lat, lon, date);
            var airQuality = await _airQualityService.GetAirQualityDataAsync(lat, lon, date);

            weather.AISummary = await _aiClient.GenerateSummaryAsync(AiPromptBuilder.BuildWeatherPrompt(weather));
            airQuality.AQI_AISummary = await _aiClient.GenerateSummaryAsync(AiPromptBuilder.BuildAirQualityPrompt(airQuality));
            airQuality.Pollen_AISummary = await _aiClient.GenerateSummaryAsync(AiPromptBuilder.BuildPollenPrompt(airQuality));

            newDayCard.AirQualityData = airQuality;
            newDayCard.WeatherData = weather;

            newDayCard = await _dayCardRepo.CreateAsync(newDayCard);

            return new DayCardDetailed
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

        public async Task<bool> DeleteDayCardAsync(int dayCardId)
        {
            return await _dayCardRepo.DeleteAsync(dayCardId);
        }

        public async Task<List<DayCardSummary>?> ReadAllDayCardsAsync(int userId)
        {
            List<DayCard>? dayCards = await _dayCardRepo.GetAllDayCardsIncludeAsync(userId);

            if (dayCards == null) return null;

            List<DayCardSummary> DTO_AllDayCards = new List<DayCardSummary>();

            foreach (var dayCard in dayCards)
            {
                DTO_AllDayCards.Add
                    (
                        new DayCardSummary()
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

        public async Task<DayCardDetailed?> ReadSingleDayCardAsync(int id, int userId)
        {
            DayCard? dayCard = await _dayCardRepo.GetDayCardByIdIncludeAsync(id, userId);

            return new DayCardDetailed
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

        public async Task<DayCardDetailed?> ReadSingleDayCardAsync(DateOnly date, int userId)
        {
            DayCard? dayCard = await _dayCardRepo.GetDayCardByDateIncludeAsync(date, userId);

            return new DayCardDetailed
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
