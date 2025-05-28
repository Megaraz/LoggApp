using AppLogic.Models.InputModels;
using Microsoft.IdentityModel.Tokens;
using Presentation.MenuState_Enums;

namespace Presentation
{
    public class View
    {
        public MainMenuHandler MainMenuHandler { get; }
        public UserMenuHandler UserMenuHandler { get; }
        public DayCardMenuHandler DayCardMenuHandler { get; }

        public IntakeMenuHandler IntakeMenuHandler { get; set; }

        public View(MainMenuHandler mainMenuHandler, UserMenuHandler userMenuHandler, DayCardMenuHandler menuHandler, IntakeMenuHandler intakeMenuHandler)
        {
            MainMenuHandler = mainMenuHandler;
            UserMenuHandler = userMenuHandler;
            DayCardMenuHandler = menuHandler;
            IntakeMenuHandler = intakeMenuHandler;
        }




        public async Task<TContext> Start<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            if (sessionContext.MainMenuState != MainMenuState.None)
            {
                sessionContext = await MainMenuHandler.HandleMenuState(sessionContext);
            }

            if (sessionContext.UserMenuState != UserMenuState.None)
            {
                sessionContext = await UserMenuHandler.HandleMenuState(sessionContext);
            }

            if (sessionContext.DayCardMenuState != DayCardMenuState.None)
            {
                sessionContext = await DayCardMenuHandler.HandleMenuState(sessionContext);
            }

            if (sessionContext.IntakeMenuState != IntakeMenuState.None)
            {
                sessionContext = await IntakeMenuHandler.HandleMenuState(sessionContext);
            }

            return sessionContext;
        }

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

        public static string? Input_TimeOfIntake(string? header = null)
        {
            return GetValidUserInput(header, MenuText.Prompt.EnterTimeOfIntake, MenuText.Error.InvalidTimeInput);
        }

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



        //public static string GetDTOValuesAsRow<T>(T dto, string separator) where T : class
        //{
        //    foreach (var value in dto)
        //    {

        //    }
        //}

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




        public static void DisplayMenu(List<string> currentMenu, ref int CurrentMenuIndex, string? currentPrompt, string mainHeader, string? subHeader = null, string? errorMessage = null)
        {
            Console.Clear();


            // Write MainHeader
            if (!mainHeader.IsNullOrEmpty())
            {
                Console.WriteLine(mainHeader + Environment.NewLine);
            }
            if (!currentPrompt.IsNullOrEmpty())
            {
                Console.WriteLine(currentPrompt + '\n');
            }

            // Write Error
            if (!errorMessage.IsNullOrEmpty())
            {
                Console.WriteLine(errorMessage + '\n');

            }
            // Write SubHeader
            if (!subHeader.IsNullOrEmpty())
            {
                Console.WriteLine(subHeader);

            }

            if (currentMenu != null && currentMenu.Count > 0)
            {
                if (CurrentMenuIndex > currentMenu.Count - 1)
                {
                    CurrentMenuIndex = 0;
                }
                if (CurrentMenuIndex < 0)
                {
                    CurrentMenuIndex = currentMenu.Count - 1;
                }

                for (int i = 0; i < currentMenu.Count; i++)
                {

                    var item = currentMenu[i];

                    if (item == MenuText.NavOption.Exit || item == MenuText.NavOption.GetTodaysWeather)
                    {
                        item = "\n" + item;
                    }

                    if (CurrentMenuIndex == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;

                        Console.WriteLine(item);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(item);
                    }
                }

            }
        }

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
    }



}

