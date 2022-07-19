using Formula1.Models;
using Formula1.Services.Ergast;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Formula1.ViewModels.TabViews
{
    public class TeamsViewModel: BaseViewModel
    {
        #region Fields

        private readonly IErgastService _ergastService;

        #endregion

        #region Properties

        public Task Init { get; }

        public ObservableCollection<ConstructorStadingsModel> TeamsList { get; set; }

        #endregion

        #region Commands

        public Command TeamDetailsCommand { get; set; }

        #endregion

        #region Constructors

        public TeamsViewModel(
            IErgastService ergastService)
        {
            Title = "Team";

            _ergastService = ergastService;

            TeamDetailsCommand = new Command<ConstructorStadingsModel>(TeamDetailsCommandHandler);

            Init = Initialize();
        }

        #endregion

        #region Command Handlers

        private async void TeamDetailsCommandHandler(ConstructorStadingsModel team)
        {
            await Shell.Current.GoToAsync($"/details?team={JsonConvert.SerializeObject(team)}");
        }

        #endregion

        #region Private Functionality

        private async Task Initialize()
        {
            MainState = LayoutState.Loading;
            var res = await _ergastService.GetTeamStadings("current");
            TeamsList = new ObservableCollection<ConstructorStadingsModel>(res);
            MainState = LayoutState.None;
        }

        #endregion
    }
}
