using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.Entities;
using AppLogic.Models.InputModels;
using AppLogic.Repositories.Interfaces;
using AppLogic.Services.Interfaces;

namespace AppLogic.Services
{
    public class SleepService : ISleepService
    {
        private readonly ISleepRepo _sleepRepo;

        public SleepService(ISleepRepo sleepRepo)
        {
            _sleepRepo = sleepRepo;
        }

        public async Task<SleepDetailed> AddSleepToDayCardAsync(int dayCardId, SleepInputModel sleepInputModel)
        {
            Sleep sleep = new Sleep(dayCardId, sleepInputModel);

            sleep = await _sleepRepo.CreateAsync(sleep);

            return new SleepDetailed(sleep);

        }
    }
}
