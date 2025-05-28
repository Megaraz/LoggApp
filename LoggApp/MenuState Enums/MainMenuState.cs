namespace Presentation.MenuState_Enums
{
    public enum MainMenuState
    {
        Main,
        SpecificUser,
        AllUsers,
        TodaysWeather,
        Back,
        Exit,
        None
    };

    public enum UserMenuState
    {
        AllDayCards,
        CreateNewDayCard,
        SpecificDayCard,
        SearchDayCard,
        UserSettings,
        None
    };

    public enum DayCardMenuState
    {
        Overview,
        WeatherDetails,
        AirQualityDetails,
        PollenDetails,
        AddWellnessCheckIn,
        WellnessCheckInDetails,
        ExerciseDetails,
        AddExercise,
        ComputerActivity,
        AddSleep,
        SleepDetails,
        UpdateDayCard,
        DeleteDayCard,
        None
    };

    public enum IntakeMenuState
    {
        CaffeineOverview,
        ShowAllCaffeineDrinks,
        AddCaffeineDrink,
        CaffeineDrinkDetails,
        UpdateCaffeineDrink,
        DeleteCaffeineDrink,
        CaffeineEmpty,
        SupplementsOverview,
        AddSupplements,
        SupplementsDetails,
        DeleteSupplements,
        None
    }


}
