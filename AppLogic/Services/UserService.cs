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

        private readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }


        public async Task<UserDetailed> RegisterNewUserAsync(UserInputModel inputModel)
        {

            User newUser = new User(inputModel);

            newUser = await _userRepo.CreateAsync(newUser);

            return new UserDetailed(newUser);

        }

        public async Task<List<UserSummary>?> GetAllUsersIncludeAsync()
        {
            List<User>? users = await _userRepo.GetAllUsersIncludeAsync();

            if (users == null)
            {
                return null;
            }
            
            return users.Select(user => new UserSummary(user)).OrderBy(u => u.Username).ToList();

        }

        public async Task<UserDetailed?> GetUserByIdIncludeAsync(int id)
        {
            var user = await _userRepo.GetUserByIdIncludeAsync(id);

            if (user == null) return null;

            return new UserDetailed(user);
        }
        public async Task<UserDetailed?> GetUserByUsernameIncludeAsync(string username)
        {
            var user = await _userRepo.GetUserByUsernameIncludeAsync(username);

            if ( user == null ) return null;

            return new UserDetailed(user);
        }

        public async Task<UserDetailed> UpdateUserAsync(int userId, UserInputModel inputModel)
        {
            User user = new User(inputModel)
            {
                Id = userId,
            };
            user = await _userRepo.UpdateUserIncludeAsync(user);

            return new UserDetailed(user);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await _userRepo.DeleteAsync(userId);
        }

        public async Task<UserDetailed?> GetUserDetailedWithStatsAsync(string username)
        {
            // Get the UserDetailed (existing mapping from user entity)
            User? user = await _userRepo.GetUserByUsernameIncludeAsync(username);

            if (user == null)
            {
                return null;
            }
            // Get up to 7 recent DayCards
            var last7 = await _userRepo.GetLast7DayCardsAsync(user.Id);

            UserDetailed userDetailed = new UserDetailed(user);

            userDetailed.SleepLastWeek = CalculateSleepRating(last7);
            userDetailed.WellnessLastWeek = CalculateWellnessRating(last7);
            userDetailed.ExerciseLastWeek = CalculateExerciseRating(last7);
            userDetailed.CaffeineLastWeek = CalculateCaffeineRating(last7);

            return userDetailed;
        }
        public async Task<UserDetailed?> GetUserDetailedWithStatsAsync(int userId)
        {
            // Get the UserDetailed (existing mapping from user entity)
            User? user = await _userRepo.GetUserByIdIncludeAsync(userId);

            if (user == null)
            {
                return null;
            }
            // Get up to 7 recent DayCards
            var last7 = await _userRepo.GetLast7DayCardsAsync(userId);

            UserDetailed userDetailed = new UserDetailed(user);

            userDetailed.SleepLastWeek = CalculateSleepRating(last7);
            userDetailed.WellnessLastWeek = CalculateWellnessRating(last7);
            userDetailed.ExerciseLastWeek = CalculateExerciseRating(last7);
            userDetailed.CaffeineLastWeek = CalculateCaffeineRating(last7);

            return userDetailed;
        }

        private string CalculateSleepRating(List<DayCard> dayCards)
        {
            // Calculate sleep rating based on total sleep time in hours:
            // 4-6 hours (and below) is very poor, 6-7 is OK, 7-8 is good, 8+ is excellent
            var ratings = dayCards
                .Select(dc => dc.Sleep?.TotalSleepTime)
                .Where(r => r.HasValue)
                .Select(r => r.Value.TotalHours)
                .ToList();

            if (!ratings.Any()) return "N/A";

            double avg = ratings.Average();

            if (avg < 6)
            {
                return "Very poor";
            }
            else if (avg < 7)
            {
                return "OK";
            }
            else if (avg < 8)
            {
                return "Good";
            }
            else
            {
                return "Excellent";
            }
        }

        private string CalculateWellnessRating(List<DayCard> dayCards)
        {
            // Assume each DayCard has a collection of WellnessCheckIns with MoodLevel convertible to int
            var ratings = dayCards
                .SelectMany(dc => dc.WellnessCheckIns ?? new List<WellnessCheckIn>())
                .Select(wc => wc.MoodLevel.HasValue ? (double)wc.MoodLevel.Value : (double?)null)
                .Where(r => r.HasValue)
                .Select(r => r.Value)
                .ToList();

            if (!ratings.Any()) return "N/A";

            double avg = ratings.Average();
            return MapScoreToRating(avg);
        }

        private string CalculateExerciseRating(List<DayCard> dayCards)
        {
            // Assume each DayCard has Exercises with PerceivedIntensity as an int (1-5)
            var ratings = dayCards
                .SelectMany(dc => dc.Exercises ?? new List<Exercise>())
                .Select(e => e.PerceivedIntensity.HasValue ? (double)e.PerceivedIntensity.Value : (double?)null)
                .Where(r => r.HasValue)
                .Select(r => r.Value)
                .ToList();

            if (!ratings.Any()) return "N/A";

            double avg = ratings.Average();
            return MapScoreToRating(avg);
        }

        private string CalculateCaffeineRating(List<DayCard> dayCards)
        {
            // Assume each DayCard has CaffeineDrinks, and lower caffeine is 'better'
            var ratings = dayCards
                .SelectMany(dc => dc.CaffeineDrinks ?? new List<CaffeineDrink>())
                .Select(c => c.EstimatedMgCaffeine.HasValue ? (double)c.EstimatedMgCaffeine.Value : (double?)null)
                .Where(r => r.HasValue)
                .Select(r => r.Value)
                .ToList();

            if (!ratings.Any()) return "N/A";

            double avg = ratings.Average();
            // Dummy transformation: lower average mg means better rating.
            // Here we invert a simple scale: assuming 0-500 mg range.
            double score = 5 - (avg / 100.0);
            if (score < 1) score = 1;
            if (score > 5) score = 5;
            return MapScoreToRating(score);
        }

        private string MapScoreToRating(double score)
        {
            // Score is expected to be between 1 and 5
            if (score < 1.5) return "Very poor";
            else if (score < 2.5) return "Poor";
            else if (score < 3.5) return "Medium";
            else if (score < 4.5) return "Good";
            else return "Very good";
        }

        
    }
}
