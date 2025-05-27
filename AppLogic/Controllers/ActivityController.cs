using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Services.Interfaces;

namespace AppLogic.Controllers
{
    public class ActivityController
    {
        private IActivityService activityService;

        public ActivityController(IActivityService activityService)
        {
            this.activityService = activityService;
        }
    }
}
