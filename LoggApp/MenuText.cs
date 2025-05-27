namespace Presentation
{

    public static class MenuText
    {

        public static class Header
        {
            // PAGE HEADERS
            public const string InitMenu = "WELCOME TO LOGGAPP!";
            //public const string AllUsers = "ALL USERS IN DB";

            public const string CurrentDayCard = "CURRENT DAYCARD ";

            public const string CurrentUserName = "CURRENT USERNAME: ";
            public const string CurrentLocation = "CURRENT LOCATION: ";

            public const string AllDayCards = "DAYCARD ID\tDATE\t\tENTRIES";

            public const string UserDeleted = "USER DELETED SUCCESSFULLY!";
            public const string CaffeineDrinkDeleted = "CAFFEINE DRINK DELETED SUCCESSFULLY!";


            public static string SpecificUser = "ID".PadRight(6) + "USERNAME".PadRight(12) + "CITY\n\n";
            public static string AllUsers = "ID".PadRight(6) + "USERNAME".PadRight(12) + "CITY".PadRight(15) + "DAYCARDS";


            public const string WeatherDetails = "TIME\tTEMP\tFEELSLIKE\tHumidity\tPrecip\tRain\tCloud\tUV\tWind\tPressure";
        }

        public static class Prompt
        {
            // PROMPTS
            public const string EnterTimeOfIntake = "ENTER TIME OF INTAKE";

            public const string CreateDayCard = "ENTER A DATE FOR THE NEW DAYCARD";

            public const string EnterUserName = "ENTER A USERNAME";
            public const string EnterUserLocation = "ENTER A CITY OR LOCATION";
            public const string ChooseLocation = "CHOOSE YOUR LOCATION";

            public const string EnterNewUserName = "ENTER A NEW USERNAME";
            public const string EnterNewUserLocation = "ENTER A NEW LOCATION";

            public const string ChooseSizeOfDrink = "CHOOSE SIZE OF DRINK";

            public const string DeleteUserConfirmation = "ARE YOU SURE YOU WANT TO DELETE THIS USER? (Y/N)";
            public const string DeleteCaffeineDrinkConfirmation = "ARE YOU SURE YOU WANT TO DELETE THIS CAFFEINE DRINK? (Y/N)";
            public const string DeleteDayCardConfirmation = "ARE YOU SURE YOU WANT TO DELETE THIS DAYCARD? (Y/N)";


        }

        public static class Error
        {
            // ERROR-MESSAGES
            public const string NoUserFound = "NO USER WITH THAT ID FOUND";
            public const string NoUsersFound = "NO USERS FOUND";

            public const string NoDayCardsFound = "NO DAYCARDS FOUND FOR THIS USER";

            public const string InvalidTimeInput = "NOT A VALID TIME";

            public const string InvalidDayCardInput = "NOT A VALID DATE";
            public const string InvalidUserNameInput = "NOT A VALID USERNAME";
            public const string InvalidUserCityInput = "NOT A VALID LOCATION";

            public const string UserDeleteFailed = "USER DELETE FAILED, PLEASE TRY AGAIN";
            public const string CaffeineDrinkDeleteFailed = "CAFFEINE DRINK DELETE FAILED, PLEASE TRY AGAIN";


        }

        public static class NavOption
        {
            
            public const string UserSettings = "[USER SETTINGS]";
            public const string UpdateUsername = "[CHANGE USERNAME]";
            public const string UpdateLocation = "[CHANGE LOCATION]";
            public const string DeleteUser = "[DELETE USER]";


            public const string Login = "[LOG IN]";
            public const string GetAllUsers = "[GET ALL USERS]";
            public const string GetTodaysWeather = "[GET WEATHER FOR TODAY]";
            public const string CreateNewUser = "[CREATE NEW USER]";
            public const string Exit = "[EXIT]";

            public const string SearchUser = "[SEARCH USER]";

            public const string DrinkLarge = "[LARGE]";
            public const string DrinkMedium = "[MEDIUM]";
            public const string DrinkSmall = "[SMALL]";


            public const string CreateNewDayCard = "[CREATE NEW DAYCARD]";
            public const string SearchDayCard = "[SEARCH DAYCARD]";
            public const string ShowAllDayCards = "[SHOW ALL DAYCARDS]";
            public const string DeleteDayCard = "[DELETE DAYCARD]";

            public const string Weather = "[WEATHER SPECIFICS]";
            public const string AirQuality = "[AIRQUALITY SPECIFICS]";
            public const string Pollen = "[POLLEN SPECIFICS]";
            public const string Supplements = "[SUPPLEMENTS]";
            public const string CaffeineDrinks = "[CAFFEINEDRINKS]";
            public const string Exercise = "[EXERCISE]";
            public const string Sleep = "[SLEEP]";
            public const string ComputerActivity = "[COMPUTER ACTIVITY]";

            public const string AddSupplements = "[ADD SUPPLEMENTS]";
            public const string AddCaffeine = "[ADD CAFFEINE]";

            public const string ShowAllCaffeineDrinks = "[SHOW ALL CAFFEINE DRINKS]";

            public const string UpdateCaffeineDrink = "[EDIT CAFFEINE DRINK]";
            public const string DeleteCaffeineDrink = "[DELETE CAFFEINE DRINK]";
            public const string UpdateSupplement = "[EDIT SUPPLEMENT]";
            public const string AddIngredient = "[ADD INGREDIENT]";
            public const string DeleteSupplement = "[DELETE SUPPLEMENT]";

            public const string Back = "[BACK]";

            // STATIC MENU OPTIONS
            public static string[] s_InitMenu = [Login, GetAllUsers, CreateNewUser, GetTodaysWeather, Exit];
            public static string[] s_AllUserMenu = [Login, CreateNewUser];
            public static string[] s_SpecificUserMenu = [CreateNewDayCard, SearchDayCard, UserSettings];
            public static string[] s_UserSettingsMenu = [UpdateUsername, UpdateLocation, DeleteUser];
            public static string[] s_SpecificDayCardMenu = [Weather, AirQuality, Pollen, Supplements, CaffeineDrinks, Exercise, ComputerActivity, Sleep];

            public static string[] s_CaffeineOverviewMenu = [ShowAllCaffeineDrinks, AddCaffeine, Back];

            public static string[] s_DrinkSize = [DrinkLarge, DrinkMedium, DrinkSmall];

            public static string[] s_CaffeineDetailsMenu = [UpdateCaffeineDrink, DeleteCaffeineDrink, Back];
            public static string[] s_SupplementDetailsMenu = [UpdateSupplement, DeleteSupplement, AddIngredient, Back];

        }




        


    }
}
