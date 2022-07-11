using Formula1.Models;
using Formula1.Services.Ergast;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Formula1.ViewModels.TabViews
{
    public class ScheduleViewModel: BaseViewModel
    {
        #region Fields

        private readonly IErgastService _ergastService;

        #endregion

        #region Properties

        public Task Init { get; }

        public ObservableCollection<RaceEventModel> UpcomingRaceEventList { get; set; }
        public ObservableCollection<RaceEventModel> PastRaceEventList { get; set; }

        #endregion

        #region Commands

        public Command CircuitDetailsCommand { get; set; }

        #endregion

        #region Constructors

        public ScheduleViewModel(
            IErgastService ergastService)
        {
            Title = "Schedule";
            _ergastService = ergastService;

            CircuitDetailsCommand = new Command<RaceEventModel>(CircuitDetailsCommandHandler);

            Init = Initialize();
        }

        #endregion

        #region Command Handlers

        private async void CircuitDetailsCommandHandler(RaceEventModel circuit)
        {
            await Shell.Current.GoToAsync($"//main/schedule/details?circuit={JsonConvert.SerializeObject(circuit)}");
        }

        #endregion

        #region Private Functionality

        private async Task Initialize()
        {
            MainState = LayoutState.Loading;
            var res = await _ergastService.GetSchedule("current");
            UpcomingRaceEventList = new ObservableCollection<RaceEventModel>(res.UpcomingRaceEvents);
            PastRaceEventList = new ObservableCollection<RaceEventModel>(res.PastRaceEvents.OrderByDescending(r => r.Round));
            MainState = LayoutState.None;
        }

        #endregion
    }
}
