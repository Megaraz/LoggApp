using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Entities;
using AppLogic.Models.InputModels;
using AppLogic.Repositories;
using AppLogic.Repositories.Interfaces;
using AppLogic.Services.Interfaces;

namespace AppLogic.Services
{
    /// <summary>
    /// Service for managing caffeine drink data, including adding, updating, and deleting caffeine drinks associated with day cards.
    /// </summary>
    public class CaffeineDrinkService : ICaffeineDrinkService
    {
        private readonly ICaffeineDrinkRepo _caffeineDrinkRepo;

        public CaffeineDrinkService(ICaffeineDrinkRepo caffeineDrinkRepo)
        {
            _caffeineDrinkRepo = caffeineDrinkRepo;
        }

        public async Task<CaffeineDrinkDetailed> AddCaffeineDrinkToDayCardAsync(int dayCardId, CaffeineDrinkInputModel model)
        {
            CaffeineDrink caffeineDrink = new(
                dayCardId, 
                model.TimeOf, 
                model.SizeOfDrink, 
                model.TypeOfDrink
            );

            caffeineDrink = await _caffeineDrinkRepo.CreateAsync(caffeineDrink);

            return new CaffeineDrinkDetailed()
            {
                DayCardId = caffeineDrink!.DayCardId!,
                Id = caffeineDrink.Id,
                TimeOf = caffeineDrink.TimeOf,
                EstimatedMgCaffeine = caffeineDrink.EstimatedMgCaffeine,
                
            };
        }

        public async Task<bool> DeleteCaffeineDrinkAsync(int caffeineDrinkId)
        {

            return await _caffeineDrinkRepo.DeleteAsync(caffeineDrinkId);
        }

        public async Task<CaffeineDrinkDetailed?> UpdateCaffeineDrinkAsync(int caffeineDrinkId, CaffeineDrinkInputModel updateInputModel)
        {
            CaffeineDrink updatedCaffeineDrink = new(
                updateInputModel.DayCardId,
                updateInputModel.TimeOf,
                updateInputModel.SizeOfDrink,
                updateInputModel.TypeOfDrink
            );
            updatedCaffeineDrink.Id = caffeineDrinkId;

            updatedCaffeineDrink = await _caffeineDrinkRepo.UpdateCaffeineDrinkAsync(updatedCaffeineDrink);

            return new CaffeineDrinkDetailed()
            {
                DayCardId = updatedCaffeineDrink!.DayCardId!,
                Id = updatedCaffeineDrink.Id,
                TimeOf = updatedCaffeineDrink.TimeOf,
                EstimatedMgCaffeine = updatedCaffeineDrink.EstimatedMgCaffeine,

            };

        }
    }
}
