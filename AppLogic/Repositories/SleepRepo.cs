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
    /// Repository for managing sleep records associated with day cards.
    /// </summary>
    public class SleepRepo : GenericRepo<Sleep>, ISleepRepo
    {
        private readonly LoggAppContext _dbContext;

        public SleepRepo(LoggAppContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Sleep> UpdateSleepAsync(Sleep updatedSleep)
        {
            try
            {

                var existingSleep = await _dbContext.Set<Sleep>().FindAsync(updatedSleep.Id)
                    ?? throw new ArgumentException("Sleep not found.");

                var changed = false;

                if (existingSleep.SleepStart != updatedSleep.SleepStart)
                {
                    existingSleep.SleepStart = updatedSleep.SleepStart;
                    changed = true;
                }

                if (existingSleep.SleepEnd != updatedSleep.SleepEnd)
                {
                    existingSleep.SleepEnd = updatedSleep.SleepEnd;
                    changed = true;
                }

                if (existingSleep.TotalSleepTime != updatedSleep.TotalSleepTime)
                {
                    existingSleep.TotalSleepTime = updatedSleep.TotalSleepTime;
                    changed = true;
                }

                if (existingSleep.DeepSleepDuration != updatedSleep.DeepSleepDuration)
                {
                    existingSleep.DeepSleepDuration = updatedSleep.DeepSleepDuration;
                    changed = true;
                }

                if (existingSleep.LightSleepDuration != updatedSleep.LightSleepDuration)
                {
                    existingSleep.LightSleepDuration = updatedSleep.LightSleepDuration;
                    changed = true;
                }

                if (existingSleep.RemSleepDuration != updatedSleep.RemSleepDuration)
                {
                    existingSleep.RemSleepDuration = updatedSleep.RemSleepDuration;
                    changed = true;
                }

                if (existingSleep.SleepScore != updatedSleep.SleepScore)
                {
                    existingSleep.SleepScore = updatedSleep.SleepScore;
                    changed = true;
                }

                if (existingSleep.TimesWokenUp != updatedSleep.TimesWokenUp)
                {
                    existingSleep.TimesWokenUp = updatedSleep.TimesWokenUp;
                    changed = true;
                }

                if (existingSleep.AvgBPM != updatedSleep.AvgBPM)
                {
                    existingSleep.AvgBPM = updatedSleep.AvgBPM;
                    changed = true;
                }

                if (existingSleep.Avg02 != updatedSleep.Avg02)
                {
                    existingSleep.Avg02 = updatedSleep.Avg02;
                    changed = true;
                }

                if (existingSleep.AvgBreathsPerMin != updatedSleep.AvgBreathsPerMin)
                {
                    existingSleep.AvgBreathsPerMin = updatedSleep.AvgBreathsPerMin;
                    changed = true;
                }

                if (existingSleep.PerceivedSleepQuality != updatedSleep.PerceivedSleepQuality)
                {
                    existingSleep.PerceivedSleepQuality = updatedSleep.PerceivedSleepQuality;
                    changed = true;
                }
                

                if (changed)
                {
                    await _dbContext.SaveChangesAsync();
                }

                return existingSleep;

            }
            catch (Exception e)
            {
                throw new ArgumentException($"Something went wrong, {e.Message}");
            }
        }

    }
}
