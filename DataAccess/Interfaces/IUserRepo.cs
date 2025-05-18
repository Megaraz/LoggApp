using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace AppLogic.Interfaces
{
    public interface IUserRepo<T> where T : User, new()
    {
    }
}
