using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.MenuState_Enums
{
    public enum InitMainMenuState
    {
        Main,
        SpecificUser,
        AllUsers,
        Exit
    };

    public enum InitUserMenuState
    {
        AllDayCards,
        CreateNewDayCard,
        SpecificDayCard,
        SearchDayCard
    };

    public enum InitDayCardMenuState
    {

    };


}
