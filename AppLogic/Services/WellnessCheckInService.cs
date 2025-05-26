using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Repositories;
using AppLogic.Repositories.Interfaces;
using AppLogic.Services.Interfaces;

namespace AppLogic.Services
{
    public class WellnessCheckInService : IWellnessCheckInService
    {
        private readonly IWellnessCheckInRepo _wellnessCheckInRepo;

        public WellnessCheckInService(IWellnessCheckInRepo wellnessCheckInRepo)
        {
            _wellnessCheckInRepo = wellnessCheckInRepo;
            
        }
    }
}
