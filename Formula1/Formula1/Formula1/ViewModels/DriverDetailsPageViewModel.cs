using Formula1.Models;
using Formula1.Services.Ergast;
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

        #endregion

        #region Properties
        
        public ObservableCollection<RaceEventModel> RaceResults { get; set; }
        public DriverStadingsModel DriverStading { get; set; }

        public LayoutState ResultsState { get; set; }

        #endregion

        #region Commands

        public Command BackCommand { get; set; }

        #endregion

        #region Constructors

        public DriverDetailsPageViewModel(
            IErgastService ergastService)
        {
            _ergastService = ergastService;

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
                await GetResults();
            }
        }

        #endregion

        #region Private Functionality

        private async Task GetResults()
        {
            ResultsState = LayoutState.Loading;
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

        #endregion
    }
}
