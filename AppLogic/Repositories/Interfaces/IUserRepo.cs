using AppLogic.Models.Entities;

namespace AppLogic.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for managing user accounts, including methods for retrieving, updating, and deleting user information.
    /// </summary>
    public interface IUserRepo : IGenericRepo<User>
    {
        Task<User?> GetUserByIdIncludeAsync(int id);
        Task<User?> GetUserByUsernameIncludeAsync(string username);
        Task<List<User>?> GetAllUsersIncludeAsync();

        Task<User> UpdateUserIncludeAsync(User user);
    }

}
