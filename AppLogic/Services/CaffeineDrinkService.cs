using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs;
using AppLogic.Models.Intake;
using AppLogic.Models.Intake.InputModels;
using AppLogic.Repositories;

namespace AppLogic.Services
{
    public class CaffeineDrinkService
    {
        private readonly LoggAppContext _dbContext;
        private readonly CaffeineDrinkRepo _caffeineDrinkRepo;

        public CaffeineDrinkService(LoggAppContext dbContext)
        {
            _dbContext = dbContext;
            _caffeineDrinkRepo = new CaffeineDrinkRepo(_dbContext);
        }

        public async Task<DTO_SpecificCaffeineDrink> AddCaffeineDrinkToDayCardAsync(int dayCardId, CaffeineDrinkInputModel model)
        {
            CaffeineDrink caffeineDrink = new(
                dayCardId, 
                model.TimeOf, 
                model.SizeOfDrink, 
                model.TypeOfDrink
            );

            caffeineDrink = await _caffeineDrinkRepo.CreateAsync(caffeineDrink);

            return new DTO_SpecificCaffeineDrink()
            {
                DayCardId = caffeineDrink.DayCardId,
                CaffeineDrinkId = caffeineDrink.Id,
                TimeOf = caffeineDrink.TimeOf,
                EstimatedMgCaffeine = caffeineDrink.EstimatedMgCaffeine,
                
            };
        }

        public static DTO_AllCaffeineDrinks ConvertToSummaryDTO(List<CaffeineDrink> caffeineDrinks)
        {

            DTO_AllCaffeineDrinks DTO_allCaffeineDrinks = new DTO_AllCaffeineDrinks();

            foreach (var drink in caffeineDrinks)
            {
                DTO_allCaffeineDrinks
                    .HourlyCaffeineData
                        .Add
                        (
                            new DTO_SpecificCaffeineDrink()
                            {
                                DayCardId = drink.DayCardId,
                                CaffeineDrinkId = drink.Id,
                                TimeOf = drink.TimeOf,
                                EstimatedMgCaffeine = drink.EstimatedMgCaffeine,
                                
                            }
                        );
            }
            DTO_allCaffeineDrinks.TotalCaffeineInMg = caffeineDrinks.Sum(x => x.EstimatedMgCaffeine);
            return DTO_allCaffeineDrinks;

            //return new DTO_AllCaffeineDrinks()
            //{
            //    DayCardId = caffeineDrink.
            //    HourlyCaffeineData = new List<DTO_SpecificCaffeineDrink>()
            //    {
            //        new DTO_SpecificCaffeineDrink()
            //        {
            //            CaffeineDrinkId = caffeineDrink.Id,
            //            DayCardId = caffeineDrink.DayCardId,
            //            EstimatedMgCaffeine = caffeineDrink.EstimatedMgCaffeine,
            //            TimeOf = caffeineDrink.TimeOf
            //        }
            //    }
            //};
        }


    }
}
