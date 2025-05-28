using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models;
using AppLogic.Models.Entities;
using AppLogic.Repositories.Interfaces;

namespace AppLogic.Repositories
{
    public class CaffeineDrinkRepo : GenericRepo<CaffeineDrink>, ICaffeineDrinkRepo
    {
        private readonly LoggAppContext _dbContext;

        public CaffeineDrinkRepo(LoggAppContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        
    }
}
