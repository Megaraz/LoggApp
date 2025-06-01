using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Entities;
using AppLogic.Models.Enums;
using AppLogic.Models.InputModels;
using Microsoft.IdentityModel.Tokens;

namespace Presentation.Input
{
    internal class ConsoleInput
    {

        #region Input Methods
        // Takes and validates user input for location.
        public static string? Input_Location(string? header = null, bool newLocation = false)
        {
            string prompt;
            if (newLocation)
            {
                prompt = MenuText.Prompt.EnterNewUserLocation;
            }
            else
            {
                prompt = MenuText.Prompt.EnterUserLocation;
            }
            return GetValidUserInput(header, prompt, MenuText.Error.InvalidUserCityInput);

        }
        // Takes and validates user input for time of intake.
        public static string? Input_TimeOfIntake(string? header = null)
        {
            return GetValidUserInput(header, MenuText.Prompt.EnterTimeOfIntake, MenuText.Error.InvalidTimeInput);
        }

        // Takes and validates user input for username.
        public static string? Input_Username(string? header = null, bool newUsername = false)
        {
            string prompt;
            if (newUsername)
            {
                prompt = MenuText.Prompt.EnterNewUserName;
            }
            else
            {
                prompt = MenuText.Prompt.EnterUserName;
            }
            return GetValidUserInput(header, prompt, MenuText.Error.InvalidUserNameInput);

        }

        // Takes and validates user input for full UserInputModel.
        public static UserInputModel? Input_User(string? header = null)
        {

            var userName = Input_Username(header);
            if (userName.IsNullOrEmpty())
            {
                return null;
            }
            var city = Input_Location(header);
            if (city.IsNullOrEmpty())
            {
                return null;
            }

            UserInputModel userInputModel = new UserInputModel(userName!)
            {
                CityName = city!
            };


            return userInputModel;
        }

        // Takes- and validates user input for full DayCardInputModel.
        public static DayCardInputModel? Input_DayCard<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            string? dateString = GetValidUserInput(null, MenuText.Prompt.CreateDayCard, MenuText.Error.InvalidDayCardInput)!;

            DayCardInputModel? dayCardInputModel = null;

            if (!dateString.IsNullOrEmpty() && sessionContext.CurrentUser != null)
            {
                dayCardInputModel = new DayCardInputModel()
                {
                    UserId = sessionContext.CurrentUser!.Id,
                    Date = DateOnly.Parse(dateString),
                    Lat = sessionContext.CurrentUser.Lat,
                    Lon = sessionContext.CurrentUser.Lon

                };
            }

            return dayCardInputModel;
        }

        // Takes- and validates user input for full SleepInputModel.
        public static SleepInputModel Input_Sleep()
        {
            var model = new SleepInputModel();
            string input;

            Console.Clear();

            // Read SleepEnd (optional)
            Console.Write("Enter SleepEnd (HH:mm) (leave empty for null): ");
            input = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(input) && DateTime.TryParseExact(input, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime sleepEnd))
            {
                model.SleepEnd = sleepEnd;
            }

