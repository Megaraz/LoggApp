namespace Presentation
{

    public static class MenuText
    {

        public static class Header
        {
            // PAGE HEADERS
            public const string InitMenu = "WELCOME TO LOGGAPP!";
            public const string AllUsers = "ALL USERS IN DB";

            public const string CurrentDayCard = "CURRENT DAYCARD: ";

            public const string AllDayCards = "DAYCARD ID\tDATE\t\tENTRIES";

            public const string SpecificUser = "USER ID\t\tUSERNAME\tCITY\n";
        }

        public static class Prompt
        {
            // PROMPTS
            public const string CreateDayCard = "ENTER A DATE FOR THE NEW DAYCARD, OR LEAVE EMPTY FOR TODAYS DATE";

            public const string CreateUser = "ENTER A USERNAME";
            public const string CreateUserCity = "ENTER A CITY OR LOCATION";

        }

        public static class Error
        {
            // ERROR-MESSAGES
            public const string NoUserFound = "NO USER WITH THAT ID FOUND";
            public const string NoUsersFound = "NO USERS FOUND";

            public const string NoDayCardsFound = "NO DAYCARDS FOUND FOR THIS USER";

            public const string InvalidDayCardInput = "NOT A VALID DATE";
            public const string InvalidUserNameInput = "NOT A VALID USERNAME";
            public const string InvalidUserCityInput = "NOT A VALID LOCATION";


        }

        public static class NavOption
        {
            public const string Login = "[LOG IN]";
            public const string GetAllUsers = "[GET ALL USERS]";

            public const string CreateNewUser = "[CREATE NEW USER]";
            public const string Exit = "[EXIT]";

            public const string SearchUser = "[SEARCH USER]";
            


            public const string CreateNewDayCard = "[CREATE NEW DAYCARD]";
            public const string SearchDayCard = "[SEARCH DAYCARD]";
            public const string ShowAllDayCards = "[SHOW ALL DAYCARDS]";

            public const string Weather = "[WEATHER SPECIFICS]";
            public const string AirQuality = "[AIRQUALITY SPECIFICS]";
            public const string Pollen = "[POLLEN SPECIFICS]";
            public const string Supplements = "[SUPPLEMENTS]";
            public const string CaffeineDrinks = "[CAFFEINEDRINKS]";
            public const string Exercise = "[EXERCISE]";
            public const string Sleep = "[SLEEP]";
            public const string ComputerActivity = "[COMPUTER ACTIVITY]";

            // STATIC MENU OPTIONS
            public static string[] s_InitMenu = [Login, GetAllUsers, CreateNewUser, Exit];
            public static string[] s_AllUserMenu = [Login, CreateNewUser];
            public static string[] s_SpecificUserMenu = [ShowAllDayCards, CreateNewDayCard, SearchDayCard];
            public static string[] s_SpecificDayCardMenu = [Weather, AirQuality, Pollen, Supplements, CaffeineDrinks, Exercise, ComputerActivity, Sleep];

        }




        


    }
}
