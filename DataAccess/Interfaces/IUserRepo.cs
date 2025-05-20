using AppLogic.Models;

namespace AppLogic.Interfaces
{
    public interface IUserRepo<T> where T : User, new()
    {
    }
}
