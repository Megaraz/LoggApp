using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Intake;
using AppLogic.Models.Intake.InputModels;

namespace AppLogic.Services.Interfaces
{
    public interface ICaffeineDrinkService
    {
        Task<CaffeineDrinkDetailed> AddCaffeineDrinkToDayCardAsync(int dayCardId, CaffeineDrinkInputModel model);

        CaffeineDrinkSummary ConvertToSummaryDTO(List<CaffeineDrink> caffeineDrinks);
    }
}
