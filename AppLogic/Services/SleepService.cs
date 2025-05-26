using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
