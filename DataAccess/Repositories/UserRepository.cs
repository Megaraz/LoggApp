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
        public static void Create(User newUser)
        {
            using (var db = new LoggAppContext())
            {
                try
                {
                    db.Users.Add(newUser);
                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Something went wrong, {e.Message}");

                }

            }
        }
        #endregion

        #region READ
        public static async Task<User?> ReadSingle(int id)
        {
            using (var db = new LoggAppContext())
            {
                try
                {
                    return await db.Users.SingleOrDefaultAsync(x => x.Id == id)!;

                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Something went wrong, {e.Message}");

                }
            }
        }
        public static async Task<User?> ReadSingle(string username)
        {
            using (var db = new LoggAppContext())
            {
                try
                {
                    return await db.Users.SingleOrDefaultAsync(x => x.Username == username)!;

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
