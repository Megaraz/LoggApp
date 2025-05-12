using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.DTOs;
using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class UserRepository
    {
        #region CREATE
        public static async Task<User> CreateAsync(User newUser)
        {
            using (var db = new LoggAppContext())
            {
                try
                {
                    await db.Users.AddAsync(newUser);
                    await db.SaveChangesAsync();

                    return newUser;
                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Something went wrong, {e.Message}");

                }

            }
        }
        #endregion

        #region READ
        public static async Task<SpecificUserMenuDto?> ReadSingleAsync(int id)
        {
            using (var db = new LoggAppContext())
            {
                try
                {
                    return await db.Users
                        .Where(u => u.Id == id)
                        .Select(u => new SpecificUserMenuDto
                        {
                            Id = u.Id,
                            Username = u.Username!,
                            CityName = u.CityName,
                            Lat = u.Lat,
                            Lon = u.Lon,
                            AllDayCardsMenu = u.DayCards!
                            .Select(d => new AllDayCardsMenuDto
                            {
                                DayCardId = d.Id,
                                UserId = d.UserId,
                                Date = d.Date

                            }).ToList()
                        })
                        .SingleOrDefaultAsync();

                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Something went wrong, {e.Message}");

                }
            }
        }
        public static async Task<SpecificUserMenuDto?> ReadSingleAsync(string username)
        {
            using (var db = new LoggAppContext())
            {
                try
                {
                    return await db.Users
                        .Where(u => u.Username == username)
                        .Select(u => new SpecificUserMenuDto
                        {
                            Id = u.Id,
                            Username = u.Username!,
                            CityName = u.CityName,
                            Lat = u.Lat,
                            Lon = u.Lon,
                            AllDayCardsMenu = u.DayCards!
                            .Select(d => new AllDayCardsMenuDto
                            {
                                DayCardId = d.Id,
                                UserId = d.UserId,
                                Date = d.Date

                            }).ToList()
                        })
                        .SingleOrDefaultAsync();


                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Something went wrong, {e.Message}");

                }
            }
        }

        public static async Task<List<AllUserMenuDto>> ReadAllAsync()
        {
            using (var db = new LoggAppContext())
            {
                try
                {
                    return await db.Users
                        .Select(x => new AllUserMenuDto
                        {
                            Id = x.Id,
                            Username = x.Username!,
                            DayCardCount = x.DayCards!.Count,
                            CityName = x.CityName
                        })
                        .ToListAsync();

                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Something went wrong, {e.Message}");
                }
            }
        }
        #endregion
    }
}
