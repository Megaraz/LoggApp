using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.MenuState_Enums
{
    public enum MainMenuState
    {
        Main,
        SpecificUser,
        AllUsers,
        Exit
    };

    public enum UserMenuState
    {
        AllDayCards,
        CreateNewDayCard,
        SpecificDayCard,
        SearchDayCard
    };

    public enum DayCardMenuState
    {
        Overview,
        AllData,
        AirQuality,
        Pollen,
        Weather,
        Sleep,
        CaffeineDrinks,
        Supplements
    };


}
