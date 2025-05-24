using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs;
using AppLogic.Models.Intake.InputModels;

namespace AppLogic.Controllers.Interfaces
{
    public interface ICaffeineDrinkController
    {
        Task<DTO_SpecificCaffeineDrink> AddCaffeineDrinkToDayCardAsync(int dayCardId, CaffeineDrinkInputModel input);
    }
}
