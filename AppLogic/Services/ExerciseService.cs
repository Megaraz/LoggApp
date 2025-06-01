using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Entities;
using AppLogic.Models.InputModels;
using AppLogic.Repositories.Interfaces;
using AppLogic.Services.Interfaces;

namespace AppLogic.Services
{
    /// <summary>
    /// Service for managing exercises, including adding, updating, and deleting exercises associated with day cards.
    /// </summary>
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepo _exerciseRepo;

        public ExerciseService(IExerciseRepo exerciseRepo)
        {
            _exerciseRepo = exerciseRepo;
        }

        public async Task<ExerciseDetailed> AddExerciseToDayCardAsync(int dayCardId, ExerciseInputModel exerciseInputModel)
        {
            Exercise exercise = new(dayCardId, exerciseInputModel);

            exercise = await _exerciseRepo.CreateAsync(exercise);

            return new ExerciseDetailed(exercise);
            
        }

        public async Task<bool> DeleteExerciseAsync(int exerciseId)
        {
            return await _exerciseRepo.DeleteAsync(exerciseId);
        }

        public async Task<ExerciseDetailed?> ReadSingleExerciseAsync(int dayCardId, int exerciseId)
        {
            Exercise? exercise = await _exerciseRepo.GetExerciseById(dayCardId, exerciseId);

            return exercise == null ? null : new ExerciseDetailed(exercise);
        }

        public async Task<ExerciseDetailed?> UpdateExerciseAsync(int exerciseId, ExerciseInputModel updateInputModel)
        {
            Exercise exercise = new(updateInputModel.DayCardId, updateInputModel);

            exercise.Id = exerciseId; 

            exercise = await _exerciseRepo.UpdateExerciseAsync(exercise);

            return new ExerciseDetailed(exercise);

        }
    }
}
