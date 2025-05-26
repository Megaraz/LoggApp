using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Intake;
using AppLogic.Repositories.Interfaces;

namespace AppLogic.Repositories
{
    public class SupplementRepo : GenericRepo<Supplement>, ISupplementRepo
    {
        public SupplementRepo(LoggAppContext dbContext) : base(dbContext)
        {
        }


    }
}
