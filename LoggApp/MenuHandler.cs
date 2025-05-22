using System.Globalization;
using AppLogic.Controllers;
using AppLogic.Models;
using AppLogic.Models.DTOs;
using AppLogic.Services;
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
                    sessionContext = await SpecificDayCardMenuHandler(sessionContext);
                    break;

                case DayCardMenuState.AllData:
                    //sessionContext = ;
                    break;

                case DayCardMenuState.AirQualityDetails:
                    sessionContext = await AirQualityDetailsMenuHandler(sessionContext);
                    break;

                case DayCardMenuState.PollenDetails:
                    sessionContext = await PollenDetailsMenuHandler(sessionContext);
                    break;

                case DayCardMenuState.WeatherDetails:
                    sessionContext = await WeatherDetailsMenuHandler(sessionContext);
                    break;

                case DayCardMenuState.CaffeineDrinks:
                    //sessionContext = ;
                    break;

                case DayCardMenuState.Sleep:
                    //sessionContext = ;
                    break;

                case DayCardMenuState.Supplements:
                    //sessionContext = ;
                    break;
            }

            return sessionContext;

        }



        public async Task<TContext> HandleUserMenuState<TContext>(TContext sessionContext) where TContext : SessionContext
        {


            switch (sessionContext.UserMenuState)
            {
                case UserMenuState.AllDayCards:
                    sessionContext = await AllDayCardsMenuHandler(sessionContext);
                    break;

                case UserMenuState.CreateNewDayCard:
                    sessionContext = await CreateDayCardMenuHandler(sessionContext);
                    break;
                case UserMenuState.SearchDayCard:
                    break;
            }

            return sessionContext;

        }


        public async Task<TContext> HandleMainMenuState<TContext>(TContext sessionContext) where TContext : SessionContext
        {

            switch (sessionContext.MainMenuState)
            {
                case MainMenuState.Main:
                    sessionContext = await InitMenuHandler(sessionContext);
                    break;

                case MainMenuState.SpecificUser:
                    sessionContext = SpecificUserMenuHandler(sessionContext);
                    break;

                case MainMenuState.AllUsers:
                    sessionContext = await AllUsersMenuHandler(sessionContext);
                    break;

                case MainMenuState.TodaysWeather:
                    sessionContext = await GetTodaysWeather(sessionContext);
                    break;

                case MainMenuState.Exit:
                    break;


            }
            return sessionContext;
        }


        public async Task<TContext> InitMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {

            var initMenuChoice = GetMenuValue(sessionContext.CurrentMainMenu!, sessionContext);
            switch (initMenuChoice)
            {
                case MenuText.NavOption.Login:
                    sessionContext = await Login(sessionContext);
                    break;

                case MenuText.NavOption.GetAllUsers:
                    sessionContext = await GetAllUsers(sessionContext);
                    break;

                case MenuText.NavOption.CreateNewUser:
                    sessionContext = await CreateNewUser(sessionContext);
                    break;

                case MenuText.NavOption.GetTodaysWeather:
                    sessionContext = await GetTodaysWeather(sessionContext);
                    break;

                case MenuText.NavOption.Exit:
                    sessionContext.MainMenuState = MainMenuState.Exit;
                    break;
            }

            return sessionContext;
        }

        private async Task<TContext> GetTodaysWeather<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            // Get basic user info/input
            string location = View.Input_Location();
            // Get list of locations that match users input location%
            GeoResultResponse geoResultResponse = await _controller.LocationGeoResultList(location);
            // User chooses a Location from the list of locations
            GeoResult geoResult = GetMenuValue(geoResultResponse.Results, sessionContext);
            string lat = geoResult.Lat?.ToString(CultureInfo.InvariantCulture)!;
            string lon = geoResult.Lon?.ToString(CultureInfo.InvariantCulture)!;
            string date = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)!;
            DTO_AllWeatherData todayWeather = WeatherService.ConvertToDTO(await WeatherService.GetWeatherDataAsync(lat, lon, date));

            Console.WriteLine(todayWeather.ToString());

            Console.ReadLine();


            sessionContext.MainMenuState = MainMenuState.Main;

            return sessionContext;
        }

        private async Task<TContext> CreateNewUser<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            // Get basic user info/input
            UserInputModel userInputModel = View.Input_User();
            // Get list of locations that match users input location
            GeoResultResponse geoResultResponse = await _controller.UserGeoResultList(userInputModel);
            // User chooses a Location from the list of locations
            userInputModel.GeoResult = GetMenuValue(geoResultResponse.Results, sessionContext);
            // Create a new user with the updated GeoResult, and sets the CurrentUser to the newly created one
            sessionContext.DTO_CurrentUser = await _controller.CreateNewUserAsync(userInputModel);
            sessionContext.MainMenuState = MainMenuState.SpecificUser;

            return sessionContext;
        }

        private async Task<TContext> GetAllUsers<TContext>(TContext sessionContext) where TContext : SessionContext
        {

            List<DTO_AllUser>? allUsers = await _controller.ReadAllUsersAsync()!;

            if (allUsers == null || allUsers.Count == 0)
            {
                sessionContext.ErrorMessage = MenuText.Error.NoUserFound;
                sessionContext.MainMenuState = MainMenuState.Main;
            }
            else
            {
                sessionContext.MainMenuState = MainMenuState.AllUsers;
                sessionContext.CurrentMainMenu = MenuText.NavOption.s_AllUserMenu.ToList();
                sessionContext.DTO_AllUsers = allUsers;

            }

            return sessionContext;
        }

        private async Task<TContext> Login<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            string username = View.GetValidUserInput(MenuText.Prompt.CreateUser, MenuText.Error.InvalidUserNameInput);

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
            return sessionContext;
        }

        public TContext ResetMenuStates<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            sessionContext.MainMenuState = MainMenuState.None;
            sessionContext.UserMenuState = UserMenuState.None;
            sessionContext.DayCardMenuState = DayCardMenuState.None;
            return sessionContext;
        }

        public async Task<TContext> CreateDayCardMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            sessionContext.MainHeader = MenuText.Prompt.CreateDayCard;
            DayCardInputModel dayCardInputModel = View.Input_DayCard(sessionContext);
            sessionContext.DTO_CurrentDayCard = await _controller.CreateNewDayCardAsync(dayCardInputModel);
            sessionContext = ResetMenuStates(sessionContext);
            sessionContext.DayCardMenuState = DayCardMenuState.Overview;

            return sessionContext;
        }

        public async Task<TContext> AllUsersMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            if (sessionContext.DTO_AllUsers == null || sessionContext.DTO_AllUsers.Count == 0)
            {
                sessionContext.ErrorMessage = MenuText.Error.NoUsersFound;
                sessionContext.MainMenuState = MainMenuState.Main;

            }
            else
            {
                sessionContext.MainHeader = MenuText.Header.AllUsers;

                var userChoice = GetMenuValue(sessionContext.DTO_AllUsers, sessionContext);

                sessionContext
                    .DTO_CurrentUser = await _controller.ReadUserSingleAsync(userChoice.Id);

                sessionContext
                    .MainHeader = MenuText.Header.SpecificUser + $"{sessionContext.DTO_CurrentUser!.ToString()}\n\n\n";

                sessionContext
                    .MainMenuState = MainMenuState.SpecificUser;

            }
            return sessionContext;
        }

        public async Task<TContext> AllDayCardsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            if (sessionContext.DTO_CurrentUser!.DTO_AllDayCards == null || sessionContext.DTO_CurrentUser.DTO_AllDayCards.Count == 0)
            {
                sessionContext.ErrorMessage = MenuText.Error.NoDayCardsFound;
                sessionContext.MainMenuState = MainMenuState.SpecificUser;
            }
            else
            {
                sessionContext.SubHeader = MenuText.Header.AllDayCards;

                var dayCardChoice = GetMenuValue(sessionContext!.DTO_CurrentUser!.DTO_AllDayCards!, sessionContext);


                sessionContext.DTO_CurrentDayCard = await _controller.ReadDayCardSingleAsync(dayCardChoice.DayCardId, sessionContext.DTO_CurrentUser.Id);
                sessionContext = ResetMenuStates(sessionContext);
                sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            }

            return sessionContext;
        }
        public async Task<TContext> SpecificDayCardMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {


            sessionContext = ResetMenuStates(sessionContext);
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


            specificDayCardMenu.Add("[BACK]");
            specificDayCardMenu.Add("[EXIT]");

            var specUserChoice = GetMenuValue(specificDayCardMenu, sessionContext);

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

                case MenuText.NavOption.CaffeineDrinks:
                    sessionContext.DayCardMenuState = DayCardMenuState.CaffeineDrinks;
                    break;

                case MenuText.NavOption.Exercise:
                    sessionContext.DayCardMenuState = DayCardMenuState.Exercise;
                    break;

                case MenuText.NavOption.ComputerActivity:
                    sessionContext.DayCardMenuState = DayCardMenuState.ComputerActivity;
                    break;

                case MenuText.NavOption.Sleep:
                    break;


            }


            //Console.WriteLine($"{specific.ToString()}");
            //Console.ReadLine();
            return sessionContext;

        }
        private async Task<TContext> WeatherDetailsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            Console.Clear();
            Console.WriteLine(MenuText.Header.SpecificUser + $"{sessionContext.DTO_CurrentUser!.ToString()}");
            Console.WriteLine(sessionContext.DTO_CurrentDayCard.WeatherSummary.ToString());
            Console.ReadLine();
            sessionContext = ResetMenuStates(sessionContext);
            sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            return sessionContext;
        }
        private async Task<TContext> AirQualityDetailsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            Console.Clear();
            Console.WriteLine(MenuText.Header.SpecificUser + $"{sessionContext.DTO_CurrentUser!.ToString()}");
            Console.WriteLine(sessionContext.DTO_CurrentDayCard.AirQualitySummary.ToString());
            Console.ReadLine();
            sessionContext = ResetMenuStates(sessionContext);
            sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            return sessionContext;
        }

        private async Task<TContext> PollenDetailsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            Console.Clear();
            Console.WriteLine(sessionContext.DTO_CurrentDayCard.PollenSummary.ToString());
            Console.WriteLine(MenuText.Header.SpecificUser + $"{sessionContext.DTO_CurrentUser!.ToString()}");
            Console.ReadLine();
            sessionContext = ResetMenuStates(sessionContext);
            sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            return sessionContext;
        }

        public TContext SpecificUserMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            sessionContext.MainHeader = MenuText.Header.SpecificUser + $"{sessionContext.DTO_CurrentUser!.ToString()}";

            var specificUserMenu = MenuText.NavOption.s_SpecificUserMenu.ToList();

            if (sessionContext.DTO_CurrentUser.DTO_AllDayCards!.Count > 0)
            {
                sessionContext.SubHeader = "Number of Daycards: " + sessionContext.DTO_CurrentUser.DTO_AllDayCards.Count;
                specificUserMenu = specificUserMenu.Prepend(MenuText.NavOption.ShowAllDayCards).ToList();
            }

            var specUserChoice = GetMenuValue(specificUserMenu, sessionContext);

            switch (specUserChoice)
            {
                case MenuText.NavOption.CreateNewDayCard:
                    sessionContext = ResetMenuStates(sessionContext);
                    sessionContext.UserMenuState = UserMenuState.CreateNewDayCard;
                    //sessionContext.MainMenuState = MainMenuState.None;
                    break;

                case MenuText.NavOption.ShowAllDayCards:
                    sessionContext = ResetMenuStates(sessionContext);
                    sessionContext.UserMenuState = UserMenuState.AllDayCards;
                    break;

                case MenuText.NavOption.SearchDayCard:
                    sessionContext = ResetMenuStates(sessionContext);
                    sessionContext.UserMenuState = UserMenuState.SearchDayCard;
                    break;
            }

            return sessionContext;
        }

        public T GetMenuValue<T>(List<T> currentMenu, SessionContext sessionContext)
        {
            List<string> currentMenuStringList = currentMenu.Select(x => x?.ToString()).ToList()!;


            ConsoleKey keyPress;
            int currentIndex = 0;
            do
            {

                View.DisplayMenu(currentMenuStringList, ref currentIndex, sessionContext.MainHeader!, sessionContext.SubHeader, sessionContext.ErrorMessage);
                keyPress = View.InputToMenuIndex(ref currentIndex);

                if (keyPress == ConsoleKey.Escape)
                {
                    
                }

            } while (keyPress != ConsoleKey.Enter && keyPress != ConsoleKey.Escape);


            return currentMenu[currentIndex]!;
        }

    }

}
