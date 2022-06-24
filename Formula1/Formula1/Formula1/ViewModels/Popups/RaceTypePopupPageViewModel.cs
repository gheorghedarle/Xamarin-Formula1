using Formula1.Helpers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Formula1.ViewModels.Popups
{
    public class RaceTypePopupPageViewModel
    {
        #region Properties

        public Task Init { get; }

        public ObservableCollection<string> RaceTypesList { get; set; }

        #endregion

        #region Commands
        public Command SelectRaceTypeCommand { get; set; }

        #endregion

        #region Constructors

        public RaceTypePopupPageViewModel()
        {
            SelectRaceTypeCommand = new Command<string>(SelectRaceTypeCommandHandler);

            Init = Initialize();
        }

        #endregion

        #region Command Handlers

        private void SelectRaceTypeCommandHandler(string raceType)
        {
            MessagingCenter.Send<RaceTypePopupPageViewModel, string>(this, "Dismiss", raceType);
        }

        #endregion

        #region Private Functionality

        private Task Initialize()
        {
            RaceTypesList = new ObservableCollection<string>(Constants.GetRaceTypesList());
            return Task.CompletedTask;
        }

        #endregion
    }
}
