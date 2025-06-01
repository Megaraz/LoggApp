using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;
using Presentation.MenuState_Enums;

namespace Presentation
{
    /// <summary>
    /// Represents the session context for the application, holding the current state of various menus and user data.
    /// </summary>
    public class SessionContext
    {
        // MENU STATES
        public MainMenuState MainMenuState { get; set; } = MainMenuState.None;
        public UserMenuState UserMenuState { get; set; } = UserMenuState.None;
        public DayCardMenuState DayCardMenuState { get; set; } = DayCardMenuState.None;
        public IntakeMenuState IntakeMenuState { get; set; } = IntakeMenuState.None;
        public ActivityMenuState ActivityMenuState { get; set; } = ActivityMenuState.None;
        public SleepMenuState SleepMenuState { get; set; } = SleepMenuState.None;
        public WellnessCheckInMenuState WellnessCheckInMenuState { get; set; } = WellnessCheckInMenuState.None;


        // SESSION DISPLAY VARIABLES
        public string? MainHeader { get; set; }
        public string? SubHeader { get; set; }
        public string? MainContent { get; set; }
        public string? Footer { get; set; }
        public string? CurrentPrompt { get; set; }
        public string? ErrorMessage { get; set; }



        // DTO REPRESENTATIONS OF ENTITIES

        public UserDetailed? CurrentUser { get; set; }
        public DayCardDetailed? CurrentDayCard { get; set; }
        public CaffeineDrinkDetailed? CurrentCaffeineDrink { get; set; }
        public ExerciseDetailed? CurrentExercise { get; set; }
        public WellnessCheckInDetailed? CurrentWellnessCheckIn { get; set; }
        public List<UserSummary>? AllUsersSummary { get; set; }
        public AirQualityDataSummary? CurrentAirQualityDataSummary { get; set; }
        public WeatherDataSummary? CurrentWeatherDataSummary { get; set; }

        public int UserCountInDb
        {
            get
            {
                return AllUsersSummary?.Count ?? 0;
            }
        }
    }
}
