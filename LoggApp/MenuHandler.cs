using System.Globalization;
using AppLogic.Controllers;
using AppLogic.Models;
using AppLogic.Models.DTOs;
using AppLogic.Models.Intake.Enums;
using AppLogic.Models.Intake.InputModels;
using AppLogic.Services;
using Microsoft.IdentityModel.Tokens;
using Presentation.MenuState_Enums;

namespace Presentation
{
    public class MenuHandler
    {

        private readonly Controller _controller;

        public MenuHandler(Controller controller)
        {
            _controller = controller;
        }


        public async Task<TContext> HandleDayCardMenuState<TContext>(TContext sessionContext) where TContext : SessionContext
        {

            switch (sessionContext.DayCardMenuState)
            {
                case DayCardMenuState.Overview:
                    SpecificDayCardMenuHandler(sessionContext);
                    break;

                case DayCardMenuState.AllData:
                    //sessionContext = ;
                    break;

                case DayCardMenuState.AirQualityDetails:
                    await AirQualityDetailsMenuHandler(sessionContext);
                    break;

                case DayCardMenuState.PollenDetails:
                    await PollenDetailsMenuHandler(sessionContext);
                    break;

                case DayCardMenuState.WeatherDetails:
                    await WeatherDetailsMenuHandler(sessionContext);
                    break;

                case DayCardMenuState.AddCaffeineDrink:
                    await CaffeineCreateMenuHandler(sessionContext);
                    break;

                case DayCardMenuState.Sleep:
                    //sessionContext = ;
                    break;

                case DayCardMenuState.Supplements:
                    //sessionContext = ;
                    break;

                case DayCardMenuState.Back:
                    ResetMenuStates(sessionContext);
                    sessionContext.UserMenuState = UserMenuState.AllDayCards;
                    break;
            }

            return sessionContext;

        }



        public async Task<TContext> HandleUserMenuState<TContext>(TContext sessionContext) where TContext : SessionContext
        {


            switch (sessionContext.UserMenuState)
            {
                case UserMenuState.AllDayCards:
                    await AllDayCardsMenuHandler(sessionContext);
                    break;

                case UserMenuState.CreateNewDayCard:
                    await CreateDayCardMenuHandler(sessionContext);
                    break;
                case UserMenuState.SearchDayCard:
                    break;

                case UserMenuState.Back:
                    ResetMenuStates(sessionContext);
                    sessionContext.MainMenuState = MainMenuState.SpecificUser;
                    break;
            }

            return sessionContext;

        }


        public async Task<TContext> HandleMainMenuState<TContext>(TContext sessionContext) where TContext : SessionContext
        {

            switch (sessionContext.MainMenuState)
            {
                case MainMenuState.Main:
                    await InitMenuHandler(sessionContext);
                    break;

                case MainMenuState.SpecificUser:
                    SpecificUserMenuHandler(sessionContext);
                    break;

                case MainMenuState.AllUsers:
                    await AllUsersMenuHandler(sessionContext);
                    break;

                case MainMenuState.TodaysWeather:
                    await GetTodaysWeather(sessionContext);
                    break;

                case MainMenuState.Back:
                    goto case MainMenuState.Main;

                case MainMenuState.Exit:
                    break;


            }
            return sessionContext;
        }

        private async Task<TContext> CaffeineCreateMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            string? timeOf = View.Input_TimeOfIntake();

