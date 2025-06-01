using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Entities;

namespace AppLogic.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for managing exercises associated with day cards, providing methods to create, read, update, and delete exercises.
    /// </summary>
    public interface IExerciseRepo : IGenericRepo<Exercise>
    {
        Task<Exercise?> GetExerciseById(int dayCardId, int exerciseId);
        Task<Exercise> UpdateExerciseAsync(Exercise exercise);
    }
}
