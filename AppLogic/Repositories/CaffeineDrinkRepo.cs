using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models;
using AppLogic.Models.Entities;
using AppLogic.Repositories.Interfaces;

namespace AppLogic.Repositories
{
    public class CaffeineDrinkRepo : GenericRepo<CaffeineDrink>, ICaffeineDrinkRepo
    {
        private readonly LoggAppContext _dbContext;

        public CaffeineDrinkRepo(LoggAppContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CaffeineDrink> UpdateCaffeineDrinkAsync(CaffeineDrink updatedCaffeineDrink)
        {
            try
            {

                var existingCaffeineDrink = await _dbContext.Set<CaffeineDrink>().FindAsync(updatedCaffeineDrink.Id)
                    ?? throw new ArgumentException("Caffeinedrink not found.");

                var changed = false;

                if (existingCaffeineDrink.EstimatedMgCaffeine != updatedCaffeineDrink.EstimatedMgCaffeine)
                {
                    existingCaffeineDrink.EstimatedMgCaffeine = updatedCaffeineDrink.EstimatedMgCaffeine;
                    changed = true;
                }
                if (existingCaffeineDrink.TimeOf != updatedCaffeineDrink.TimeOf)
                {
                    existingCaffeineDrink.TimeOf = updatedCaffeineDrink.TimeOf;
                    changed = true;
                }

                if (changed)
                {
                    await _dbContext.SaveChangesAsync();
                }

                return existingCaffeineDrink;

            }
            catch (Exception e)
            {
                throw new ArgumentException($"Something went wrong, {e.Message}");
            }
        }
    }
}
