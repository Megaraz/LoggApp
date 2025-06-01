using System.Text.Json;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Entities;
using AppLogic.Models.InputModels;
using AppLogic.Repositories;
using AppLogic.Repositories.Interfaces;
using AppLogic.Services.Interfaces;

namespace AppLogic.Services
{
    /// <summary>
    /// Service for managing user accounts, including registration, retrieval, updating, and deletion of user information.
    /// </summary>
    public class UserService : IUserService
    {

        private readonly IUserRepo _userRepository;

        public UserService(IUserRepo userRepo)
        {
            _userRepository = userRepo;
        }


        public async Task<UserDetailed> RegisterNewUserAsync(UserInputModel inputModel)
        {

            User newUser = new User(inputModel);

            newUser = await _userRepository.CreateAsync(newUser);

            return new UserDetailed(newUser);

        }

        public async Task<List<UserSummary>?> GetAllUsersIncludeAsync()
        {
            List<User>? users = await _userRepository.GetAllUsersIncludeAsync();

            if (users == null)
            {
                return null;
            }
            
            return users.Select(user => new UserSummary(user)).OrderBy(u => u.Username).ToList();

        }

        public async Task<UserDetailed?> GetUserByIdIncludeAsync(int id)
        {
            var user = await _userRepository.GetUserByIdIncludeAsync(id);

            if (user == null) return null;

            return new UserDetailed(user);
        }
        public async Task<UserDetailed?> GetUserByUsernameIncludeAsync(string username)
        {
            var user = await _userRepository.GetUserByUsernameIncludeAsync(username);

            if ( user == null ) return null;

            return new UserDetailed(user);
        }

        public async Task<UserDetailed> UpdateUserAsync(int userId, UserInputModel inputModel)
        {
            User user = new User(inputModel)
            {
                Id = userId,
            };
            user = await _userRepository.UpdateUserIncludeAsync(user);

            return new UserDetailed(user);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await _userRepository.DeleteAsync(userId);
        }
    }
}
