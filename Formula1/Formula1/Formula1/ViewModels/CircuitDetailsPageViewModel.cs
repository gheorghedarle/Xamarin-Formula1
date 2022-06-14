using Formula1.Models;
using Xamarin.Forms;

namespace Formula1.ViewModels
{
    public class CircuitDetailsPageViewModel : BaseViewModel
    {
        #region Properties

        public ScheduleModel Circuit { get; set; }

        #endregion

        #region Commands

        public Command BackCommand { get; set; }

        #endregion

        #region Constructors

        public CircuitDetailsPageViewModel()
        {
            BackCommand = new Command(BackCommandHandler);
        }

        #endregion

        #region Command Handlers

        private async void BackCommandHandler()
        {
            //await _navigationService.GoBackAsync();
            await Shell.Current.Navigation.PopAsync();
        }

        #endregion

        #region Navigation Handlers

        //public override void OnNavigatedTo(INavigationParameters parameters)
        //{
        //    var circuit = parameters.GetValue<ScheduleModel>("circuit");
        //    if (circuit != null)
        //    {
        //        Circuit = circuit;
        //    }
        //}

        #endregion
    }
}
