using Formula1.Core;
using Formula1.Services.Ergast;
using Formula1.Services.Information;
using Formula1.ViewModels.Popups;
using Formula1.ViewModels.TabViews;
using System;

namespace Formula1.ViewModels
{
    public class ViewModelLocator
    {
        private readonly Lazy<HttpClientFactory> httpClientFactory;
        private readonly Lazy<IErgastService> ergastService;
        private readonly Lazy<IInformationService> informationService;

        public ViewModelLocator()
        {
            httpClientFactory = new Lazy<HttpClientFactory>(() => new HttpClientFactory());
            ergastService = new Lazy<IErgastService>(() => new ErgastService(httpClientFactory.Value));
            informationService = new Lazy<IInformationService>(() => new InformationService(httpClientFactory.Value));
        }

        public WelcomePageViewModel WelcomePage => new();
        public ProfilePageViewModel ProfilePage => new();

        public HomeViewModel HomeView => new(ergastService.Value);
        public ScheduleViewModel ScheduleView => new(ergastService.Value);
        public DriversViewModel DriversView => new(ergastService.Value);
        public TeamsViewModel TeamsView => new(ergastService.Value);
        public HistoryViewModel HistoryView => new(ergastService.Value);

        public DriverDetailsPageViewModel DriverDetailsPage => new(ergastService.Value, informationService.Value);
        public CircuitDetailsPageViewModel CircuitDetailsPage => new(ergastService.Value, informationService.Value);
        public CircuitLapsPageViewModel CircuitLapsPage => new(ergastService.Value);
        public TeamDetailsPageViewModel TeamDetailsPage => new(ergastService.Value, informationService.Value);

        public SeasonPopupPageViewModel SeasonPopupPage => new();
        public RaceTypePopupPageViewModel RaceTypePopupPage => new();
    }
}
