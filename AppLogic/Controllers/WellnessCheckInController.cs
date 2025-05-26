using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Controllers.Interfaces;
using AppLogic.Services.Interfaces;

namespace AppLogic.Controllers
{
    public class WellnessCheckInController : IWellnessCheckInController
    {
        private IWellnessCheckInService wellnessCheckInService;

        public WellnessCheckInController(IWellnessCheckInService wellnessCheckInService)
        {
            this.wellnessCheckInService = wellnessCheckInService;
        }
    }
}
