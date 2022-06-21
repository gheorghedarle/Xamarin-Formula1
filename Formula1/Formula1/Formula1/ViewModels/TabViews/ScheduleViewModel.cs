using Formula1.Models;
using Formula1.Services.Ergast;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
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

        public ObservableCollection<ScheduleModel> Schedule { get; set; }

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

            CircuitDetailsCommand = new Command<ScheduleModel>(CircuitDetailsCommandHandler);

            Init = Initialize();
        }

        #endregion

        #region Command Handlers

        private async void CircuitDetailsCommandHandler(ScheduleModel circuit)
        {
            await Shell.Current.GoToAsync($"circuitdetails?circuit={JsonConvert.SerializeObject(circuit)}");
        }

        #endregion

        #region Private Functionality

        private async Task Initialize()
        {
            MainState = LayoutState.Loading;
            var res = await _ergastService.GetSchedule("current");
            Schedule = new ObservableCollection<ScheduleModel>(res);
            MainState = LayoutState.None;
        }

        #endregion
    }
}
