using AppLogic;
using AppLogic.Controllers;
using AppLogic.Controllers.Interfaces;
using AppLogic.Repositories;
using AppLogic.Repositories.Interfaces;
using AppLogic.Services;
using AppLogic.Services.Interfaces;
using Presentation.MenuState_Enums;

namespace Presentation
{
    internal class App
    {

        private LoggAppContext _dbContext;
        private SessionContext _sessionContext;
        private View _view;
        private DayCardMenuHandler _dayCardMenuHandler;
        private UserMenuHandler _userMenuHandler;
        private MainMenuHandler _mainMenuHandler;
        private OpenAiResponseClient _openAiResponseClient;

        // Repos
        private IUserRepo _userRepo;
        private IDayCardRepo _dayCardRepo;
        private ICaffeineDrinkRepo _caffeineDrinkRepo;
        private IWeatherRepo _weatherRepo;
        private IAirQualityRepo _airQualityRepo;
        //private IOpenAiResponseClient _openAiResponseClient;

        // Services
        private IUserService _userService;
        private IWeatherService _weatherService;
        private IAirQualityService _airQualityService;
        private IDayCardService _dayCardService;
        private ICaffeineDrinkService _caffeineDrinkService;

        // Controllers
        private IUserController _userController;
        private IWeatherController _weatherController;
        private IAirQualityController _airQualityController;
        private IDayCardController _dayCardController;
        private ICaffeineDrinkController _caffeineDrinkController;

        public App(LoggAppContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        
        public void InitRepos()
        {
            _userRepo = new UserRepo(_dbContext);
            _dayCardRepo = new DayCardRepo(_dbContext);
            _caffeineDrinkRepo = new CaffeineDrinkRepo(_dbContext);
            _weatherRepo = new WeatherRepo(_dbContext);
            _airQualityRepo = new AirQualityRepo(_dbContext);
            _openAiResponseClient = new OpenAiResponseClient();
        }

        public void InitServices()
        {
            _userService = new UserService(_userRepo);
            _weatherService = new WeatherService(_weatherRepo);
            _airQualityService = new AirQualityService(_airQualityRepo);
            _caffeineDrinkService = new CaffeineDrinkService(_caffeineDrinkRepo);
            _dayCardService = new DayCardService(_dayCardRepo, _weatherService, _airQualityService, _caffeineDrinkService, _openAiResponseClient);
        }

        public void InitControllers()
        {
            _userController = new UserController(_userService);
            _weatherController = new WeatherController(_weatherService);
            _airQualityController = new AirQualityController(_airQualityService);
            _dayCardController = new DayCardController(_dayCardService);
            _caffeineDrinkController = new CaffeineDrinkController(_caffeineDrinkService);

        }

        public void InitPresentation()
        {
            _mainMenuHandler = new MainMenuHandler(_userController, _weatherController);
            _userMenuHandler = new UserMenuHandler(_dayCardController);
            _dayCardMenuHandler = new DayCardMenuHandler(_caffeineDrinkController);
            _view = new View(_mainMenuHandler, _userMenuHandler, _dayCardMenuHandler);


            _sessionContext = new SessionContext();
            _sessionContext.MainMenuState = MainMenuState.Main;
            _sessionContext.MainHeader = MenuText.Header.InitMenu;
            _sessionContext.CurrentMenuIndex = 0;
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
