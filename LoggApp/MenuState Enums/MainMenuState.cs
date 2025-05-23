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
        Back,
        None
    };

    public enum DayCardMenuState
    {
        Overview,
        AllData,
        WeatherDetails,
        AirQualityDetails,
        PollenDetails,
        Exercise,
        ComputerActivity,
        Sleep,
        AddCaffeineDrink,
        CaffeineDrinkDetails,
        Supplements,
        Back,
        None
    };


}
