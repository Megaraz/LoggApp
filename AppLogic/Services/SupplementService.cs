using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Repositories.Interfaces;
using AppLogic.Services.Interfaces;

namespace AppLogic.Services
{
    public class SupplementService : ISupplementService
    {
        private readonly ISupplementRepo _supplementRepo;

        public SupplementService(ISupplementRepo supplementRepo)
        {
            _supplementRepo = supplementRepo;
        }
    }
}
