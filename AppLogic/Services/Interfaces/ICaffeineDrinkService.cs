using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Entities;
using AppLogic.Models.InputModels;

namespace AppLogic.Services.Interfaces
{
    public interface ICaffeineDrinkService
    {
        Task<CaffeineDrinkDetailed> AddCaffeineDrinkToDayCardAsync(int dayCardId, CaffeineDrinkInputModel model);
        Task<bool> DeleteCaffeineDrinkAsync(int caffeineDrinkId);
        Task<CaffeineDrinkDetailed?> UpdateCaffeineDrinkAsync(int caffeineDrinkId, CaffeineDrinkInputModel updateInputModel);
    }
}
