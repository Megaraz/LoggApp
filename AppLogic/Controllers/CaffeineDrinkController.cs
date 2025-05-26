using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Controllers.Interfaces;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.Intake.InputModels;
using AppLogic.Services.Interfaces;

namespace AppLogic.Controllers
{
    public class CaffeineDrinkController : ICaffeineDrinkController
    {

        private readonly ICaffeineDrinkService _caffeineDrinkService;

        public CaffeineDrinkController(ICaffeineDrinkService caffeineDrinkService)
        {
            _caffeineDrinkService = caffeineDrinkService;
        }


        public async Task<CaffeineDrinkDetailed> AddCaffeineDrinkToDayCardAsync(int dayCardId, CaffeineDrinkInputModel input)
        {
            return await _caffeineDrinkService.AddCaffeineDrinkToDayCardAsync(dayCardId, input);
        }

        public async Task<bool> DeleteCaffeineDrinkAsync(int caffeineDrinkId)
        {
            return await _caffeineDrinkService.DeleteCaffeineDrinkAsync(caffeineDrinkId);
        }
    }
}