            // Read TotalSleepTime (optional) - format: HH:mm:ss
            Console.Write("Enter TotalSleepTime (HH:mm) (leave empty for null): ");
            input = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(input) && TimeSpan.TryParse(input, out TimeSpan totalSleepTime))
            {
                model.TotalSleepTime = totalSleepTime;
            }

            // Read DeepSleepDuration (optional)
            Console.Write("Enter DeepSleepDuration (HH:mm) (leave empty for null): ");
            input = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(input) && TimeSpan.TryParse(input, out TimeSpan deepSleep))
            {
                model.DeepSleepDuration = deepSleep;
            }

            // Read LightSleepDuration (optional)
            Console.Write("Enter LightSleepDuration (HH:mm) (leave empty for null): ");
            input = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(input) && TimeSpan.TryParse(input, out TimeSpan lightSleep))
            {
                model.LightSleepDuration = lightSleep;
            }

            // Read RemSleepDuration (optional)
            Console.Write("Enter RemSleepDuration (HH:mm) (leave empty for null): ");
            input = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(input) && TimeSpan.TryParse(input, out TimeSpan remSleep))
            {
                model.RemSleepDuration = remSleep;
            }

            // Read SleepScore (optional)
            Console.Write("Enter SleepScore (leave empty for null): ");
            input = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int score))
            {
                model.SleepScore = score;
            }

            // Read TimesWokenUp (optional)
            Console.Write("Enter TimesWokenUp (leave empty for null): ");
            input = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int times))
            {
                model.TimesWokenUp = times;
            }

            // Read AvgBPM (optional)
            Console.Write("Enter AvgBPM (leave empty for null): ");
            input = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int avgBpm))
            {
                model.AvgBPM = avgBpm;
            }

            // Read Avg02 (optional)
            Console.Write("Enter Avg02 (leave empty for null): ");
            input = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int avg02))
            {
                model.Avg02 = avg02;
            }

            // Read AvgBreathsPerMin (optional)
            Console.Write("Enter AvgBreathsPerMin (leave empty for null): ");
            input = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int avgBreaths))
            {
                model.AvgBreathsPerMin = avgBreaths;
            }

            // Read PerceivedSleepQuality (optional)
            Console.Write("Enter PerceivedSleepQuality (Poor, Fair, Good, VeryGood, Excellent) (leave empty for null): ");
            input = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(input) && Enum.TryParse(typeof(PerceivedSleepQuality), input, true, out object quality))
            {
                model.PerceivedSleepQuality = (PerceivedSleepQuality)quality;
            }

            return model;
        }
        // Takes- and validates user input for full ExerciseInputModel
        public static ExerciseInputModel Input_Exercise(ExerciseInputModel inputModel)
        {

            Console.Write("Enter ExerciseType (Run, Walk, Stretch, Strength): ");
            var exerciseTypeInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(exerciseTypeInput))
                inputModel.ExerciseType = Enum.Parse<ExerciseType>(exerciseTypeInput, true);

            Console.Write("Enter TrainingLoad (number): ");
            var trainingLoadInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(trainingLoadInput))
                inputModel.TrainingLoad = int.Parse(trainingLoadInput);

            Console.Write("Enter AvgHeartRate (number): ");
            var avgHeartRateInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(avgHeartRateInput))
                inputModel.AvgHeartRate = int.Parse(avgHeartRateInput);

            Console.Write("Enter ClockIntensity (Easy, Intense, Aerobic, Anaerobic, VO2Max): ");
            var clockIntensityInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(clockIntensityInput))
                inputModel.ClockIntensity = Enum.Parse<CLOCK_Intensity>(clockIntensityInput, true);

            Console.Write("Enter ActiveKcalBurned (number): ");
            var activeKcalInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(activeKcalInput))
                inputModel.ActiveKcalBurned = int.Parse(activeKcalInput);

            Console.Write("Enter Distance (number): ");
            var distanceInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(distanceInput))
                inputModel.Distance = int.Parse(distanceInput);

            Console.Write("Enter AvgKmTempo (number): ");
            var avgKmTempoInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(avgKmTempoInput))
                inputModel.AvgKmTempo = int.Parse(avgKmTempoInput);

            Console.Write("Enter Steps (number): ");
            var stepsInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(stepsInput))
                inputModel.Steps = int.Parse(stepsInput);

            Console.Write("Enter AvgStepLength (number): ");
            var avgStepLengthInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(avgStepLengthInput))
                inputModel.AvgStepLength = int.Parse(avgStepLengthInput);

            Console.Write("Enter AvgStepPerMin (number): ");
            var avgStepPerMinInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(avgStepPerMinInput))
                inputModel.AvgStepPerMin = int.Parse(avgStepPerMinInput);

            return inputModel;
        }
        // Takes- and validates user input for ActivityTime
        internal static (TimeOnly? timeOf, TimeOnly? endTime, TimeSpan? duration) Input_ActivityTime()
        {
            TimeOnly? timeOf = null;
            TimeOnly? endTime = null;
            TimeSpan? duration = null;

            Console.Clear();

            Console.Write("Enter TimeOf (HH:mm): ");
            var timeOfInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(timeOfInput))
                timeOf = TimeOnly.Parse(timeOfInput);

            Console.Write("Enter EndTime (HH:mm): ");
            var endTimeInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(endTimeInput))
                endTime = TimeOnly.Parse(endTimeInput);

            Console.Write("Enter Duration (e.g., hh:mm:ss): ");
            var durationInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(durationInput))
                duration = TimeSpan.Parse(durationInput);

            if ((timeOf != null && endTime != null) && duration is null)
            {
                duration = endTime - timeOf;
            }

            return (timeOf, endTime, duration);

        }
        // Takes user input for confirmation(mainly for delete actions).
        internal static bool Input_Confirmation(string? prompt = null)
        {
            ConsoleKey keyPress;
            bool validInput;
            bool isConfirm;

            Console.Clear();
            if (!prompt.IsNullOrEmpty())
            {
                Console.WriteLine(prompt);

            }

            do
            {
                keyPress = Console.ReadKey(true).Key;

                validInput = keyPress == ConsoleKey.Y || keyPress == ConsoleKey.N || keyPress == ConsoleKey.Enter || keyPress == ConsoleKey.Escape;

                isConfirm = keyPress == ConsoleKey.Y || keyPress == ConsoleKey.Enter;


            } while (!validInput);

            return isConfirm;

        }
        // Main method to navigate/handle indexing for menu navigation.
        public static ConsoleKey InputToMenuIndex(ref int currentMenuIndex)
        {

            var keyPress = Console.ReadKey(true).Key;
            switch (keyPress)
            {
                case ConsoleKey.DownArrow:
                    ++currentMenuIndex;
                    break;
                case ConsoleKey.UpArrow:
                    --currentMenuIndex;
                    break;
                case ConsoleKey.LeftArrow:
                    --currentMenuIndex;
                    break;
                case ConsoleKey.RightArrow:
                    ++currentMenuIndex;
                    break;
                case ConsoleKey.Escape:
                    break;
                case ConsoleKey.Enter:
                    break;
                default: break;
            }
            return keyPress;
        }

        // Gets valid user input from the console, ensuring it is not null or empty.
        public static string? GetValidUserInput(string? header = null, string? prompt = null, string? errorMessage = null)
        {
            string? userInput = null;
            bool validInput;

            do
            {
                Console.Clear();

                if (!header.IsNullOrEmpty())
                {
                    Console.WriteLine(header);

                }

                if (!prompt.IsNullOrEmpty())
                {
                    Console.WriteLine(prompt);

                }

                var keyPress = Console.ReadKey();

                if (keyPress.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else if (char.IsLetterOrDigit(keyPress.KeyChar))
                {
                    userInput = keyPress.KeyChar.ToString();
                    userInput += Console.ReadLine()!;
                }

                validInput = !string.IsNullOrWhiteSpace(userInput) && !string.IsNullOrEmpty(userInput);

                if (!validInput)
                {
                    Console.Clear();
                    if (!errorMessage.IsNullOrEmpty())
                    {
                        Console.WriteLine(errorMessage);
                        Thread.Sleep(1200);
                    }
                }
            }
            while (!validInput);


            return userInput;
        }

        #endregion
    }
}
