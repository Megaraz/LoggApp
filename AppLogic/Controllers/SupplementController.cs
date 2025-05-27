using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Services.Interfaces;

namespace AppLogic.Controllers
{
    public class SupplementController
    {
        private ISupplementService supplementService;

        public SupplementController(ISupplementService supplementService)
        {
            this.supplementService = supplementService;
        }
    }
}
