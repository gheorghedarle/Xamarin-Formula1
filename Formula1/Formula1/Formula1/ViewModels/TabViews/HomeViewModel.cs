using Formula1.Models;
using Formula1.Services.Ergast;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Formula1.ViewModels.TabViews
{
    public class HomeViewModel: BaseViewModel
    {
        #region Fields

        private readonly IErgastService _ergastService;

        #endregion

        #region Properties

        public Task Init { get; }

        public ObservableCollection<RaceResultModel> LatestResults { get; set; }

        #endregion

        #region Commands

        public Command ProfileCommand { get; set; }

        #endregion

        #region Constructors

        public HomeViewModel(
            IErgastService ergastService)
        {
            Title = "Home";
            _ergastService = ergastService;

            ProfileCommand = new Command(ProfileCommandHandler);

            Init = Initialize();
        }

        #endregion


        #region Command Handlers

        private async void ProfileCommandHandler()
        {
            await Shell.Current.GoToAsync($"profile");
        }

        #endregion

        #region Private Functionality

        private async Task Initialize()
        {
            MainState = LayoutState.Loading;
            var res = await _ergastService.GetResults("current", "last", "results");
            LatestResults = new ObservableCollection<RaceResultModel>(res.First().Results);
            MainState = LayoutState.None;
        }

        #endregion
    }
}
