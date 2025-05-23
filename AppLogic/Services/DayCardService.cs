using System.Globalization;
using AppLogic.Models;
using AppLogic.Models.DTOs;
using AppLogic.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace AppLogic.Services
{
    public class DayCardService
    {

        private readonly LoggAppContext _dbContext;
        private readonly DayCardRepo _dayCardRepo;

        public DayCardService(LoggAppContext dbContext)
        {
            _dbContext = dbContext;
            _dayCardRepo = new DayCardRepo(_dbContext);
        }


        public async Task<DTO_SpecificDayCard> CreateNewDayCardAsync(int userId, DayCardInputModel dayCardInputModel)
        {
            string lat = dayCardInputModel.Lat?.ToString(CultureInfo.InvariantCulture)!;
            string lon = dayCardInputModel.Lon?.ToString(CultureInfo.InvariantCulture)!;
            string date = dayCardInputModel.Date?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)!;

            var TASK_weatherData = WeatherService.GetWeatherDataAsync(lat, lon, date)!;
            var TASK_airQualityData = AirQualityService.GetAirQualityDataAsync(lat, lon, date);

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
                CaffeineDrinksSummary = CaffeineDrinkService.ConvertToSummaryDTO(newDayCard.CaffeineDrinks!.ToList()),
                AirQualitySummary = AirQualityService.ConvertToAQDTO(newDayCard.AirQualityData!),
                PollenSummary = AirQualityService.ConvertToPollenDTO(newDayCard.AirQualityData!),
                WeatherSummary = WeatherService.ConvertToDTO(newDayCard.WeatherData!)
            };
        }





        public async Task<List<DTO_AllDayCard>?> ReadAllDayCardsAsync(int userId)
        {
            List<DayCard>? dayCards = await _dayCardRepo.ReadAllAsync(userId);

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
            DayCard? dayCard = await _dayCardRepo.ReadSingleAsync(id, userId);

            return new DTO_SpecificDayCard
            {
                DayCardId = dayCard!.Id,
                UserId = dayCard.UserId!,
                Date = dayCard.Date,
                CaffeineDrinksSummary = CaffeineDrinkService.ConvertToSummaryDTO(dayCard.CaffeineDrinks!.ToList()),
                AirQualitySummary = AirQualityService.ConvertToAQDTO(dayCard.AirQualityData!),
                PollenSummary = AirQualityService.ConvertToPollenDTO(dayCard.AirQualityData!),
                WeatherSummary = WeatherService.ConvertToDTO(dayCard.WeatherData!)
            };

        }

        public async Task<DTO_SpecificDayCard?> ReadSingleDayCardAsync(DateOnly date, int userId)
        {
            DayCard? dayCard = await _dayCardRepo.ReadSingleAsync(date, userId);

            return new DTO_SpecificDayCard
            {
                DayCardId = dayCard!.Id,
                UserId = dayCard.UserId,
                Date = dayCard.Date,
                CaffeineDrinksSummary = CaffeineDrinkService.ConvertToSummaryDTO(dayCard.CaffeineDrinks!.ToList()),
                AirQualitySummary = AirQualityService.ConvertToAQDTO(dayCard.AirQualityData!),
                PollenSummary = AirQualityService.ConvertToPollenDTO(dayCard.AirQualityData!),
                WeatherSummary = WeatherService.ConvertToDTO(dayCard.WeatherData!)
            };
        }

    }
}
