using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.InputModels;
using AppLogic.Services.Interfaces;

namespace AppLogic.Controllers
{
    public class ExerciseController
    {
        private IExerciseService _exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        public async Task<ExerciseDetailed?> AddExerciseToDayCardAsync(int dayCardId, ExerciseInputModel exerciseInputModel)
        {
            return await _exerciseService.AddExerciseToDayCardAsync(dayCardId, exerciseInputModel);
        }

        public async Task<bool> DeleteExerciseAsync(int exerciseId)
        {
            return await _exerciseService.DeleteExerciseAsync(exerciseId);
        }

        public async Task<ExerciseDetailed?> UpdateExerciseAsync(int exerciseId, ExerciseInputModel updateInputModel)
        {
            return await _exerciseService.UpdateExerciseAsync(exerciseId, updateInputModel);
        }
    }
}
