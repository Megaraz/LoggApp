using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.InputModels;
using AppLogic.Services.Interfaces;

namespace AppLogic.Controllers
{
    public class SleepController
    {
        private ISleepService _sleepService;

        public SleepController(ISleepService sleepService)
        {
            _sleepService = sleepService;
        }

        public async Task<SleepDetailed> AddSleepToDayCardAsync(int dayCardId, SleepInputModel sleepInputModel)
        {
            return await _sleepService.AddSleepToDayCardAsync(dayCardId, sleepInputModel);
        }
    }
}
