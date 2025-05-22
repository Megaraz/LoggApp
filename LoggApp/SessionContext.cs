using AppLogic.Models.DTOs;
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

        // Main Navigation Menu
        public List<string>? CurrentMainMenu { get; set; }

        // ERROR VARIABLE, UPDATE DYNAMICALLY
        public string? ErrorMessage { get; set; }

        // DYNAMIC MENU OPTIONS

        public DTO_SpecificUser? DTO_CurrentUser { get; set; }
        public DTO_SpecificDayCard? DTO_CurrentDayCard { get; set; }
        public List<DTO_AllUser>? DTO_AllUsers { get; set; }
        public List<DTO_AllDayCard>? DTO_AllDayCards { get; set; }

        public DTO_AllAirQualityData DTO_AllAirQualityData { get; set; }
        public DTO_AllWeatherData DTO_AllWeatherData { get; set; }

        //public User? CurrentUser { get; set; }
        //public DayCard? CurrentDayCard { get; set; }
        //public List<User>? AllUsers { get; set; }
        //public List<DayCard>? AllDayCards { get; set; }





    }
}
