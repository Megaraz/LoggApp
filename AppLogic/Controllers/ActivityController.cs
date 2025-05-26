using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Controllers.Interfaces;
using AppLogic.Services.Interfaces;

namespace AppLogic.Controllers
{
    public class ActivityController : IActivityController
    {
        private IActivityService activityService;

        public ActivityController(IActivityService activityService)
        {
            this.activityService = activityService;
        }
    }
}
