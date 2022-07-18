using Formula1.Models;
using Formula1.Services.Ergast;
using Formula1.Services.Informations;
using Formula1.Views.Popups;
using Newtonsoft.Json;
using System;
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
        private readonly IInformationsService _informationsService;

        #endregion

        #region Properties

        public RaceEventResultsModel Results { get; set; }
        public RaceEventModel Circuit { get; set; }
        public CircuitInformationsModel CircuitInformations { get; set; }
        public string SelectedRaceType { get; set; }
        public int SelectedTab { get; set; }

        public LayoutState ResultsState { get; set; }
        public LayoutState InformationsState { get; set; }

        #endregion

        #region Commands

        public Command BackCommand { get; set; }
        public Command SelectRaceTypeCommand { get; set; }

        #endregion

        #region Constructors

        public CircuitDetailsPageViewModel(
            IErgastService ergastService,
            IInformationsService informationsService)
        {
            _ergastService = ergastService;
            _informationsService = informationsService;

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
            if(raceType != null)
            {
                SelectedRaceType = raceType.ToString();
                await GetResults();
            }
        }

        #endregion

        #region IQueryAttributable

        public async void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            query.TryGetValue("circuit", out var circuitParam);
            query.TryGetValue("selectedTab", out var selectedTabParam);
            string circuitString = string.IsNullOrEmpty(circuitParam) ? "" : HttpUtility.UrlDecode(circuitParam);
            string seledctedTabString = string.IsNullOrEmpty(selectedTabParam) ? "" : HttpUtility.UrlDecode(query["selectedTab"]);
            if(!string.IsNullOrEmpty(circuitString))
            {
                var circuit = JsonConvert.DeserializeObject<RaceEventModel>(circuitString);
                Circuit = circuit;
                SelectedRaceType = "Race";
                await GetResults();
                await GetInformations();
            }
            if (!string.IsNullOrEmpty(seledctedTabString))
            {
                SelectedTab = string.IsNullOrEmpty(seledctedTabString) ? 0 : Convert.ToInt32(seledctedTabString);
            }
        }

        #endregion

        #region Private Functionality

        private async Task GetResults()
        {
            ResultsState = LayoutState.Loading;
            var res = await _ergastService.GetResults("current", Circuit.Round.ToString(), ConvertNameToRaceType(SelectedRaceType));
            if (res != null)
            {
                Results = new RaceEventResultsModel()
                {
                    RaceResults = res.First().Results != null ? new ObservableCollection<RaceResultModel>(res.First().Results) : null,
                    QualifyingResults = res.First().QualifyingResults != null ? new ObservableCollection<QualifyingResultModel>(res.First().QualifyingResults) : null,
                    SprintResults = res.First().SprintResults != null ? new ObservableCollection<RaceResultModel>(res.First().SprintResults) : null,
                };
                ResultsState = LayoutState.None;
            }
            else
            {
                Results = null;
                ResultsState = LayoutState.Empty;
            }
        }

        private async Task GetInformations()
        {
            InformationsState = LayoutState.Loading;
            var res = await _informationsService.GetCircuitInformations(Circuit.Circuit.Location.Country);
            if (res != null)
            {
                CircuitInformations = res;
                InformationsState = LayoutState.None;
            }
            else
            {
                CircuitInformations = null;
                InformationsState = LayoutState.Empty;
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
