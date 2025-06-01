using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Entities;
using AppLogic.Repositories.Interfaces;

namespace AppLogic.Repositories
{
    /// <summary>
    /// Repository for managing wellness check-ins associated with day cards.
    /// </summary>
    public class WellnessCheckInRepo : GenericRepo<WellnessCheckIn>, IWellnessCheckInRepo
    {
        private readonly LoggAppContext _dbContext;
        public WellnessCheckInRepo(LoggAppContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WellnessCheckIn> UpdateCheckInAsync(WellnessCheckIn updatedCheckIn)
        {
            try
            {
                var existingCheckIn = await  _dbContext.Set<WellnessCheckIn>().FindAsync(updatedCheckIn.Id) 
                    ?? throw new ArgumentException("Exercise not found.");

                var changed = false;


                if (existingCheckIn.TimeOf != updatedCheckIn.TimeOf)
                {
                    existingCheckIn.TimeOf = updatedCheckIn.TimeOf;
                    changed = true;
                }
                if (existingCheckIn.MoodLevel != updatedCheckIn.MoodLevel)
                {
                    existingCheckIn.MoodLevel = updatedCheckIn.MoodLevel;
                    changed = true;
                }
                if (existingCheckIn.EnergyLevel != updatedCheckIn.EnergyLevel)
                {
                    existingCheckIn.EnergyLevel = updatedCheckIn.EnergyLevel;
                    changed = true;
                }

                if (changed)
                {
                    await _dbContext.SaveChangesAsync();
                }

                return existingCheckIn;


            }
            catch (Exception e)
            {
                throw new ArgumentException($"Something went wrong, {e.Message}");
            }
        }
    }
}
