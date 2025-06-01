namespace Presentation.MenuState_Enums
{
    public enum MainMenuState
    {
        Main,
        AllUsers,
        TodaysWeather,
        Back,
        Exit,
        None
    };

    public enum UserMenuState
    {
        Overview,
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
        //ComputerActivity,
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

    public enum ActivityMenuState
    {
        ExerciseOverview,
        AddExercise,
        ShowAllExercises,
        ExerciseDetails,
        UpdateExercise,
        DeleteExercise,
        None
    }

    public enum SleepMenuState
    {
        SleepDetails,
        AddSleep,
        UpdateSleep,
        DeleteSleep,
        None
    }

}
