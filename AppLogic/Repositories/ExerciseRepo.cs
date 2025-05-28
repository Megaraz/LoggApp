using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Entities;
using AppLogic.Repositories.Interfaces;

namespace AppLogic.Repositories
{
    public class ExerciseRepo : GenericRepo<Exercise>, IExerciseRepo
    {
        public ExerciseRepo(LoggAppContext dbContext) : base(dbContext)
        {
        }
    }
}