            if (timeOf != null)
            {
                var sizeOfDrinkChoice = GetMenuValue(MenuText.NavOption.s_DrinkSize.ToList(), sessionContext);

                if (sizeOfDrinkChoice != null)
                {

                    CaffeineDrinkInputModel caffeineDrinkInputModel = new CaffeineDrinkInputModel()
                    {
                        TimeOf = TimeOnly.Parse(timeOf)
                    };
                    switch (sizeOfDrinkChoice)
                    {
                        case MenuText.NavOption.DrinkMedium:
                            caffeineDrinkInputModel.SizeOfDrink = SizeOfDrink.Medium;
                            break;

                        case MenuText.NavOption.DrinkSmall:
                            caffeineDrinkInputModel.SizeOfDrink = SizeOfDrink.Small;
                            break;

                        case MenuText.NavOption.DrinkLarge:
                            caffeineDrinkInputModel.SizeOfDrink = SizeOfDrink.Large;
                            break;
                    }

                    sessionContext
                        .DTO_CurrentDayCard!
                            .CaffeineDrinksSummary!
                                .HourlyCaffeineData
                                    .Add
                                    (await _controller
                                        .AddCaffeineDrinkToDayCardAsync
                                        (sessionContext.DTO_CurrentDayCard!.DayCardId,
                                            caffeineDrinkInputModel
                                        )
                                    );

                }
            }
            sessionContext.DayCardMenuState = DayCardMenuState.Overview;

            return sessionContext;
        }



        public async Task<TContext> InitMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            sessionContext.MainHeader = MenuText.Header.InitMenu;

            var initMenuChoice = GetMenuValue(MenuText.NavOption.s_InitMenu.ToList(), sessionContext);

            if (initMenuChoice != null)
            {
                switch (initMenuChoice)
                {
                    case MenuText.NavOption.Login:
                        await Login(sessionContext);
                        break;

                    case MenuText.NavOption.GetAllUsers:
                        await GetAllUsers(sessionContext);
                        break;

                    case MenuText.NavOption.CreateNewUser:
                        await CreateNewUser(sessionContext);
                        break;

                    case MenuText.NavOption.GetTodaysWeather:
                        await GetTodaysWeather(sessionContext);
                        break;

                    case MenuText.NavOption.Exit:
                        sessionContext.MainMenuState = MainMenuState.Exit;
                        break;
                }

            }
            else
            {
                sessionContext.MainMenuState = MainMenuState.Exit;
            }

