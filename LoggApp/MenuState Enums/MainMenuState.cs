namespace Presentation.MenuState_Enums
{
    public enum MainMenuState
    {
        Main,
        SpecificUser,
        AllUsers,
        TodaysWeather,
        Exit,
        None
    };

    public enum UserMenuState
    {
        AllDayCards,
        CreateNewDayCard,
        SpecificDayCard,
        SearchDayCard,
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
        CaffeineDrinks,
        Supplements,
        None
    };


}
