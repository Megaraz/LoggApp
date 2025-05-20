using AppLogic;
using AppLogic.Controllers;
using Presentation.MenuState_Enums;

namespace Presentation
{
    public enum MenuState
    {
        InitMenu,
        AllUsers,
        SpecificUser,
        CreateNewUser,
        CreateNewDayCard,
        AllDayCards,
        SpecificDayCard,
        SearchDayCard,
    };
    internal class App
    {

        private LoggAppContext _dbContext;
        private SessionContext _sessionContext;
        private View _view;
        private MenuHandler _menuHandler;
        private Controller _controller;

        public App(LoggAppContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        

        public void Init()
        {

            _sessionContext = new SessionContext();
            _controller = new Controller(_dbContext);
            _menuHandler = new MenuHandler(_controller);
            _view = new View(_menuHandler);

            _sessionContext.MainMenuState = MainMenuState.Main;
            _sessionContext.MainHeader = MenuText.Header.InitMenu;
            _sessionContext.CurrentMenuIndex = 0;
            _sessionContext.CurrentMainMenu = MenuText.NavOption.s_InitMenu.ToList();

            _sessionContext.UserMenuState = UserMenuState.None;
            _sessionContext.DayCardMenuState = DayCardMenuState.None;

        }

        public async Task Run()
        {
            
            do
            {
                _sessionContext = await _view.Start(_sessionContext);




            } while (_sessionContext.MainMenuState != MainMenuState.Exit);


        }
    }
}
