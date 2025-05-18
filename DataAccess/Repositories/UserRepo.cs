using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Interfaces;
using AppLogic.Models.DTOs;
using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class UserRepo : GenericRepo<User>
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
                        .ThenInclude(dc => dc.AirQualities)
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
                        .ThenInclude(dc => dc.AirQualities)
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
                        .ThenInclude(dc => dc.AirQualities)
                    .AsSplitQuery()
                    .ToListAsync();

            }
            catch (Exception e)
            {
                throw new ArgumentException($"Something went wrong, {e.Message}");
            }
        }
                

        //public async Task<DTO_SpecificUser?> ReadSingleAsync(int id)
        //{
        //    try
        //    {
        //        return await _dbContext.Users
        //            .Where(u => u.Id == id)
        //            .Select(u => new DTO_SpecificUser
        //            {
        //                Id = u.Id,
        //                Username = u.Username!,
        //                CityName = u.CityName,
        //                Lat = u.Lat,
        //                Lon = u.Lon,
        //                AllDayCardsMenu = u.DayCards!
        //                .Select(d => new DTO_AllDayCards
        //                {
        //                    DayCardId = d.Id,
        //                    UserId = d.UserId,
        //                    Date = d.Date

        //                }).ToList()
        //            })
        //            .SingleOrDefaultAsync();

        //    }
        //    catch (Exception e)
        //    {
        //        throw new ArgumentException($"Something went wrong, {e.Message}");

        //    }
        //}
        //public async Task<DTO_SpecificUser?> ReadSingleAsync(string username)
        //{
        //    try
        //    {
        //        return await _dbContext.Users
        //            .Where(u => u.Username == username)
        //            .Select(u => new DTO_SpecificUser
        //            {
        //                Id = u.Id,
        //                Username = u.Username!,
        //                CityName = u.CityName,
        //                Lat = u.Lat,
        //                Lon = u.Lon,
        //                AllDayCardsMenu = u.DayCards!
        //                .Select(d => new DTO_AllDayCards
        //                {
        //                    DayCardId = d.Id,
        //                    UserId = d.UserId,
        //                    Date = d.Date

        //                }).ToList()
        //            })
        //            .SingleOrDefaultAsync();


        //    }
        //    catch (Exception e)
        //    {
        //        throw new ArgumentException($"Something went wrong, {e.Message}");

        //    }
        //}

        //public async Task<List<DTO_AllUser>> ReadAllAsync()
        //{
        //    try
        //    {
        //        return await _dbContext.Users
        //            .Select(x => new DTO_AllUser
        //            {
        //                Id = x.Id,
        //                Username = x.Username!,
        //                DayCardCount = x.DayCards!.Count,
        //                CityName = x.CityName
        //            })
        //            .ToListAsync();

        //    }
        //    catch (Exception e)
        //    {
        //        throw new ArgumentException($"Something went wrong, {e.Message}");
        //    }
        //}
        #endregion
    }
}
