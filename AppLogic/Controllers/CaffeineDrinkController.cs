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
    /// <summary>
    /// Controller for managing caffeine drinks, including adding, updating, and deleting caffeine drinks associated with day cards.
    /// </summary>
    public class CaffeineDrinkController
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

        public async Task<CaffeineDrinkDetailed?> UpdateCaffeineDrinkAsync(int caffeineDrinkId, CaffeineDrinkInputModel updateInputModel)
        {
            return await _caffeineDrinkService.UpdateCaffeineDrinkAsync(caffeineDrinkId, updateInputModel);
        }
    }
}
