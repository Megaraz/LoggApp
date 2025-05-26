using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Intake;
using AppLogic.Models.Intake.InputModels;
using AppLogic.Repositories;
using AppLogic.Repositories.Interfaces;
using AppLogic.Services.Interfaces;

namespace AppLogic.Services
{
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
                DayCardId = (int)caffeineDrink!.DayCardId!,
                CaffeineDrinkId = caffeineDrink.Id,
                TimeOf = caffeineDrink.TimeOf,
                EstimatedMgCaffeine = caffeineDrink.EstimatedMgCaffeine,
                
            };
        }

        public CaffeineDrinkSummary ConvertToSummaryDTO(List<CaffeineDrink> caffeineDrinks)
        {

            CaffeineDrinkSummary DTO_allCaffeineDrinks = new CaffeineDrinkSummary();

            foreach (var drink in caffeineDrinks)
            {
                DTO_allCaffeineDrinks
                    .CaffeineDrinksDetails
                        .Add
                        (
                            new CaffeineDrinkDetailed()
                            {
                                DayCardId = (int)drink.DayCardId!,
                                CaffeineDrinkId = drink.Id,
                                TimeOf = drink.TimeOf,
                                EstimatedMgCaffeine = drink.EstimatedMgCaffeine,
                                
                            }
                        );
            }
            DTO_allCaffeineDrinks.TotalCaffeineInMg = caffeineDrinks.Sum(x => x.EstimatedMgCaffeine);
            return DTO_allCaffeineDrinks;

        }

        public async Task<bool> DeleteCaffeineDrinkAsync(int caffeineDrinkId)
        {

            return await _caffeineDrinkRepo.DeleteAsync(caffeineDrinkId);
        }
    }
}
