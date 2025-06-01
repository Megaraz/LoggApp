using System.Globalization;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Entities;
using AppLogic.Models.InputModels;
using AppLogic.Repositories;
using AppLogic.Repositories.Interfaces;
using AppLogic.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace AppLogic.Services
{
    /// <summary>
    /// Service for managing day cards, including creating, reading, updating, and deleting day cards.
    /// </summary>
    public class DayCardService : IDayCardService
    {
        #region FIELDS
        private readonly IDayCardRepo _dayCardRepo;
        private readonly IWeatherService _weatherService;
        private readonly IAirQualityService _airQualityService;
        private readonly OpenAiResponseClient _aiClient;
        #endregion

        public DayCardService(IDayCardRepo dayCardRepo, IWeatherService weatherService, IAirQualityService airQualityService, OpenAiResponseClient aiClient)
        {
            _dayCardRepo = dayCardRepo;
            _weatherService = weatherService;
            _airQualityService = airQualityService;
            _aiClient = aiClient;
        }

        public async Task<DayCardDetailed> CreateNewDayCardAsync(int userId, DayCardInputModel dayCardInputModel)
        {
            DayCard newDayCard = await GenerateDayCard(userId, dayCardInputModel);

            newDayCard = await _dayCardRepo.CreateAsync(newDayCard);
            return MapToDtoDetailed(newDayCard);
        }

        private DayCardDetailed MapToDtoDetailed(DayCard dayCard)
        {
            DayCardDetailed dayCardDetailed = new(dayCard);

            dayCardDetailed.UpdateTotalValues();

            return dayCardDetailed;
        }

        private async Task<DayCard> GenerateDayCard(int userId, DayCardInputModel dayCardInputModel)
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
            return newDayCard;
        }

        public async Task<bool> DeleteDayCardAsync(int dayCardId)
        {
            return await _dayCardRepo.DeleteAsync(dayCardId);
        }

        public async Task<List<DayCardSummary>?> ReadAllDayCardsAsync(int userId)
        {
            List<DayCard>? dayCards = await _dayCardRepo.GetAllDayCardsIncludeAsync(userId);

            if (dayCards == null) return null;

            List<DayCardSummary> AllDayCardsSummary = dayCards.Select(dayCard => new DayCardSummary(dayCard)).ToList();

            return AllDayCardsSummary.OrderBy(x => x.Date).ToList();
        }

        public async Task<DayCardDetailed?> ReadSingleDayCardAsync(int id, int userId)
        {
            DayCard? dayCard = await _dayCardRepo.GetDayCardByIdIncludeAsync(id, userId);

            return dayCard == null ? null : MapToDtoDetailed(dayCard);

        }

        public async Task<DayCardDetailed?> ReadSingleDayCardAsync(DateOnly date, int userId)
        {
            DayCard? dayCard = await _dayCardRepo.GetDayCardByDateIncludeAsync(date, userId);

            return dayCard == null ? null : MapToDtoDetailed(dayCard);

        }

        public async Task<DayCardDetailed> UpdateDayCardDateAsync(int dayCardId, DayCardInputModel dayCardInputModel)
        {
            DayCard dayCard = await GenerateDayCard(dayCardInputModel.UserId, dayCardInputModel);

            dayCard.Id = dayCardId;

            dayCard = await _dayCardRepo.UpdateDayCardAsync(dayCard);

            return MapToDtoDetailed(dayCard);
        }
    }
}
