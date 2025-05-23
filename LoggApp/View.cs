using AppLogic.Models;
using AppLogic.Models.Intake.InputModels;
using Microsoft.IdentityModel.Tokens;
using Presentation.MenuState_Enums;

namespace Presentation
{
    public class View
    {
        public MenuHandler MenuHandler { get; }

        public View(MenuHandler menuHandler)
        {
            MenuHandler = menuHandler;
        }




        public async Task<TContext> Start<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            if (sessionContext.MainMenuState != MainMenuState.None)
            {
                sessionContext = await MenuHandler.HandleMainMenuState(sessionContext);

            }

            if (sessionContext.UserMenuState != UserMenuState.None)
            {
                sessionContext = await MenuHandler.HandleUserMenuState(sessionContext);
            }

            if (sessionContext.DayCardMenuState != DayCardMenuState.None)
            {
                sessionContext = await MenuHandler.HandleDayCardMenuState(sessionContext);
            }

            //sessionContext = await MenuHandler.HandleUserMenuState(sessionContext);
            return sessionContext;
        }

        public static string? Input_Location()
        {
            return GetValidUserInput(MenuText.Prompt.CreateUserCity, MenuText.Error.InvalidUserCityInput);

        }

        public static string? Input_TimeOfIntake()
        {
            return GetValidUserInput(MenuText.Prompt.EnterTimeOfIntake, MenuText.Error.InvalidTimeInput);
        }



        public static UserInputModel? Input_User()
        {

            var userName = GetValidUserInput(MenuText.Prompt.CreateUser, MenuText.Error.InvalidUserNameInput);
            if (userName.IsNullOrEmpty())
            {
                return null;
            }
            var city = GetValidUserInput(MenuText.Prompt.CreateUserCity, MenuText.Error.InvalidUserCityInput);
            if (city.IsNullOrEmpty())
            {
                return null;
            }

            UserInputModel userInputModel = new UserInputModel()
            {
                Username = userName!,
                CityName = city!
            };


            return userInputModel;
        }


        public static DayCardInputModel? Input_DayCard<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            string? dateString = GetValidUserInput(MenuText.Prompt.CreateDayCard, MenuText.Error.InvalidDayCardInput)!;

            DayCardInputModel? dayCardInputModel = null;

            if (!dateString.IsNullOrEmpty() && sessionContext.DTO_CurrentUser != null)
            {
                dayCardInputModel = new DayCardInputModel()
                {
                    UserId = sessionContext.DTO_CurrentUser!.Id,
                    Date = DateOnly.Parse(dateString),
                    Lat = sessionContext.DTO_CurrentUser.Lat,
                    Lon = sessionContext.DTO_CurrentUser.Lon

                };
            }

            return dayCardInputModel;
        }

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


        public static string? GetValidUserInput(string prompt, string? errorMessage = null)
        {
            Console.Clear();
            if (!prompt.IsNullOrEmpty())
            {
                Console.WriteLine(prompt);

            }
            string? userInput = null;
            bool validInput;

            do
            {

                var keyPress = Console.ReadKey();

                if (keyPress.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else if (keyPress.Key == ConsoleKey.Enter)
                {
                    return DateOnly.FromDateTime(DateTime.Today).ToString();
                }
                else
                {
                    if (char.IsLetterOrDigit(keyPress.KeyChar))
                    {
                        userInput = keyPress.KeyChar.ToString();
                    }
                }

                userInput += Console.ReadLine()!;


                validInput = !string.IsNullOrWhiteSpace(userInput) && !string.IsNullOrEmpty(userInput);

                if (!validInput)
                {
                    Console.Clear();
                    if (!errorMessage.IsNullOrEmpty())
                    {
                        Console.WriteLine(errorMessage);
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

    }
}

