using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Entities;
using AppLogic.Models.InputModels;

namespace AppLogic.Services.Interfaces
{
    public interface IExerciseService
    {
        Task<ExerciseDetailed> AddExerciseToDayCardAsync(int dayCardId, ExerciseInputModel exerciseInputModel);
        Task<bool> DeleteExerciseAsync(int exerciseId);
        Task<ExerciseDetailed?> UpdateExerciseAsync(int exerciseId, ExerciseInputModel updateInputModel);
    }
}
