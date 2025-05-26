using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models;
using AppLogic.Repositories.Interfaces;

namespace AppLogic.Repositories
{
    public class SleepRepo : GenericRepo<Sleep>, ISleepRepo
    {
        public SleepRepo(LoggAppContext dbContext) : base(dbContext)
        {
        }


    }
}
