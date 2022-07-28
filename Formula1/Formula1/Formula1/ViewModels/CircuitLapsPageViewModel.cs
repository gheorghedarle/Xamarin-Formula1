using Formula1.Models;
using Formula1.Services.Ergast;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Formula1.ViewModels
{
    public class CircuitLapsPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Fields

        private readonly IErgastService _ergastService;

        #endregion

        #region Properties

        public RaceResultsLapByLapModel LapResults { get; set; }

        public LayoutState LapsState { get; set; }

        #endregion

        #region Commands

        public Command BackCommand { get; set; }

        #endregion

        #region Constructors

        public CircuitLapsPageViewModel(
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
            query.TryGetValue("round", out var roundParam);
            query.TryGetValue("driverId", out var driverIdParam);
            if (!string.IsNullOrEmpty(roundParam) && !string.IsNullOrEmpty(driverIdParam))
            {
                var round = Convert.ToInt32(roundParam);
                var driverId = driverIdParam.ToString();
                LapsState = LayoutState.Loading;
                await GetLapResults(round, driverId);
            }
        }

        #endregion

        #region Private Functionality

        private async Task GetLapResults(int round, string driverId)
        {
            var res = await _ergastService.GetResultsLapByLap("current", round, driverId);
            if (res != null)
            {
                LapResults = res.Count > 0 ? res.First() : null;
                LapsState = LayoutState.None;
            }
            else
            {
                LapResults = null;
                LapsState = LayoutState.Empty;
            }
        }

        #endregion
    }
}
