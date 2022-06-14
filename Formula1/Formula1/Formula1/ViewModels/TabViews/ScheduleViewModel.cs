using Formula1.Models;
using Formula1.Services.Ergast;
using Formula1.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
            //var param = new NavigationParameters()
            //{
            //    { "circuit", circuit }
            //};
            //await _navigationService.NavigateAsync(nameof(CircuitDetailsPage), param);
            await Shell.Current.Navigation.PushAsync(new CircuitDetailsPage());
        }

        #endregion

        #region Private Functionality

        private async Task Initialize()
        {
            var res = await _ergastService.GetSchedule("current");
            Schedule = new ObservableCollection<ScheduleModel>(res);
        }

        #endregion
    }
}