            return sessionContext;
        }

        private async Task<TContext> GetTodaysWeather<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            // Get basic user info/input
            string? location = View.Input_Location();


            if (!location.IsNullOrEmpty())
            {
                // Get list of locations that match users input location%
                GeoResultResponse geoResultResponse = await _controller.LocationGeoResultList(location!);
                // User chooses a Location from the list of locations
                GeoResult geoResult = GetMenuValue(geoResultResponse.Results, sessionContext);

                if (geoResult != null)
                {
                    string lat = geoResult.Lat?.ToString(CultureInfo.InvariantCulture)!;
                    string lon = geoResult.Lon?.ToString(CultureInfo.InvariantCulture)!;
                    string date = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)!;
                    DTO_AllWeatherData todayWeather = WeatherService.ConvertToDTO(await WeatherService.GetWeatherDataAsync(lat, lon, date));

                    Console.WriteLine(todayWeather.ToString());

                    Console.ReadLine();


                    sessionContext.MainMenuState = MainMenuState.Back;
                }
            }
            else
            {
                sessionContext.MainMenuState = MainMenuState.Back;
            }

            return sessionContext;
        }

        public async Task<TContext> AddCaffeineDrink<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);



            return sessionContext;
        }

        private async Task<TContext> CreateNewUser<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            // Get basic user info/input
            UserInputModel userInputModel = View.Input_User()!;

            if (userInputModel != null)
            {
                // Get list of locations that match users input location
                GeoResultResponse geoResultResponse = await _controller.UserGeoResultList(userInputModel);

                // User chooses a Location from the list of locations
                sessionContext.CurrentPrompt = MenuText.Prompt.CreateUserLocationChoice;
                userInputModel.GeoResult = GetMenuValue(geoResultResponse.Results, sessionContext);


                if (userInputModel.GeoResult != null)
                {
                    // Create a new user with the updated GeoResult, and sets the CurrentUser to the newly created one
                    sessionContext.DTO_CurrentUser = await _controller.CreateNewUserAsync(userInputModel);
                    sessionContext.MainMenuState = MainMenuState.SpecificUser;

                }
            }
            else
            {
                sessionContext.MainMenuState = MainMenuState.Back;
            }
            return sessionContext;
        }

        private async Task<TContext> GetAllUsers<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            List<DTO_AllUser>? allUsers = await _controller.ReadAllUsersAsync()!;

            if (allUsers == null || allUsers.Count == 0)
            {
                sessionContext.ErrorMessage = MenuText.Error.NoUsersFound;
                sessionContext.MainMenuState = MainMenuState.Main;
            }
            else
            {
                sessionContext.MainMenuState = MainMenuState.AllUsers;
                //sessionContext.CurrentMainMenu = MenuText.NavOption.s_AllUserMenu.ToList();
                sessionContext.DTO_AllUsers = allUsers;

            }

            return sessionContext;
        }

        private async Task<TContext> Login<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            string? username = View.GetValidUserInput(MenuText.Prompt.CreateUser, MenuText.Error.InvalidUserNameInput);

            if (username != null)
            {

                DTO_SpecificUser? resultUser = await _controller.ReadUserSingleAsync(username);

                if (resultUser == null)
                {
                    sessionContext.ErrorMessage = MenuText.Error.NoUserFound;
                    sessionContext.MainMenuState = MainMenuState.Main;

                }
                else
                {
                    sessionContext.DTO_CurrentUser = resultUser;
                    sessionContext.MainMenuState = MainMenuState.SpecificUser;
                }
            }
            else
            {
                sessionContext.MainMenuState = MainMenuState.Back;
            }

            return sessionContext;
        }

        public TContext ResetMenuStates<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            sessionContext.CurrentPrompt = string.Empty;
            sessionContext.MainHeader = string.Empty;
            sessionContext.SubHeader = string.Empty;
            sessionContext.ErrorMessage = string.Empty;

            sessionContext.MainMenuState = MainMenuState.None;
            sessionContext.UserMenuState = UserMenuState.None;
            sessionContext.DayCardMenuState = DayCardMenuState.None;
            return sessionContext;
        }

        public async Task<TContext> CreateDayCardMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            //sessionContext.MainHeader = MenuText.Prompt.CreateDayCard;
            DayCardInputModel? dayCardInputModel = View.Input_DayCard(sessionContext);

            if (dayCardInputModel != null)
            {
                sessionContext.DTO_CurrentDayCard = await _controller.CreateNewDayCardAsync(sessionContext.DTO_CurrentUser!.Id, dayCardInputModel);
                sessionContext.DayCardMenuState = DayCardMenuState.Overview;

            }
            else
            {
                sessionContext.UserMenuState = UserMenuState.Back;
            }

            return sessionContext;
        }

        public async Task<TContext> AllUsersMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {

            ResetMenuStates(sessionContext);
            if (sessionContext.DTO_AllUsers == null || sessionContext.DTO_AllUsers.Count == 0)
            {
                sessionContext.ErrorMessage = MenuText.Error.NoUsersFound;
                sessionContext.MainMenuState = MainMenuState.Main;

            }
            else
            {
                sessionContext.MainHeader = MenuText.Header.AllUsers;

                var userChoice = GetMenuValue(sessionContext.DTO_AllUsers, sessionContext);

                if (userChoice != null)
                {
                    sessionContext
                        .DTO_CurrentUser = await _controller.ReadUserSingleAsync(userChoice.Id);

                    sessionContext
                        .MainHeader = MenuText.Header.SpecificUser + $"{sessionContext.DTO_CurrentUser!.ToString()}\n\n\n";

                    sessionContext
                        .MainMenuState = MainMenuState.SpecificUser;
                }
                else
                {
                    sessionContext.MainMenuState = MainMenuState.Back;
                }

            }
            return sessionContext;
        }

        public async Task<TContext> AllDayCardsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {

            ResetMenuStates(sessionContext);
            if (sessionContext.DTO_CurrentUser!.DTO_AllDayCards == null || sessionContext.DTO_CurrentUser.DTO_AllDayCards.Count == 0)
            {
                sessionContext.ErrorMessage = MenuText.Error.NoDayCardsFound;
                sessionContext.UserMenuState = UserMenuState.Back;
            }
            else
            {
                sessionContext.MainHeader = sessionContext.MainHeader = MenuText.Header.SpecificUser + $"{sessionContext.DTO_CurrentUser!.ToString()}";
                sessionContext.SubHeader = MenuText.Header.AllDayCards;

                var dayCardChoice = GetMenuValue(sessionContext!.DTO_CurrentUser!.DTO_AllDayCards!, sessionContext);

                if (dayCardChoice != null)
                {
                    sessionContext.DTO_CurrentDayCard = await _controller.ReadDayCardSingleAsync(dayCardChoice.DayCardId, sessionContext.DTO_CurrentUser.Id);
                    sessionContext.DayCardMenuState = DayCardMenuState.Overview;
                }
                else
                {
                    sessionContext.UserMenuState = UserMenuState.Back;
                }


            }

            return sessionContext;
        }
        public TContext SpecificDayCardMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            sessionContext.MainHeader = MenuText.Header.SpecificUser + $"{sessionContext.DTO_CurrentUser!.ToString()}";
            sessionContext.SubHeader = MenuText.Header.CurrentDayCard + "\n[OVERVIEW]\n" + sessionContext.DTO_CurrentDayCard!.ToString();

            List<string>? specificDayCardMenu = new List<string>()
            {
                MenuText.NavOption.Weather,
                MenuText.NavOption.AirQuality,
                MenuText.NavOption.Pollen,

            };

            if (sessionContext.DTO_CurrentDayCard.SupplementsSummary != null)
            {
                specificDayCardMenu.Add(MenuText.NavOption.Supplements);
            }
            else
            {
                specificDayCardMenu.Add(MenuText.NavOption.AddSupplements);
            }

            if (sessionContext.DTO_CurrentDayCard.CaffeineDrinksSummary != null)
            {
                specificDayCardMenu.Add(MenuText.NavOption.CaffeineDrinks);
            }
            else
            {
                specificDayCardMenu.Add(MenuText.NavOption.AddCaffeine);
            }


            specificDayCardMenu.Add(MenuText.NavOption.Back);


            var specUserChoice = GetMenuValue(specificDayCardMenu, sessionContext);

            if (specUserChoice != null)
            {
                switch (specUserChoice)
                {
                    case MenuText.NavOption.Weather:
                        sessionContext.DayCardMenuState = DayCardMenuState.WeatherDetails;
                        break;

                    case MenuText.NavOption.AirQuality:
                        sessionContext.DayCardMenuState = DayCardMenuState.AirQualityDetails;
                        break;

                    case MenuText.NavOption.Pollen:
                        sessionContext.DayCardMenuState = DayCardMenuState.PollenDetails;
                        break;

                    case MenuText.NavOption.Supplements:
                        sessionContext.DayCardMenuState = DayCardMenuState.Supplements;
                        break;

                    case MenuText.NavOption.AddCaffeine:
                        sessionContext.DayCardMenuState = DayCardMenuState.AddCaffeineDrink;
                        break;

                    case MenuText.NavOption.CaffeineDrinks:
                        sessionContext.DayCardMenuState = DayCardMenuState.CaffeineDrinkDetails;
                        break;

                    case MenuText.NavOption.Exercise:
                        sessionContext.DayCardMenuState = DayCardMenuState.Exercise;
                        break;

                    case MenuText.NavOption.ComputerActivity:
                        sessionContext.DayCardMenuState = DayCardMenuState.ComputerActivity;
                        break;

                    case MenuText.NavOption.Sleep:
                        break;

                    case MenuText.NavOption.Back:
                        sessionContext.DayCardMenuState = DayCardMenuState.Back;
                        break;

                }

            }
            else
            {
                sessionContext.DayCardMenuState = DayCardMenuState.Back;
            }

            return sessionContext;

        }
        private async Task<TContext> WeatherDetailsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            Console.Clear();
            Console.WriteLine(MenuText.Header.SpecificUser + $"{sessionContext.DTO_CurrentUser!.ToString()}");
            Console.WriteLine(sessionContext.DTO_CurrentDayCard.WeatherSummary.ToString());
            Console.ReadLine();
            sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            return sessionContext;
        }
        private async Task<TContext> AirQualityDetailsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            Console.Clear();
            Console.WriteLine(MenuText.Header.SpecificUser + $"{sessionContext.DTO_CurrentUser!.ToString()}");
            Console.WriteLine(sessionContext.DTO_CurrentDayCard.AirQualitySummary.ToString());
            Console.ReadLine();
            sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            return sessionContext;
        }

        private async Task<TContext> PollenDetailsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            Console.Clear();
            Console.WriteLine(sessionContext.DTO_CurrentDayCard.PollenSummary.ToString());
            Console.WriteLine(MenuText.Header.SpecificUser + $"{sessionContext.DTO_CurrentUser!.ToString()}");
            Console.ReadLine();
            sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            return sessionContext;
        }

        public TContext SpecificUserMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            sessionContext.MainHeader = MenuText.Header.SpecificUser + $"{sessionContext.DTO_CurrentUser!.ToString()}";

            var specificUserMenu = MenuText.NavOption.s_SpecificUserMenu.ToList();

            if (sessionContext.DTO_CurrentUser.DTO_AllDayCards!.Count > 0)
            {
                sessionContext.SubHeader = "Number of Daycards: " + sessionContext.DTO_CurrentUser.DTO_AllDayCards.Count;
                specificUserMenu = specificUserMenu.Prepend(MenuText.NavOption.ShowAllDayCards).ToList();
            }

            specificUserMenu.Add(MenuText.NavOption.Back);

            var specUserChoice = GetMenuValue(specificUserMenu, sessionContext);

            if (specUserChoice != null)
            {
                switch (specUserChoice)
                {
                    case MenuText.NavOption.CreateNewDayCard:
                        ResetMenuStates(sessionContext);
                        sessionContext.UserMenuState = UserMenuState.CreateNewDayCard;
                        //sessionContext.MainMenuState = MainMenuState.None;
                        break;

                    case MenuText.NavOption.ShowAllDayCards:
                        ResetMenuStates(sessionContext);
                        sessionContext.UserMenuState = UserMenuState.AllDayCards;
                        break;

                    case MenuText.NavOption.SearchDayCard:
                        ResetMenuStates(sessionContext);
                        sessionContext.UserMenuState = UserMenuState.SearchDayCard;
                        break;

                    case MenuText.NavOption.Back:
                        ResetMenuStates(sessionContext);
                        sessionContext.MainMenuState = MainMenuState.AllUsers;
                        break;
                }

            }
            else
            {
                sessionContext.MainMenuState = MainMenuState.AllUsers;
            }


            return sessionContext;
        }

        public T? GetMenuValue<T>(List<T> currentMenu, SessionContext sessionContext)
        {
            List<string> currentMenuStringList = currentMenu.Select(x => x?.ToString()).ToList()!;


            ConsoleKey keyPress;
            int currentIndex = 0;
            do
            {

                View.DisplayMenu(currentMenuStringList, ref currentIndex, sessionContext.CurrentPrompt!, sessionContext.MainHeader!, sessionContext.SubHeader, sessionContext.ErrorMessage);
                keyPress = View.InputToMenuIndex(ref currentIndex);

                if (keyPress == ConsoleKey.Escape)
                {
                    if (sessionContext.MainMenuState != MainMenuState.None)
                    {
                        if (sessionContext.MainMenuState == MainMenuState.Main)
                        {
                            sessionContext.MainMenuState = MainMenuState.Exit;
                        }
                        else
                        {
                            sessionContext.MainMenuState = MainMenuState.Back;

                        }
                    }
                    if (sessionContext.UserMenuState != UserMenuState.None)
                    {
                        sessionContext.UserMenuState = UserMenuState.Back;
                    }
                    if (sessionContext.DayCardMenuState != DayCardMenuState.None)
                    {
                        sessionContext.DayCardMenuState = DayCardMenuState.Back;
                    }
                }


            } while (keyPress != ConsoleKey.Enter && keyPress != ConsoleKey.Escape);

            if (keyPress != ConsoleKey.Escape)
            {
                return currentMenu[currentIndex]!;

            }
            else
            {
                return default;
            }

        }

    }

}
