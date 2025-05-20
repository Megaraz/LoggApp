using AppLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace AppLogic.Repositories
{
    public class DayCardRepo
    {

        private readonly LoggAppContext _dbContext;

        public DayCardRepo(LoggAppContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<DayCard> Create(DayCard newDayCard)
        {
            try
            {
                await _dbContext.DayCards.AddAsync(newDayCard);
                await _dbContext.SaveChangesAsync();
                return newDayCard;
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Something went wrong, {e.Message}");

            }
        }

        //public async Task<DTO_SpecificDayCard?> ReadSingleAsync(DateOnly date, int userId)
        //{
        //    using (var db = new LoggAppContext())
        //    {
        //        try
        //        {
        //            var dayCard = await db.DayCards
        //                .Include(d => d.WeatherData)
        //                .Include(d => d.AirQualities)
        //                .SingleOrDefaultAsync(d => d.Date == date && d.UserId == userId);

        //            if (dayCard == null) return null;

        //            return new DTO_SpecificDayCard
        //            {
        //                DayCardId = dayCard.Id,
        //                UserId = dayCard.UserId,
        //                Date = dayCard.Date,
        //                AirQualitySummary = dayCard.AirQualities?
        //                    .Select(a => new DTO_AllAirQualities
        //                    {
        //                        AirQualityId = a.Id,
        //                        DayCardId = a.DayCardId,
        //                        MaxAqi = a.HourlyBlock?.AQI?.Max(),
        //                        MaxBirchPollen = a.HourlyBlock?.BirchPollen?.Max()
        //                    })
        //                    .FirstOrDefault(),

        //                WeatherSummary = dayCard.WeatherData?
        //                .Select(w => new DTO_AllWeatherData
        //                {
        //                    WeatherDataId = w.Id,
        //                    DayCardId = w.DayCardId,
        //                    MaxTemp = w.HourlyBlock?.Temperature2m?.Max(),
        //                    MaxPrecipitation = w.HourlyBlock?.Precipitation?.Max()
        //                })
        //                .FirstOrDefault()

        //            };
        //        }
        //        catch (Exception e)
        //        {
        //            throw new ArgumentException($"Something went wrong, {e.Message}");

        //        }
        //    }
        //}

        public async Task<DayCard?> ReadSingleAsync(DateOnly date, int userId)
        {

            try
            {
                return await _dbContext.DayCards
                    .Include(dc => dc.Activities)
                    .Include(dc => dc.CaffeineDrinks)
                    .Include(dc => dc.Supplements)
                    .Include(dc => dc.WeatherData)
                    .Include(dc => dc.AirQualityData)
                    .AsSplitQuery()
                    .SingleOrDefaultAsync(dc => dc.Date == date && dc.UserId == userId);


            }
            catch (Exception e)
            {
                throw new ArgumentException($"Something went wrong, {e.Message}");

            }
        }
        public async Task<DayCard?> ReadSingleAsync(int id, int userId)
        {

            try
            {
                return await _dbContext.DayCards
                    .Include(dc => dc.Activities)
                    .Include(dc => dc.CaffeineDrinks)
                    .Include(dc => dc.Supplements)
                    .Include(dc => dc.WeatherData)
                    .Include(dc => dc.AirQualityData)
                    .AsSplitQuery()
                    .SingleOrDefaultAsync(dc => dc.Id == id && dc.UserId == userId);


            }
            catch (Exception e)
            {
                throw new ArgumentException($"Something went wrong, {e.Message}");

            }
        }
        //public async Task<DTO_SpecificDayCardMenu?> ReadSingleAsync(int id, int userId)
        //{
        //    using (var db = new LoggAppContext())
        //    {
        //        try
        //        {
        //            var dayCard = await db.DayCards
        //                .Include(d => d.WeatherData)
        //            .Include(d => d.AirQualities)
        //                .SingleOrDefaultAsync(d => d.Id == id && d.UserId == userId);

        //            if (dayCard == null) return null;

        //            return new DTO_SpecificDayCardMenu
        //            {
        //                DayCardId = dayCard.Id,
        //                UserId = dayCard.UserId,
        //                Date = dayCard.Date,
        //                AirQualitySummary = dayCard.AirQualities?
        //                    .Select(a => new DTO_AllAirQualitiesMenu
        //                    {
        //                        AirQualityId = a.Id,
        //                        DayCardId = a.DayCardId,
        //                        MaxAqi = a.HourlyBlock?.AQI?.Max(),
        //                        MaxBirchPollen = a.HourlyBlock?.BirchPollen?.Max()
        //                    })
        //                    .FirstOrDefault(),

        //                WeatherSummary = dayCard.WeatherData?
        //                    .Select(w => new DTO_AllWeatherDataMenu
        //                    {
        //                        WeatherDataId = w.Id,
        //                        DayCardId = w.DayCardId,
        //                        MaxTemp = w.HourlyBlock?.Temperature2m?.Max(),
        //                        MaxPrecipitation = w.HourlyBlock?.Precipitation?.Max()
        //                    })
        //                    .FirstOrDefault()

        //            };
        //        }
        //        catch (Exception e)
        //        {
        //            throw new ArgumentException($"Something went wrong, {e.Message}");

        //        }
        //    }
        //}

        public async Task<List<DayCard>?> ReadAllAsync(int userId)
        {

            try
            {
                return await _dbContext.DayCards
                    .Include(dc => dc.Activities)
                    .Include(dc => dc.CaffeineDrinks)
                    .Include(dc => dc.Supplements)
                    .Include(dc => dc.WeatherData)
                    .Include(dc => dc.AirQualityData)
                    .AsSplitQuery()
                    .Where(dc => dc.UserId == userId)
                    .ToListAsync();


            }
            catch (Exception e)
            {
                throw new ArgumentException($"Something went wrong, {e.Message}");
            }
        }
    }
}
