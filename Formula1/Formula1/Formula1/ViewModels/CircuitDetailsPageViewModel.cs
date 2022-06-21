using Formula1.Models;
using Formula1.Services.Ergast;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Formula1.ViewModels
{
    public class CircuitDetailsPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Fields

        private readonly IErgastService _ergastService;

        #endregion

        #region Properties

        public ObservableCollection<RaceResultModel> RaceResults { get; set; }
        public ScheduleModel Circuit { get; set; }

        public LayoutState ResultsState { get; set; }

        #endregion

        #region Commands

        public Command BackCommand { get; set; }

        #endregion

        #region Constructors

        public CircuitDetailsPageViewModel(
            IErgastService ergastService)
        {
            _ergastService = ergastService;

            BackCommand = new Command(BackCommandHandler);
        }

        #endregion

        #region Command Handlers

        private async void BackCommandHandler()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        #endregion

        #region IQueryAttributable

        public async void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            string circuitString = HttpUtility.UrlDecode(query["circuit"]);
            var circuit = JsonConvert.DeserializeObject<ScheduleModel>(circuitString);
            Circuit = circuit;
            await GetResults();
        }

        #endregion

        #region Private Functionality

        private async Task GetResults()
        {
            ResultsState = LayoutState.Loading;
            var res = await _ergastService.GetRaceResults("current", Circuit.Round.ToString());
            if(res != null)
            {
                RaceResults = new ObservableCollection<RaceResultModel>(res.First().Results);
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
