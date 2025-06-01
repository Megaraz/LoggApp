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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Presentation.Input
{
    /// <summary>
    /// Handles user input from the console, including validation and formatting for various input types.
    /// </summary>
    internal class ConsoleInput
    {
        #region Input Methods
        /// <summary>
        /// Takes and validates user input for location.
        /// </summary>
        /// <param name="header"></param>
        /// <param name="newLocation"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Takes and validates user input for time of intake.
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public static string? Input_TimeOfIntake(string? header = null)
        {
            return GetValidUserInput(header, MenuText.Prompt.EnterTimeOfIntake, MenuText.Error.InvalidTimeInput);
        }


        /// <summary>
        /// Takes and validates user input for time of check-in.
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public static string? Input_TimeOfCheckIn(string? header = null)
        {
            return GetValidUserInput(header, MenuText.Prompt.EnterTimeOfCheckIn, MenuText.Error.InvalidTimeInput);
        }


        /// <summary>
        /// Takes and validates user input for username.
        /// </summary>
        /// <param name="header"></param>
        /// <param name="newUsername"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Takes and validates user input for full UserInputModel.
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Takes- and validates user input for full DayCardInputModel.
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="sessionContext"></param>
        /// <returns></returns>
        public static DayCardInputModel? Input_DayCard<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            string? dateString = GetValidUserInput(null, MenuText.Prompt.CreateDayCard, MenuText.Error.InvalidDayCardInput)!;

            if (!DateOnly.TryParse(dateString, out DateOnly parsedDate))
            {
                Console.Clear();
                Console.WriteLine(MenuText.Error.InvalidDayCardInput);
                Thread.Sleep(1500);
                return null;
            }

            DayCardInputModel? dayCardInputModel = null;

            if (!dateString.IsNullOrEmpty() && sessionContext.CurrentUser != null)
            {
                dayCardInputModel = new DayCardInputModel()
                {
                    UserId = sessionContext.CurrentUser!.Id,
                    Date = parsedDate,
                    Lat = sessionContext.CurrentUser.Lat,
                    Lon = sessionContext.CurrentUser.Lon

                };
            }

            return dayCardInputModel;
        }


        /// <summary>
        /// Takes- and validates user input for full SleepInputModel.
        /// </summary>
        /// <returns></returns>
        public static SleepInputModel? Input_Sleep()
        {
            var model = new SleepInputModel();
            string? input;

            Console.Clear();

            // Read SleepEnd (optional)
            Console.Write("Enter SleepEnd (HH:mm) (leave empty for null): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!DateTime.TryParseExact(input, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime sleepEnd))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid SleepEnd input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                model.SleepEnd = sleepEnd;
            }

            // Read TotalSleepTime (optional) - format: HH:mm:ss
            Console.Write("Enter TotalSleepTime (HH:mm:ss) (leave empty for null): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!TimeSpan.TryParse(input, out TimeSpan totalSleepTime))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid TotalSleepTime input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                model.TotalSleepTime = totalSleepTime;
            }

            // Read DeepSleepDuration (optional)
            Console.Write("Enter DeepSleepDuration (HH:mm:ss) (leave empty for null): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!TimeSpan.TryParse(input, out TimeSpan deepSleep))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid DeepSleepDuration input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                model.DeepSleepDuration = deepSleep;
            }

            // Read LightSleepDuration (optional)
            Console.Write("Enter LightSleepDuration (HH:mm:ss) (leave empty for null): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!TimeSpan.TryParse(input, out TimeSpan lightSleep))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid LightSleepDuration input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                model.LightSleepDuration = lightSleep;
            }

            // Read RemSleepDuration (optional)
            Console.Write("Enter RemSleepDuration (HH:mm:ss) (leave empty for null): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!TimeSpan.TryParse(input, out TimeSpan remSleep))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid RemSleepDuration input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                model.RemSleepDuration = remSleep;
            }

            // Read SleepScore (optional)
            Console.Write("Enter SleepScore (leave empty for null): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!int.TryParse(input, out int score))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid SleepScore input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                model.SleepScore = score;
            }

            // Read TimesWokenUp (optional)
            Console.Write("Enter TimesWokenUp (leave empty for null): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!int.TryParse(input, out int times))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid TimesWokenUp input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                model.TimesWokenUp = times;
            }

            // Read AvgBPM (optional)
            Console.Write("Enter AvgBPM (leave empty for null): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!int.TryParse(input, out int avgBpm))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid AvgBPM input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                model.AvgBPM = avgBpm;
            }

            // Read Avg02 (optional)
            Console.Write("Enter Avg02 (leave empty for null): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!int.TryParse(input, out int avg02))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Avg02 input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                model.Avg02 = avg02;
            }

            // Read AvgBreathsPerMin (optional)
            Console.Write("Enter AvgBreathsPerMin (leave empty for null): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!int.TryParse(input, out int avgBreaths))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid AvgBreathsPerMin input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                model.AvgBreathsPerMin = avgBreaths;
            }

            // Read PerceivedSleepQuality (optional)
            Console.Write("Enter PerceivedSleepQuality (Poor, Fair, Good, VeryGood, Excellent) (leave empty for null): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!Enum.TryParse(typeof(PerceivedSleepQuality), input, true, out object quality))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid PerceivedSleepQuality input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                model.PerceivedSleepQuality = (PerceivedSleepQuality)quality;
            }

            return model;
        }


        /// <summary>
        /// Takes- and validates user input for full ExerciseInputModel
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        public static ExerciseInputModel? Input_Exercise(ExerciseInputModel inputModel)
        {
            string? input;

            Console.Write("Enter ExerciseType (Run, Walk, Stretch, Strength): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!Enum.TryParse<ExerciseType>(input, true, out ExerciseType exType))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid ExerciseType input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                inputModel.ExerciseType = exType;
            }

            Console.Write("Enter TrainingLoad (number): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!int.TryParse(input, out int trainingLoad))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid TrainingLoad input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                inputModel.TrainingLoad = trainingLoad;
            }

            Console.Write("Enter AvgHeartRate (number): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!int.TryParse(input, out int avgHeart))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid AvgHeartRate input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                inputModel.AvgHeartRate = avgHeart;
            }

            Console.Write("Enter ActiveKcalBurned (int): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!int.TryParse(input, out int activeKcal))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid ActiveKcalBurned input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                inputModel.ActiveKcalBurned = activeKcal;
            }

            Console.Write("Enter Distance in Km (decimal): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!double.TryParse(input, out double distance))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Distance input.");
                    Thread.Sleep(1500);
                    return null;
                }
                inputModel.DistanceInKm = distance;
            }

            Console.Write("Enter AvgKmTempo (mm:ss): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!TimeSpan.TryParse(input, out TimeSpan avgKmTempo))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid AvgKmTempo input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                inputModel.AvgKmTempo = avgKmTempo;
            }

            Console.Write("Enter Steps (int): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!int.TryParse(input, out int steps))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Steps input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                inputModel.Steps = steps;
            }

            Console.Write("Enter AvgStepLength in cm (int): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!int.TryParse(input, out int avgStepLength))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid AvgStepLength input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                inputModel.AvgStepLengthInCm = avgStepLength;
            }

            Console.Write("Enter AvgStepPerMin (int): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!int.TryParse(input, out int avgStepPerMin))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid AvgStepPerMin input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                inputModel.AvgStepPerMin = avgStepPerMin;
            }

            return inputModel;
        }


        /// <summary>
        /// Takes- and validates user input for ActivityTime
        /// </summary>
        /// <returns></returns>
        internal static (TimeOnly? timeOf, TimeOnly? endTime, TimeSpan? duration)? Input_ActivityTime()
        {
            TimeOnly? timeOf = null;
            TimeOnly? endTime = null;
            TimeSpan? duration = null;

            string? input;

            Console.Clear();

            Console.Write("Enter TimeOf (HH:mm): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!TimeOnly.TryParse(input, out TimeOnly tOf))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid TimeOf input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                timeOf = tOf;
            }

            Console.Write("Enter EndTime (HH:mm): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!TimeOnly.TryParse(input, out TimeOnly eTime))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid EndTime input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                endTime = eTime;
            }

            Console.Write("Enter Duration (e.g., hh:mm): ");
            input = ReadLineWithEscape();
            if (input == null) return null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!TimeSpan.TryParse(input, out TimeSpan dur))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Duration input.");
                    System.Threading.Thread.Sleep(1500);
                    return null;
                }
                duration = dur;
            }

            if ((timeOf != null && endTime != null) && duration is null)
            {
                duration = endTime - timeOf;
            }

            return (timeOf, endTime, duration);
        }


        /// <summary>
        /// Takes user input for confirmation(mainly for delete actions).
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Processes user input to navigate through a menu, updating the current menu index based on arrow key presses.
        /// </summary>
        /// <param name="currentMenuIndex"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Prompts the user for input with a header and prompt, validates the input, and returns it.
        /// </summary>
        /// <param name="header"></param>
        /// <param name="prompt"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static string? GetValidUserInput(string? header = null, string? prompt = null, string? errorMessage = null)
        {
            string? userInput;
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

                userInput = ReadLineWithEscape();
                if (userInput == null) return null; // User pressed Escape

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


        /// <summary>
        /// Reads a line of input from the console, allowing for Escape to cancel input.
        /// </summary>
        /// <returns></returns>
        private static string? ReadLineWithEscape()
        {
            var input = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Escape)
                    return null; // User pressed Escape

                if (key.Key == ConsoleKey.Enter)
                    break;
                // Handle Backspace(erase last character)
                if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input.Length--;
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    input.Append(key.KeyChar);
                    Console.Write(key.KeyChar);
                }
            }
            Console.WriteLine();
            return input.ToString();
        }

        #endregion
    }
}
