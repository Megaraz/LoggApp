using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;
using Presentation.MenuState_Enums;

namespace Presentation
{
    public class SessionContext
    {
        //public MenuState CurrentMenuState { get; set; }
        public MainMenuState MainMenuState { get; set; } = MainMenuState.None;
        public UserMenuState UserMenuState { get; set; } = UserMenuState.None;
        public DayCardMenuState DayCardMenuState { get; set; } = DayCardMenuState.None;
        public IntakeMenuState IntakeMenuState { get; set; } = IntakeMenuState.None;
        public ActivityMenuState ActivityMenuState { get; set; } = ActivityMenuState.None;
        public SleepMenuState SleepMenuState { get; set; } = SleepMenuState.None;


        // MAIN PAGE HEADER VARIABLE, UPDATE DYNAMICALLY
        public string? MainHeader { get; set; }
        public string? SubHeader { get; set; }
        public string? MainContent { get; set; }

        public string? Footer { get; set; }
        public string? CurrentPrompt { get; set; }


        // ERROR VARIABLE, UPDATE DYNAMICALLY
        public string? ErrorMessage { get; set; }

        // DYNAMIC MENU OPTIONS

        public UserDetailed? CurrentUser { get; set; }
        public DayCardDetailed? CurrentDayCard { get; set; }

        public CaffeineDrinkDetailed? CurrentCaffeineDrink { get; set; }
        public List<UserSummary>? AllUsersSummary { get; set; }

        public ExerciseDetailed? CurrentExercise { get; set; }

        public AirQualityDataSummary? CurrentAirQualityDataSummary { get; set; }
        public WeatherDataSummary? CurrentWeatherDataSummary { get; set; }

        public int UserCountInDb
        {
            get
            {
                return AllUsersSummary?.Count ?? 0;
            }
        }

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
            CurrentAirQualityDataSummary = null;
            CurrentWeatherDataSummary = null;

        }



    }
}
