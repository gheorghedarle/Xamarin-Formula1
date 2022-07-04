using Formula1.Core;
using Formula1.Services.Ergast;
using Formula1.ViewModels.Popups;
using Formula1.ViewModels.TabViews;
using System;

namespace Formula1.ViewModels
{
    public class ViewModelLocator
    {
        private readonly Lazy<HttpClientFactory> httpClientFactory;
        private readonly Lazy<IErgastService> ergastService;

        public ViewModelLocator()
        {
            httpClientFactory = new Lazy<HttpClientFactory>(() => new HttpClientFactory());
            ergastService = new Lazy<IErgastService>(() => new ErgastService(httpClientFactory.Value));
        }

        public WelcomePageViewModel WelcomePage => new WelcomePageViewModel();
        public HomeViewModel HomeView => new HomeViewModel();
        public ScheduleViewModel ScheduleView => new ScheduleViewModel(ergastService.Value);
        public DriversViewModel DriversView => new DriversViewModel(ergastService.Value);
        public TeamsViewModel TeamsView => new TeamsViewModel(ergastService.Value);
        public HistoryViewModel HistoryView => new HistoryViewModel(ergastService.Value);
        public DriverDetailsPageViewModel DriverDetailsPage => new DriverDetailsPageViewModel(ergastService.Value);
        public CircuitDetailsPageViewModel CircuitDetailsPage => new CircuitDetailsPageViewModel(ergastService.Value);
        public SeasonPopupPageViewModel SeasonPopupPage => new SeasonPopupPageViewModel();
        public RaceTypePopupPageViewModel RaceTypePopupPage => new RaceTypePopupPageViewModel();
    }
}
