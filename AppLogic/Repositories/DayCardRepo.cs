using AppLogic.Models;
using AppLogic.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AppLogic.Repositories
{
    public class DayCardRepo : GenericRepo<DayCard>, IDayCardRepo
    {

        private readonly LoggAppContext _dbContext;

        public DayCardRepo(LoggAppContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<DayCard?> GetDayCardByDateIncludeAsync(DateOnly date, int userId)
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
        public async Task<DayCard?> GetDayCardByIdIncludeAsync(int id, int userId)
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

        public async Task<List<DayCard>?> GetAllDayCardsIncludeAsync(int userId)
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
