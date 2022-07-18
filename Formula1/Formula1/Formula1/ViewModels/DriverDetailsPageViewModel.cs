using Formula1.Models;
using Formula1.Services.Ergast;
using Formula1.Services.Informations;
using Formula1.Views.Popups;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Web;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Formula1.ViewModels
{
    public class DriverDetailsPageViewModel: BaseViewModel, IQueryAttributable
    {
        #region Fields

        private readonly IErgastService _ergastService;
        private readonly IInformationsService _informationsService;

        #endregion

        #region Properties
        
        public ObservableCollection<RaceEventModel> RaceResults { get; set; }
        public DriverStadingsModel DriverStading { get; set; }
        public DriverInformationsModel DriverInformations { get; set; }
        public string SelectedSeason { get; set; }

        public LayoutState ResultsState { get; set; }
        public LayoutState InformationsState { get; set; }

        #endregion

        #region Commands

        public Command BackCommand { get; set; }
        public Command SelectSeasonCommand { get; set; }

        #endregion

        #region Constructors

        public DriverDetailsPageViewModel(
            IErgastService ergastService,
            IInformationsService informationsService)
        {
            _ergastService = ergastService;
            _informationsService = informationsService;

            BackCommand = new Command(BackCommandHandler);
            SelectSeasonCommand = new Command(SelectSeasonCommandHandler);
        }

        #endregion

        #region Command Handlers

        private async void BackCommandHandler()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void SelectSeasonCommandHandler()
        {
            var season = await Shell.Current.Navigation.ShowPopupAsync(new SeasonPopupPage());
            if (season != null)
            {
                SelectedSeason = season.ToString() == DateTime.Now.Year.ToString() ? "Current Season" : season.ToString();
                ResultsState = LayoutState.Loading;
                await GetResults();            
            }
        }

        #endregion

        #region IQueryAttributable

        public async void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            query.TryGetValue("driver", out var driverParam);
            string driverString = HttpUtility.UrlDecode(driverParam);
            if(!string.IsNullOrEmpty(driverString))
            {
                var driver = JsonConvert.DeserializeObject<DriverStadingsModel>(driverString);
                DriverStading = driver;
                SelectedSeason = "Current Season";
                ResultsState = LayoutState.Loading;
                InformationsState = LayoutState.Loading;
                await GetResults();
                await GetInformations();
            }
        }

        #endregion

        #region Private Functionality

        private async Task GetResults()
        {
            var season = SelectedSeason == "Current Season" ? "current" : SelectedSeason;
            var res = await _ergastService.GetResultsByDriver(season, DriverStading.Driver.DriverId);
            if (res != null)
            {
                RaceResults = new ObservableCollection<RaceEventModel>(res);
                ResultsState = LayoutState.None;
            }
            else
            {
                RaceResults = null;
                ResultsState = LayoutState.Empty;
            }
        }

        private async Task GetInformations()
        {
            var res = await _informationsService.GetDriverInformations(string.Format("{0}-{1}", DriverStading.Driver.GivenName.ToLower(), DriverStading.Driver.FamilyName.ToLower()));
            if (res != null)
            {
                DriverInformations = res;
                InformationsState = LayoutState.None;
            }
            else
            {
                DriverInformations = null;
                InformationsState = LayoutState.Empty;
            }
        }

        #endregion
    }
}
