using AppLogic.Models.Entities;
using AppLogic.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AppLogic.Repositories
{
    /// <summary>
    /// Repository for managing day cards, providing methods to retrieve, update, and manage day card entities.
    /// </summary>
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
                    .Include(dc => dc.Exercises)
                    .Include(dc => dc.Sleep)
                    .Include(dc => dc.CaffeineDrinks)
                    .Include(dc => dc.WellnessCheckIns)
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
                    .Include(dc => dc.Exercises)
                    .Include(dc => dc.Sleep)
                    .Include(dc => dc.CaffeineDrinks)
                    .Include(dc => dc.WellnessCheckIns)
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

        public async Task<DayCard> UpdateDayCardAsync(DayCard updatedDayCard)
        {
            try
            {

                var existingDayCard = await _dbContext.Set<DayCard>().FindAsync(updatedDayCard.Id)
                    ?? throw new ArgumentException("Daycard not found.");

                var changed = false;

                if (existingDayCard.Date != updatedDayCard.Date)
                {
                    existingDayCard.Date = updatedDayCard.Date;
                    changed = true;
                }

                if (changed)
                {
                    await _dbContext.SaveChangesAsync();
                }

                return existingDayCard;

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
                    .Include(dc => dc.Exercises)
                    .Include(dc => dc.Sleep)
                    .Include(dc => dc.CaffeineDrinks)
                    .Include(dc => dc.WellnessCheckIns)
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
