using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Activity;
using AppLogic.Repositories.Interfaces;

namespace AppLogic.Repositories
{
    public class ActivityRepo : GenericRepo<Activity>, IActivityRepo
    {
        public ActivityRepo(LoggAppContext dbContext) : base(dbContext)
        {
        }
    }
}
