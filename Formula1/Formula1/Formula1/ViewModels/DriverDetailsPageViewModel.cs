using Formula1.Models;
using Formula1.Services.Ergast;
using Formula1.Services.Informations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Web;
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

        public LayoutState ResultsState { get; set; }
        public LayoutState InformationsState { get; set; }

        #endregion

        #region Commands

        public Command BackCommand { get; set; }

        #endregion

        #region Constructors

        public DriverDetailsPageViewModel(
            IErgastService ergastService,
            IInformationsService informationsService)
        {
            _ergastService = ergastService;
            _informationsService = informationsService;

            BackCommand = new Command(BackCommandHandler);
        }

        #endregion

        #region Command Handlers

        private async void BackCommandHandler()
        {
            await Shell.Current.GoToAsync("..");
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
            var res = await _ergastService.GetResultsByDriver("current", DriverStading.Driver.DriverId);
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
