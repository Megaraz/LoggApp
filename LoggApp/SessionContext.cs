using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;
using Presentation.MenuState_Enums;

namespace Presentation
{
    public class SessionContext
    {
        //public MenuState CurrentMenuState { get; set; }
        public MainMenuState MainMenuState { get; set; }
        public UserMenuState UserMenuState { get; set; }
        public DayCardMenuState DayCardMenuState { get; set; }

        public IntakeMenuState IntakeMenuState { get; set; }


        // MAIN PAGE HEADER VARIABLE, UPDATE DYNAMICALLY
        public string? MainHeader { get; set; }
        public string? SubHeader { get; set; }

        public string? CurrentPrompt { get; set; }


        // ERROR VARIABLE, UPDATE DYNAMICALLY
        public string? ErrorMessage { get; set; }

        // DYNAMIC MENU OPTIONS

        public UserDetailed? CurrentUser { get; set; }
        public DayCardDetailed? CurrentDayCard { get; set; }

        public CaffeineDrinkDetailed? CurrentCaffeineDrink { get; set; }
        public List<UserSummary>? AllUsersSummary { get; set; }
        public List<DayCardSummary>? AllDayCardsSummary { get; set; }

        public AirQualityDataSummary? CurrentAirQualityDataSummary { get; set; }
        public WeatherDataSummary? CurrentWeatherDataSummary { get; set; }

        //public User? CurrentUser { get; set; }
        //public DayCard? CurrentDayCard { get; set; }
        //public List<User>? AllUsers { get; set; }
        //public List<DayCard>? AllDayCards { get; set; }

        public void ClearAllSessionData()
        {
            MainHeader = null;
            SubHeader = null;
            CurrentPrompt = null;
            ErrorMessage = null;
            CurrentUser = null;
            CurrentDayCard = null;
            CurrentCaffeineDrink = null;
            AllUsersSummary = null;
            AllDayCardsSummary = null;
            CurrentAirQualityDataSummary = null;
            CurrentWeatherDataSummary = null;

        }



    }
}
