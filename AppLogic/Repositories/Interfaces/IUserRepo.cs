using AppLogic.Models;

namespace AppLogic.Repositories.Interfaces
{
    public interface IUserRepo : IGenericRepo<User>
    {
        Task<User?> GetUserByIdIncludeAsync(int id);
        Task<User?> GetUserByUsernameIncludeAsync(string username);
        Task<List<User>?> GetAllUsersIncludeAsync();

        Task<User> UpdateUserIncludeAsync(User user);
    }

}
