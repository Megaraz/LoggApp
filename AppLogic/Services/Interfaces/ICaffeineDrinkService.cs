using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs;
using AppLogic.Models.Intake;
using AppLogic.Models.Intake.InputModels;

namespace AppLogic.Services.Interfaces
{
    public interface ICaffeineDrinkService
    {
        Task<DTO_SpecificCaffeineDrink> AddCaffeineDrinkToDayCardAsync(int dayCardId, CaffeineDrinkInputModel model);

        DTO_AllCaffeineDrinks ConvertToSummaryDTO(List<CaffeineDrink> caffeineDrinks);
    }
}
