using Formula1.Core;
using Formula1.Services.Ergast;
using Formula1.Services.Informations;
using Formula1.ViewModels.Popups;
using Formula1.ViewModels.TabViews;
using System;

namespace Formula1.ViewModels
{
    public class ViewModelLocator
    {
        private readonly Lazy<HttpClientFactory> httpClientFactory;
        private readonly Lazy<IErgastService> ergastService;
        private readonly Lazy<IInformationsService> informationService;

        public ViewModelLocator()
        {
            httpClientFactory = new Lazy<HttpClientFactory>(() => new HttpClientFactory());
            ergastService = new Lazy<IErgastService>(() => new ErgastService(httpClientFactory.Value));
            informationService = new Lazy<IInformationsService>(() => new InformationsService(httpClientFactory.Value));
        }

        public WelcomePageViewModel WelcomePage => new WelcomePageViewModel();
        public ProfilePageViewModel ProfilePage => new ProfilePageViewModel();

        public HomeViewModel HomeView => new HomeViewModel(ergastService.Value);
        public ScheduleViewModel ScheduleView => new ScheduleViewModel(ergastService.Value);
        public DriversViewModel DriversView => new DriversViewModel(ergastService.Value);
        public TeamsViewModel TeamsView => new TeamsViewModel(ergastService.Value);
        public HistoryViewModel HistoryView => new HistoryViewModel(ergastService.Value);
        
        public DriverDetailsPageViewModel DriverDetailsPage => new DriverDetailsPageViewModel(ergastService.Value, informationService.Value);
        public CircuitDetailsPageViewModel CircuitDetailsPage => new CircuitDetailsPageViewModel(ergastService.Value, informationService.Value);
        public CircuitLapsPageViewModel CircuitLapsPage => new CircuitLapsPageViewModel(ergastService.Value);
        public TeamDetailsPageViewModel TeamDetailsPage => new TeamDetailsPageViewModel(ergastService.Value, informationService.Value);
        
        public SeasonPopupPageViewModel SeasonPopupPage => new SeasonPopupPageViewModel();
        public RaceTypePopupPageViewModel RaceTypePopupPage => new RaceTypePopupPageViewModel();
    }
}
