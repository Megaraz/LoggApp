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


        // MAIN PAGE HEADER VARIABLE, UPDATE DYNAMICALLY
        public string? MainHeader { get; set; }
        public string? SubHeader { get; set; }

        public string? CurrentPrompt { get; set; }

        public int CurrentMenuIndex { get; set; }


        // ERROR VARIABLE, UPDATE DYNAMICALLY
        public string? ErrorMessage { get; set; }

        // DYNAMIC MENU OPTIONS

        public UserDetailed? UserDetailed { get; set; }
        public DayCardDetailed? DayCardDetailed { get; set; }
        public List<UserSummary>? UsersSummary { get; set; }
        public List<DayCardSummary>? DayCardsSummary { get; set; }

        public AirQualityDataSummary AirQualityDataSummary { get; set; }
        public WeatherDataSummary WeatherDataSummary { get; set; }

        //public User? CurrentUser { get; set; }
        //public DayCard? CurrentDayCard { get; set; }
        //public List<User>? AllUsers { get; set; }
        //public List<DayCard>? AllDayCards { get; set; }





    }
}
