using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs;
using AppLogic.Models;

namespace AppLogic.Controllers.Interfaces
{
    public interface IDayCardController
    {
        Task<DTO_SpecificDayCard> CreateNewDayCardAsync(int userId, DayCardInputModel input);

        Task<List<DTO_AllDayCard>?> ReadAllDayCardsAsync(int userId);
        Task<DTO_SpecificDayCard?> ReadDayCardSingleAsync(int id, int userId);
        Task<DTO_SpecificDayCard?> ReadDayCardSingleAsync(DateOnly date, int userId);
    }
}
