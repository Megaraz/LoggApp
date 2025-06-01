using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Entities;
using AppLogic.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AppLogic.Repositories
{
    /// <summary>
    /// Repository for managing exercises associated with day cards, providing methods to create, read, update, and delete exercises.
    /// </summary>
    public class ExerciseRepo : GenericRepo<Exercise>, IExerciseRepo
    {
        private readonly LoggAppContext _dbContext;

        public ExerciseRepo(LoggAppContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;

        }

        public Task<Exercise?> GetExerciseById(int dayCardId, int exerciseId)
        {
            try
            {
                return _dbContext.Set<Exercise>()
                    .SingleOrDefaultAsync(e => e.DayCardId == dayCardId && e.Id == exerciseId);

            }
            catch (Exception e)
            {
                throw new NullReferenceException($"No Exercise found by those IDs, {e.Message}");
            }
        }

        public async Task<Exercise> UpdateExerciseAsync(Exercise updatedExercise)
        {
            try
            {

                var existingExercise = await _dbContext.Set<Exercise>().FindAsync(updatedExercise.Id)
                    ?? throw new ArgumentException("Exercise not found.");

                var changed = false;

                if (existingExercise.TimeOf != updatedExercise.TimeOf)
                {
                    existingExercise.TimeOf = updatedExercise.TimeOf;
                    changed = true;
                }
                if (existingExercise.EndTime != updatedExercise.EndTime)
                {
                    existingExercise.EndTime = updatedExercise.EndTime;
                    changed = true;
                }
                if (existingExercise.Duration != updatedExercise.Duration)
                {
                    existingExercise.Duration = updatedExercise.Duration;
                    changed = true;
                }
                if (existingExercise.ExerciseType != updatedExercise.ExerciseType)
                {
                    existingExercise.ExerciseType = updatedExercise.ExerciseType;
                    changed = true;
                }
                if (existingExercise.PerceivedIntensity != updatedExercise.PerceivedIntensity)
                {
                    existingExercise.PerceivedIntensity = updatedExercise.PerceivedIntensity;
                    changed = true;
                }
                if (existingExercise.TrainingLoad != updatedExercise.TrainingLoad)
                {
                    existingExercise.TrainingLoad = updatedExercise.TrainingLoad;
                    changed = true;
                }
                if (existingExercise.AvgHeartRate != updatedExercise.AvgHeartRate)
                {
                    existingExercise.AvgHeartRate = updatedExercise.AvgHeartRate;
                    changed = true;
                }
                if (existingExercise.ActiveKcalBurned != updatedExercise.ActiveKcalBurned)
                {
                    existingExercise.ActiveKcalBurned = updatedExercise.ActiveKcalBurned;
                    changed = true;
                }
                if (existingExercise.DistanceInKm != updatedExercise.DistanceInKm)
                {
                    existingExercise.DistanceInKm = updatedExercise.DistanceInKm;
                    changed = true;
                }
                if (existingExercise.AvgKmTempo != updatedExercise.AvgKmTempo)
                {
                    existingExercise.AvgKmTempo = updatedExercise.AvgKmTempo;
                    changed = true;
                }
                if (existingExercise.Steps != updatedExercise.Steps)
                {
                    existingExercise.Steps = updatedExercise.Steps;
                    changed = true;
                }
                if (existingExercise.AvgStepLengthInCm != updatedExercise.AvgStepLengthInCm)
                {
                    existingExercise.AvgStepLengthInCm = updatedExercise.AvgStepLengthInCm;
                    changed = true;
                }
                if (existingExercise.AvgStepPerMin != updatedExercise.AvgStepPerMin)
                {
                    existingExercise.AvgStepPerMin = updatedExercise.AvgStepPerMin;
                    changed = true;
                }

                if (changed)
                {
                    await _dbContext.SaveChangesAsync();
                }

                return existingExercise;

            }
            catch (Exception e)
            {
                throw new ArgumentException($"Something went wrong, {e.Message}");
            }
        }
    }
}
