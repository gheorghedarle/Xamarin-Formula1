﻿using Formula1.Models;
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
    public class TeamDetailsPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Fields

        private readonly IErgastService _ergastService;
        private readonly IInformationsService _informationsService;

        #endregion

        #region Properties

        public ObservableCollection<RaceEventModel> RaceResults { get; set; }
        public ConstructorStadingsModel ConstructorStading { get; set; }
        public ConstructorInformationsModel ConstructorInformations { get; set; }
        public string SelectedSeason { get; set; }

        public LayoutState ResultsState { get; set; }
        public LayoutState InformationsState { get; set; }

        #endregion

        #region Commands

        public Command BackCommand { get; set; }
        public Command SelectSeasonCommand { get; set; }

        #endregion

        #region Constructors

        public TeamDetailsPageViewModel(
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
            }
        }

        #endregion

        #region IQueryAttributable

        public async void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            query.TryGetValue("team", out var teamParam);
            string teamString = HttpUtility.UrlDecode(teamParam);
            if (!string.IsNullOrEmpty(teamString))
            {
                var team = JsonConvert.DeserializeObject<ConstructorStadingsModel>(teamString);
                ConstructorStading = team;
                SelectedSeason = "Current Season";
                ResultsState = LayoutState.Loading;
                InformationsState = LayoutState.Loading;
                //await GetResults();
                await GetInformations();
            }
        }

        #endregion

        #region Private Functionality

        private async Task GetInformations()
        {
            var res = await _informationsService.GetTeamInformations(ConstructorStading.Constructor.Name);
            if (res != null)
            {
                ConstructorInformations = res;
                InformationsState = LayoutState.None;
            }
            else
            {
                ConstructorInformations = null;
                InformationsState = LayoutState.Empty;
            }
        }

        #endregion
    }
}
