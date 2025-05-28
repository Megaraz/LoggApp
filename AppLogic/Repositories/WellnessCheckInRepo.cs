using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Entities;
using AppLogic.Repositories.Interfaces;

namespace AppLogic.Repositories
{
    public class WellnessCheckInRepo : GenericRepo<WellnessCheckIn>, IWellnessCheckInRepo
    {
        public WellnessCheckInRepo(LoggAppContext dbContext) : base(dbContext)
        {
        }


    }
}
