using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Services.Interfaces;

namespace AppLogic.Controllers
{
    public class WellnessCheckInController
    {
        private IWellnessCheckInService wellnessCheckInService;

        public WellnessCheckInController(IWellnessCheckInService wellnessCheckInService)
        {
            this.wellnessCheckInService = wellnessCheckInService;
        }
    }
}
