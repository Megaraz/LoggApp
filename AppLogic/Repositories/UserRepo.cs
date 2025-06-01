using AppLogic;
using AppLogic.Models.Entities;
using AppLogic.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AppLogic.Repositories
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {

        private readonly LoggAppContext _dbContext;

        public UserRepo(LoggAppContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        #region READ
        public async Task<User?> GetUserByIdIncludeAsync(int id)
        {
            try
            {
               return await _dbContext.Users
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.Exercises)
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.Sleep)
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.WellnessCheckIns)
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.CaffeineDrinks)
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.WeatherData)
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.AirQualityData)
                    .AsSplitQuery()
                    .SingleOrDefaultAsync(u => u.Id == id);

            }
            catch (Exception e)
            {
                throw new ArgumentException($"Something went wrong, {e.Message}");

            }
        }
        public async Task<User?> GetUserByUsernameIncludeAsync(string username)
        {
            try
            {
                return await _dbContext.Users
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.Exercises)
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.Sleep)
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.WellnessCheckIns)
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.CaffeineDrinks)
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.WeatherData)
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.AirQualityData)
                    .AsSplitQuery()
                    .SingleOrDefaultAsync(u => u.Username == username);


            }
            catch (Exception e)
            {
                throw new ArgumentException($"Something went wrong, {e.Message}");

            }
        }

        public async Task<List<User>?> GetAllUsersIncludeAsync()
        {
            try
            {
                //return await _dbContext.Users.ToListAsync();
                return await _dbContext.Users
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.Exercises)
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.Sleep)
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.WellnessCheckIns)
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.CaffeineDrinks)
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.WeatherData)
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.AirQualityData)
                    .AsSplitQuery()
                    .ToListAsync();

            }
            catch (Exception e)
            {
                throw new ArgumentException($"Something went wrong, {e.Message}");
            }
        }

        public async Task<User> UpdateUserIncludeAsync(User updatedUser)
        {
            try
            {

                var existingUser = await _dbContext.Set<User>().FindAsync(updatedUser.Id) 
                    ?? throw new ArgumentException("User not found.");

                var changed = false;

                if (existingUser.Username != updatedUser.Username)
                {
                    existingUser.Username = updatedUser.Username;
                    changed = true;
                }
                if (existingUser.CityName != updatedUser.CityName)
                {
                    existingUser.CityName = updatedUser.CityName;
                    changed = true;
                }
                if (existingUser.Lat != updatedUser.Lat || existingUser.Lon != updatedUser.Lon)
                {
                    existingUser.Lat = updatedUser.Lat;
                    existingUser.Lon = updatedUser.Lon;
                    changed = true;
                }
                
                if (changed)
                {
                    await _dbContext.SaveChangesAsync();
                }

                return existingUser;

            }
            catch (Exception e)
            {
                throw new ArgumentException($"Something went wrong, {e.Message}");
            }
        }

        public Task<List<DayCard>> GetLast7DayCardsAsync(int userId)
        {
            try
            {
                return _dbContext.DayCards
                    .Where(dc => dc.UserId == userId)
                    .OrderByDescending(dc => dc.Date)
                    .Take(7)
                    .Include(dc => dc.Exercises)
                    .Include(dc => dc.Sleep)
                    .Include(dc => dc.WellnessCheckIns)
                    .Include(dc => dc.CaffeineDrinks)
                    .Include(dc => dc.WeatherData)
                    .Include(dc => dc.AirQualityData)
                    .ToListAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
