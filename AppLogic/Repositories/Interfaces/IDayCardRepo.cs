using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Entities;

namespace AppLogic.Repositories.Interfaces
{
    public interface IDayCardRepo : IGenericRepo<DayCard>
    {
        Task<DayCard?> GetDayCardByDateIncludeAsync(DateOnly date, int userId);
        Task<DayCard?> GetDayCardByIdIncludeAsync(int id, int userId);
        Task<List<DayCard>?> GetAllDayCardsIncludeAsync(int userId);

        Task<DayCard> UpdateDayCardAsync(DayCard updatedDayCard);
    }

}
