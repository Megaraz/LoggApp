using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Entities;

namespace AppLogic.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for managing caffeine drink entities, providing methods for CRUD operations and specific updates.
    /// </summary>
    public interface ICaffeineDrinkRepo : IGenericRepo<CaffeineDrink>
    {
        Task<CaffeineDrink> UpdateCaffeineDrinkAsync(CaffeineDrink updatedCaffeineDrink);
    }
}
