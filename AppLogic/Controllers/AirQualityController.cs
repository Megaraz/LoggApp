using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Controllers.Interfaces;
using AppLogic.Services.Interfaces;

namespace AppLogic.Controllers
{
    public class AirQualityController : IAirQualityController
    {

        private readonly IAirQualityService _airQualityService;

        public AirQualityController(IAirQualityService airQualityService)
        {
            _airQualityService = airQualityService;
        }
    }
}
