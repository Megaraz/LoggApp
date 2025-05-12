using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.DTOs;
using BusinessLogic.Models;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppLogic.Repositories
{
    public class DayCardRepo
    {
        public static void Create(DayCard newDayCard)
        {
            using (var db = new LoggAppContext())
            {
                try
                {
                    db.DayCards.Add(newDayCard);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Something went wrong, {e.Message}");

                }
            }
        }

        public static async Task<SpecificDayCardMenuDto?> ReadSingleAsync(DateOnly date, int userId)
        {
            using (var db = new LoggAppContext())
            {
                try
                {
                    var dayCard = await db.DayCards
                        .Include(d => d.WeatherData)
                        .Include(d => d.AirQualities)
                        .SingleOrDefaultAsync(d => d.Date == date && d.UserId == userId);

                    if (dayCard == null) return null;

                    return new SpecificDayCardMenuDto
                    {
                        DayCardId = dayCard.Id,
                        UserId = dayCard.UserId,
                        Date = dayCard.Date,
                        AirQualitySummary = dayCard.AirQualities?
                            .Select(a => new AllAirQualitiesMenuDto
                            {
                                AirQualityId = a.Id,
                                DayCardId = a.DayCardId,
                                MaxAqi = a.HourlyBlock?.AQI?.Max(),
                                MaxBirchPollen = a.HourlyBlock?.BirchPollen?.Max()
                            })
                            .FirstOrDefault(),

                        WeatherSummary = dayCard.WeatherData?
                        .Select(w => new AllWeatherDataMenuDto
                        {
                            WeatherDataId = w.Id,
                            DayCardId = w.DayCardId,
                            MaxTemp = w.HourlyBlock?.Temperature2m?.Max(),
                            MaxPrecipitation = w.HourlyBlock?.Precipitation?.Max()
                        })
                        .FirstOrDefault()

                    };
                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Something went wrong, {e.Message}");

                }
            }
        }

        public static async Task<SpecificDayCardMenuDto?> ReadSingleAsync(int id, int userId)
        {
            using (var db = new LoggAppContext())
            {
                try
                {
                    var dayCard = await db.DayCards
                        .Include(d => d.WeatherData)
                    .Include(d => d.AirQualities)
                        .SingleOrDefaultAsync(d => d.Id == id && d.UserId == userId);

                    if (dayCard == null) return null;

                    return new SpecificDayCardMenuDto
                    {
                        DayCardId = dayCard.Id,
                        UserId = dayCard.UserId,
                        Date = dayCard.Date,
                        AirQualitySummary = dayCard.AirQualities?
                            .Select(a => new AllAirQualitiesMenuDto
                            {
                                AirQualityId = a.Id,
                                DayCardId = a.DayCardId,
                                MaxAqi = a.HourlyBlock?.AQI?.Max(),
                                MaxBirchPollen = a.HourlyBlock?.BirchPollen?.Max()
                            })
                            .FirstOrDefault(),

                        WeatherSummary = dayCard.WeatherData?
                            .Select(w => new AllWeatherDataMenuDto
                            {
                                WeatherDataId = w.Id,
                                DayCardId = w.DayCardId,
                                MaxTemp = w.HourlyBlock?.Temperature2m?.Max(),
                                MaxPrecipitation = w.HourlyBlock?.Precipitation?.Max()
                            })
                            .FirstOrDefault()

                    };
                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Something went wrong, {e.Message}");

                }
            }
        }

        internal static async Task<List<AllDayCardsMenuDto>> ReadAllAsync(int userId)
        {
            using (var db = new LoggAppContext())
            {
                try
                {
                    return await db.DayCards
                        .Where(x => x.UserId == userId)
                        .Select(x => new AllDayCardsMenuDto
                        {
                            DayCardId = x.Id,
                            Date = x.Date
                        })
                        .ToListAsync();

                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Something went wrong, {e.Message}");
                }
            }
        }
    }
}
