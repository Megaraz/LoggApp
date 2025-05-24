using AppLogic;
using AppLogic.Models;
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
                        .ThenInclude(dc => dc.Activities)
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.CaffeineDrinks)
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.Supplements)
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
                        .ThenInclude(dc => dc.Activities)
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.CaffeineDrinks)
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.Supplements)
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
                return await _dbContext.Users
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.Activities)
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.CaffeineDrinks)
                    .Include(u => u.DayCards!)
                        .ThenInclude(dc => dc.Supplements)
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
           
        #endregion
    }
}
