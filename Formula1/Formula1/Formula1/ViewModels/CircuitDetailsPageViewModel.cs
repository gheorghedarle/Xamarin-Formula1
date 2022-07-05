using Formula1.Models;
using Formula1.Services.Ergast;
using Formula1.Views.Popups;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Xamarin.CommunityToolkit.Extensions;
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
        public RaceEventModel Circuit { get; set; }
        public string SelectedRaceType { get; set; }

        public LayoutState ResultsState { get; set; }

        #endregion

        #region Commands

        public Command BackCommand { get; set; }
        public Command SelectRaceTypeCommand { get; set; }

        #endregion

        #region Constructors

        public CircuitDetailsPageViewModel(
            IErgastService ergastService)
        {
            _ergastService = ergastService;

            BackCommand = new Command(BackCommandHandler);
            SelectRaceTypeCommand = new Command(SelectRaceTypeCommandHandler);
        }

        #endregion

        #region Command Handlers

        private async void BackCommandHandler()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        private async void SelectRaceTypeCommandHandler()
        {
            var raceType = await Shell.Current.Navigation.ShowPopupAsync(new RaceTypePopupPage());
            SelectedRaceType = raceType.ToString();
        }

        #endregion

        #region IQueryAttributable

        public async void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            string circuitString = HttpUtility.UrlDecode(query["circuit"]);
            var circuit = JsonConvert.DeserializeObject<RaceEventModel>(circuitString);
            Circuit = circuit;
            SelectedRaceType = "Race";
            await GetResults();
        }

        #endregion

        #region Private Functionality

        private async Task GetResults()
        {
            ResultsState = LayoutState.Loading;
            var res = await _ergastService.GetResults("current", Circuit.Round.ToString(), ConvertNameToRaceType(SelectedRaceType));
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

        private string ConvertNameToRaceType(string name)
        {
            switch(name.ToLower())
            {
                case "race": return "results";
                case "qualification": return "qualifying";
                case "sprint": return "sprint";
                default: return "results";
            }
        }

        #endregion
    }
}
