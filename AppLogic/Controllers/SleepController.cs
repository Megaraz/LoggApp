using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Controllers.Interfaces;
using AppLogic.Services.Interfaces;

namespace AppLogic.Controllers
{
    public class SleepController : ISleepController
    {
        private ISleepService sleepService;

        public SleepController(ISleepService sleepService)
        {
            this.sleepService = sleepService;
        }
    }
}
