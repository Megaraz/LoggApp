using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public static class GenericRepo<T> where T : class
    {

        public static void Add(T entity)
        {
            using (var db = new LoggAppContext())
            {
                try
                {
                    db.Set<T>().Add(entity);
                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Something went wrong, {e.Message}");

                }

            }
        }

        public static User? ReadUser_Single(string username)
        {
            using (var db = new LoggAppContext())
            {
                try
                {
                    //var result = db.Users.SingleOrDefault(x => x.Username == username)!;



                    return db.Users.SingleOrDefault(x => x.Username == username)!;
                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Something went wrong, {e.Message}");

                }
            }
        }

        public static DayCard ReadDayCard_Single(int id)
        {
            using (var db = new LoggAppContext())
            {
                try
                {
                    return db.DayCards.SingleOrDefault(x => x.Id == id);
                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Something went wrong, {e.Message}");

                }
            }
        }

        public static DayCard ReadDayCard_Single(DateOnly date)
        {
            using (var db = new LoggAppContext())
            {
                try
                {
                    return db.DayCards.SingleOrDefault(x => x.Date == date);
                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Something went wrong, {e.Message}");

                }
            }
        }
        public static List<T> ReadAll()
        {
            using (var db = new LoggAppContext())
            {
                try
                {
                    return db.Set<T>().ToList();
                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Something went wrong, {e.Message}");

                }
            }
        }

        //public static List<DayCard> ReadAllDayCards()
        //{
        //    using (var db = new LoggAppContext())
        //    {
        //        try
        //        {
        //            return db.DayCards.Include(j => j.Supplements).Include(h => h.Activities).Include(c => c.CaffeineDrinks).ToList();
        //        }
        //        catch (Exception e)
        //        {
        //            throw new ArgumentException($"Something went wrong, {e.Message}");

        //        }
        //    }

        //}

        public static void UpdateDayCard(int id, DateOnly date)
        {

            using (var db = new LoggAppContext())
            {
                try
                {
                    //DayCard dayCardToUpdate = ReadDayCard_Single(id);

                    DayCard toChange = db.DayCards.SingleOrDefault(x => x.Id == id);
                    toChange.Date = date;
                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Something went wrong, {e.Message}");

                }
            }

        }

        public static void DeleteDayCard(int id)
        {
            using (var db = new LoggAppContext())
            {
                try
                {
                    db.DayCards.Remove(ReadDayCard_Single(id));
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Something went wrong, {e.Message}");

                }
            }
        }

    }
}
