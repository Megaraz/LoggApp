using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Repositories.Interfaces;
using AppLogic.Services.Interfaces;

namespace AppLogic.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepo _exerciseRepo;

        public ExerciseService(IExerciseRepo exerciseRepo)
        {
            _exerciseRepo = exerciseRepo;
        }
    }
}
