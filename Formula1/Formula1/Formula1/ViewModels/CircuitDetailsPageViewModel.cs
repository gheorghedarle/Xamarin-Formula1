using Formula1.Models;
using Formula1.Services.Ergast;
using Formula1.Services.Information;
using Formula1.Views.Popups;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Formula1.ViewModels
{
    public class CircuitDetailsPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Fields

        private readonly IErgastService _ergastService;
        private readonly IInformationService _informationsService;

        #endregion

        #region Properties

        public RaceEventResultsModel Results { get; set; }
        public RaceEventModel RaceEvent { get; set; }
        public CircuitBasicInformationsModel CircuitInformations { get; set; }
        public string SelectedRaceType { get; set; }
        public int SelectedTab { get; set; }

        public LayoutState ResultsState { get; set; }
        public LayoutState InformationsState { get; set; }

        #endregion

        #region Commands

        public Command BackCommand { get; set; }
        public Command SelectRaceTypeCommand { get; set; }
        public Command ViewLapByLapCommand { get; set; }

        #endregion

        #region Constructors

        public CircuitDetailsPageViewModel(
            IErgastService ergastService,
            IInformationService informationsService)
        {
            _ergastService = ergastService;
            _informationsService = informationsService;

            BackCommand = new Command(BackCommandHandler);
            SelectRaceTypeCommand = new Command(SelectRaceTypeCommandHandler);
            ViewLapByLapCommand = new Command<RaceResultModel>(ViewLapByLapCommandHandler);
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
                ResultsState = LayoutState.Loading;
                await GetResults();
            }
        }
        private async void ViewLapByLapCommandHandler(RaceResultModel result)
        {
            await Shell.Current.GoToAsync($"laps?round={RaceEvent.Round}&driverId={result.Driver.DriverId}");
        }

        #endregion

        #region IQueryAttributable

        public async void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            query.TryGetValue("round", out var roundParam);
            query.TryGetValue("selectedTab", out var selectedTabParam);
            if (!string.IsNullOrEmpty(roundParam))
            {
                var round = Convert.ToInt32(roundParam);
                MainState = LayoutState.Loading;
                SelectedRaceType = "Race";
                SelectedTab = string.IsNullOrEmpty(selectedTabParam) ? 0 : Convert.ToInt32(selectedTabParam);
                await GetRaceEvent(round);
            }
        }

        #endregion

        #region Private Functionality

        private async Task GetRaceEvent(int round)
        {
            var res = await _ergastService.GetRaceEventInformations("current", round);
            if (res != null)
            {
                RaceEvent = res;
                SelectedRaceType = "Race";
                MainState = LayoutState.None;
                ResultsState = LayoutState.Loading;
                InformationsState = LayoutState.Loading;
                await GetResults();
                await GetInformations();
            }
        }

        private async Task GetResults()
        {
            var res = await _ergastService.GetResults("current", RaceEvent.Round.ToString(), ConvertNameToRaceType(SelectedRaceType));
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
                if(SelectedRaceType == "Sprint")
                {
                    ResultsState = LayoutState.Success;
                }
                else
                {
                    ResultsState = LayoutState.Empty;
                }
            }
        }

        private async Task GetInformations()
        {
            var res = await _informationsService.GetCircuitInformation(RaceEvent.Circuit.Location.Country);
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
            return name.ToLower() switch
            {
                "race" => "results",
                "qualification" => "qualifying",
                "sprint" => "sprint",
                _ => "results",
            };
        }

        #endregion
    }
}
