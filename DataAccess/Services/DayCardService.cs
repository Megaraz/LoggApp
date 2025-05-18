using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AppLogic.Models;
using AppLogic.Models.DTOs;
using AppLogic.Models.Weather.AirQuality;
using AppLogic.Repositories;
using BusinessLogic.Models;
using BusinessLogic.Models.Weather;
using DataAccess;
using DataAccess.Repositories;
using Presentation;
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


        public async Task<DTO_SpecificDayCard> CreateNewDayCardAsync(DayCardInputModel dayCardInputModel)
        {
            string lat = dayCardInputModel.Lat?.ToString(CultureInfo.InvariantCulture)!;
            string lon = dayCardInputModel.Lon?.ToString(CultureInfo.InvariantCulture)!;
            string date = dayCardInputModel.Date?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)!;

            var TASK_weatherData = WeatherService.GetWeatherDataAsync(lat, lon, date)!;
            var TASK_airQualityData = AirQualityService.GetAirQualityDataAsync(lat, lon, date);

            DayCard newDayCard = new DayCard()
            {
                Date = DateOnly.Parse(date),
                UserId = dayCardInputModel.UserId,

            };

            newDayCard.WeatherData = new List<WeatherData>()
            {
                await TASK_weatherData
            };
            newDayCard.AirQualities = new List<AirQuality>()
            {
                await TASK_airQualityData
            };

            newDayCard = await _dayCardRepo.Create(newDayCard);

            return new DTO_SpecificDayCard
            {
                DayCardId = newDayCard.Id,
                UserId = newDayCard.UserId,
                Date = newDayCard.Date,
                AirQualitySummary = newDayCard.AirQualities?
                    .Select(a => new DTO_AllAirQualities
                    {
                        AirQualityId = a.Id,
                        DayCardId = a.DayCardId,
                        MaxAqi = a.HourlyBlock?.AQI?.Max(),
                        MaxBirchPollen = a.HourlyBlock?.BirchPollen?.Max()
                    })
                    .FirstOrDefault(),

                WeatherSummary = newDayCard.WeatherData?
                    .Select(w => new DTO_AllWeatherData
                    {
                        WeatherDataId = w.Id,
                        DayCardId = w.DayCardId,
                        MaxTemp = w.HourlyBlock?.Temperature2m?.Max(),
                        MaxPrecipitation = w.HourlyBlock?.Precipitation?.Max()
                    })
                    .FirstOrDefault()
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

            return DTO_AllDayCards;


            //return dayCardsTask!.OrderBy(x => x.Date).ToList();
        }

        public async Task<DTO_SpecificDayCard?> ReadSingleDayCardAsync(int id, int userId)
        {
            DayCard? dayCard = await _dayCardRepo.ReadSingleAsync(id, userId);

            return new DTO_SpecificDayCard
            {
                DayCardId = dayCard.Id,
                UserId = dayCard.UserId,
                Date = dayCard.Date,
                AirQualitySummary = dayCard.AirQualities?
                    .Select(a => new DTO_AllAirQualities
                    {
                        AirQualityId = a.Id,
                        DayCardId = a.DayCardId,
                        MaxAqi = a.HourlyBlock?.AQI?.Max(),
                        MaxBirchPollen = a.HourlyBlock?.BirchPollen?.Max()
                    })
                    .FirstOrDefault(),

                WeatherSummary = dayCard.WeatherData?
                    .Select(w => new DTO_AllWeatherData
                    {
                        WeatherDataId = w.Id,
                        DayCardId = w.DayCardId,
                        MaxTemp = w.HourlyBlock?.Temperature2m?.Max(),
                        MaxPrecipitation = w.HourlyBlock?.Precipitation?.Max()
                    })
                    .FirstOrDefault()
            };



            //return dayCardTask;
        }

        public async Task<DTO_SpecificDayCard?> ReadSingleDayCardAsync(DateOnly date, int userId)
        {
            DayCard? dayCard = await _dayCardRepo.ReadSingleAsync(date, userId);

            return new DTO_SpecificDayCard
            {
                DayCardId = dayCard!.Id,
                UserId = dayCard.UserId,
                Date = dayCard.Date,
                AirQualitySummary = dayCard.AirQualities?
                    .Select(a => new DTO_AllAirQualities
                    {
                        AirQualityId = a.Id,
                        DayCardId = a.DayCardId,
                        MaxAqi = a.HourlyBlock?.AQI?.Max(),
                        MaxBirchPollen = a.HourlyBlock?.BirchPollen?.Max()
                    })
                    .FirstOrDefault(),

                WeatherSummary = dayCard.WeatherData?
                    .Select(w => new DTO_AllWeatherData
                    {
                        WeatherDataId = w.Id,
                        DayCardId = w.DayCardId,
                        MaxTemp = w.HourlyBlock?.Temperature2m?.Max(),
                        MaxPrecipitation = w.HourlyBlock?.Precipitation?.Max()
                    })
                    .FirstOrDefault()
            };
        }




        //public async Task<DTO_SpecificDayCard> CreateNewDayCardAsync(DayCardInputModel dayCardInputModel)
        //{
        //    string lat = dayCardInputModel.Lat?.ToString(CultureInfo.InvariantCulture)!;
        //    string lon = dayCardInputModel.Lon?.ToString(CultureInfo.InvariantCulture)!;
        //    string date = dayCardInputModel.Date?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)!;

        //    var TASK_weatherData = WeatherService.GetWeatherData(lat, lon, date)!;
        //    var TASK_airQualityData = AirQualityService.GetAirQualityData(lat, lon, date);

        //    DayCard newDayCard = new DayCard()
        //    {
        //        Date = DateOnly.Parse(date),
        //        UserId = dayCardInputModel.UserId,

        //    };

        //    newDayCard.WeatherData = new List<WeatherData>()
        //    {
        //        await TASK_weatherData
        //    };
        //    newDayCard.AirQualities = new List<AirQuality>()
        //    {
        //        await TASK_airQualityData
        //    };

        //    newDayCard = await _dayCardRepo.Create(newDayCard);

        //    return new DTO_SpecificDayCard
        //    {
        //        DayCardId = newDayCard.Id,
        //        UserId = newDayCard.UserId,
        //        Date = newDayCard.Date,
        //        AirQualitySummary = newDayCard.AirQualities?
        //            .Select(a => new DTO_AllAirQualities
        //            {
        //                AirQualityId = a.Id,
        //                DayCardId = a.DayCardId,
        //                MaxAqi = a.HourlyBlock?.AQI?.Max(),
        //                MaxBirchPollen = a.HourlyBlock?.BirchPollen?.Max()
        //            })
        //            .FirstOrDefault(),

        //        WeatherSummary = newDayCard.WeatherData?
        //            .Select(w => new DTO_AllWeatherData
        //            {
        //                WeatherDataId = w.Id,
        //                DayCardId = w.DayCardId,
        //                MaxTemp = w.HourlyBlock?.Temperature2m?.Max(),
        //                MaxPrecipitation = w.HourlyBlock?.Precipitation?.Max()
        //            })
        //            .FirstOrDefault()
        //    };
        //}

        //public async Task<List<DTO_AllDayCards>> ReadAllDayCardsAsync(int userId)
        //{
        //    var dayCardsTask = await _dayCardRepo.ReadAllAsync(userId);
        //    return dayCardsTask.OrderBy(x => x.DayCardId).ToList();
        //}

        //public async Task<DTO_SpecificDayCard>? ReadSingleDayCardAsync(int id, int userId)
        //{
        //    var dayCardTask = await _dayCardRepo.ReadSingleAsync(id, userId);
        //    return dayCardTask!;
        //}

        //public async Task<DTO_SpecificDayCard>? ReadSingleDayCardAsync(DateOnly date, int userId)
        //{
        //    var dayCardTask = await _dayCardRepo.ReadSingleAsync(date, userId);
        //    return dayCardTask!;
        //}
    }
}
