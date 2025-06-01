namespace Presentation
{
    /// <summary>
    /// This class contains static strings used for menu headers, prompts, error messages, and navigation options in the application.
    /// </summary>
    public static class MenuText
    {
        // HEADER SUBCLASS
        public static class Header
        {
            // PAGE HEADERS
            public const string InitMenu = "WELCOME TO LOGGAPP!";

            public const string CurrentDayCard = "CURRENT DAYCARD ";

            public const string CurrentUserName = "CURRENT USERNAME: ";
            public const string CurrentLocation = "CURRENT LOCATION: ";

            public const string AllDayCards = "DAYCARD ID\tDATE\t\tENTRIES";

            public const string AllExercises = "EXERCISE ID\t\tDURATION\tINTENSITY";

            public const string UserDeleted = "USER DELETED SUCCESSFULLY!";
            public const string CaffeineDrinkDeleted = "CAFFEINE DRINK DELETED SUCCESSFULLY!";
            public const string DayCardDeleted = "DAYCARD DELETED SUCCESSFULLY!";
            public const string ExerciseDeleted = "EXERCISE DELETED SUCCESSFULLY!";
            public const string SleepDeleted = "SLEEP DELETED SUCCESSFULLY!";
            public const string CheckInDeleted = "WELLNESS CHECK-IN DELETED SUCCESSFULLY!";


            public static string SpecificUser = "ID".PadRight(6) + "USERNAME".PadRight(12) + "CITY".PadRight(15) + "DAYCARDS\n\n";
            public static string AllUsers = "ID".PadRight(6) + "USERNAME".PadRight(12) + "CITY".PadRight(15) + "DAYCARDS";

            public static string AllCheckins = "ID".PadRight(6) + "TIME".PadRight(10) + "MOOD".PadRight(10) + "ENERGY".PadRight(10);

            public const string WeatherDetails = "TIME\tTEMP\tFEELSLIKE\tHumidity\tPrecip\tRain\tCloud\tUV\tWind\tPressure";
        }

        // PROMPT SUBCLASS
        public static class Prompt
        {
            // PROMPTS
            public const string EnterTimeOfIntake = "ENTER TIME OF INTAKE";
            public const string EnterTimeOfCheckIn = "ENTER TIME OF CHECK-IN";

            public const string CreateDayCard = "ENTER A DATE FOR THE NEW DAYCARD";
            public const string SearchDayCard = "ENTER A DATE TO SEARCH FOR DAYCARD(yyyy-MM-dd)";

            public const string EnterUserName = "ENTER A USERNAME";
            public const string EnterNewUserName = "ENTER A NEW USERNAME";
            public const string EnterUserLocation = "ENTER A CITY OR LOCATION";
            public const string EnterNewUserLocation = "ENTER A NEW LOCATION";

            public const string ChooseLocation = "CHOOSE YOUR LOCATION";
            public const string ChooseSizeOfDrink = "CHOOSE SIZE OF DRINK";

            public const string DeleteUserConfirmation = "ARE YOU SURE YOU WANT TO DELETE THIS USER? (Y/N)";
            public const string DeleteCaffeineDrinkConfirmation = "ARE YOU SURE YOU WANT TO DELETE THIS CAFFEINE DRINK? (Y/N)";
            public const string DeleteDayCardConfirmation = "ARE YOU SURE YOU WANT TO DELETE THIS DAYCARD? (Y/N)";
            public const string DeleteExerciseConfirmation = "ARE YOU SURE YOU WANT TO DELETE THIS EXERCISE? (Y/N)";
            public const string DeleteSleepConfirmation = "ARE YOU SURE YOU WANT TO DELETE THIS SLEEP? (Y/N)";
            public const string DeleteCheckInConfirmation = "ARE YOU SURE YOU WANT TO DELETE THIS WELLNESS CHECK-IN? (Y/N)";


        }
        // ERROR MESSAGE SUBCLASS
        public static class Error
        {
            // ERROR-MESSAGES
            public const string NoUserFound = "NO USER WITH THAT NAME/ID FOUND";
            public const string NoUsersFound = "NO USERS FOUND";

            public const string NoDayCardsFound = "NO DAYCARDS FOUND FOR THIS USER";
            public const string NoDayCardFound = "NO DAYCARD FOUND FOR THIS DATE";

            public const string InvalidTimeInput = "NOT A VALID TIME";
            public const string InvalidDayCardInput = "NOT A VALID DATE";
            public const string InvalidUserNameInput = "NOT A VALID USERNAME";
            public const string InvalidUserCityInput = "NOT A VALID LOCATION";

            public const string UserDeleteFailed = "USER DELETE FAILED, PLEASE TRY AGAIN";
            public const string CaffeineDrinkDeleteFailed = "CAFFEINE DRINK DELETE FAILED, PLEASE TRY AGAIN";
            public const string DayCardDeleteFailed = "DAYCARD DELETE FAILED, PLEASE TRY AGAIN";
            public const string ExerciseDeleteFailed = "EXERCISE DELETE FAILED, PLEASE TRY AGAIN";
            public const string SleepDeleteFailed = "SLEEP DELETE FAILED, PLEASE TRY AGAIN";
            public const string CheckInDeleteFailed = "WELLNESS CHECK-IN DELETE FAILED, PLEASE TRY AGAIN";


        }

        // NAVIGATION OPTIONS SUBCLASS
        public static class NavOption
        {
            // MENU NAVIGATION CONSTANTS
            
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

            public const string ExerciseRelaxed = "[RELAXED]";
            public const string ExerciseLight = "[LIGHT]";
            public const string ExerciseModerate = "[MODERATE]";
            public const string ExerciseIntense = "[INTENSE]";
            public const string ExerciseMaxEffort = "[MAX EFFORT]";

            public const string CreateNewDayCard = "[CREATE NEW DAYCARD]";
            public const string SearchDayCard = "[SEARCH DAYCARD]";
            public const string ShowAllDayCards = "[SHOW ALL DAYCARDS]";
            public const string DeleteDayCard = "[DELETE DAYCARD]";
            public const string ChangeDayCardDate = "[CHANGE DAYCARD DATE]";

            public const string Weather = "[WEATHER SPECIFICS]";
            public const string AirQuality = "[AIRQUALITY SPECIFICS]";
            public const string Pollen = "[POLLEN SPECIFICS]";
            public const string Supplements = "[SUPPLEMENTS]";
            public const string CaffeineDrinks = "[CAFFEINEDRINKS]";
            public const string Exercise = "[EXERCISE]";
            public const string SleepDetails = "[SLEEP DETAILS]";
            public const string ComputerActivity = "[COMPUTER ACTIVITY]";
            public const string WellnessCheckIns = "[WELLNESS CHECK-INS]";


            public const string AddCaffeine = "[ADD CAFFEINE]";
            public const string ShowAllCaffeineDrinks = "[SHOW ALL CAFFEINE DRINKS]";
            public const string UpdateCaffeineDrink = "[EDIT CAFFEINE DRINK]";
            public const string DeleteCaffeineDrink = "[DELETE CAFFEINE DRINK]";

            public const string UpdateSleep = "[EDIT SLEEP]";
            public const string DeleteSleep = "[DELETE SLEEP]";
            public const string AddSleep = "[ADD SLEEP]";

            public const string ShowAllExercise = "[SHOW ALL EXERCISE]";
            public const string AddExercise = "[ADD EXERCISE]";
            public const string UpdateExercise = "[EDIT EXERCISE]";
            public const string DeleteExercise = "[DELETE EXERCISE]";


            public const string ShowAllCheckins = "[SHOW ALL WELLNESS CHECK-INS]";
            public const string AddWellnessCheckIn = "[ADD WELLNESS CHECK-IN]";
            public const string UpdateWellnessCheckIn = "[EDIT WELLNESS CHECK-IN]";
            public const string DeleteWellnessCheckIn = "[DELETE WELLNESS CHECK-IN]";

            public const string WellnessRatingVeryLow = "[VERY LOW]";
            public const string WellnessRatingLow = "[LOW]";
            public const string WellnessRatingMedium = "[MEDIUM]";
            public const string WellnessRatingHigh = "[HIGH]";
            public const string WellnessRatingVeryHigh = "[VERY HIGH]";

            public const string Back = "[BACK]";


            // MENU NAVIGATION COLLECTIONS
            public static string[] s_InitMenu = [Login, GetAllUsers, CreateNewUser, GetTodaysWeather, Exit];
            public static string[] s_AllUserMenu = [Login, CreateNewUser];
            public static string[] s_SpecificUserMenu = [CreateNewDayCard, SearchDayCard, UserSettings];
            public static string[] s_UserSettingsMenu = [UpdateUsername, UpdateLocation, DeleteUser];
            public static string[] s_SpecificDayCardMenu = [Weather, AirQuality, Pollen, Supplements, CaffeineDrinks, Exercise, ComputerActivity, SleepDetails];

            public static string[] s_CaffeineOverviewMenu = [ShowAllCaffeineDrinks, AddCaffeine, Back];

            public static string[] s_WellnessCheckInOverviewMenu = [ShowAllCheckins, AddWellnessCheckIn, Back];
            public static string[] s_WellnessCheckInDetailsMenu = [UpdateWellnessCheckIn, DeleteWellnessCheckIn, Back];
            public static string[] s_WellnessRating = [WellnessRatingVeryLow, WellnessRatingLow, WellnessRatingMedium, WellnessRatingHigh, WellnessRatingVeryHigh];

            public static string[] s_ExerciseOverviewMenu = [ShowAllExercise, AddExercise, Back];
            public static string[] s_ExerciseDetailsMenu = [UpdateExercise, DeleteExercise, Back];

            public static string[] s_SleepDetailsMenu = [UpdateSleep, DeleteSleep, Back];

            public static string[] s_DrinkSize = [DrinkLarge, DrinkMedium, DrinkSmall];

            public static string[] s_ExercisePerceivedIntensity = [ExerciseRelaxed, ExerciseLight, ExerciseModerate, ExerciseIntense, ExerciseMaxEffort];

            public static string[] s_CaffeineDetailsMenu = [UpdateCaffeineDrink, DeleteCaffeineDrink, Back];

        }

    }
}
