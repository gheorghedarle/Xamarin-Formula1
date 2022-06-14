using Formula1.Models;
using Formula1.Services.Ergast;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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

        #region Constructors

        public TeamsViewModel(
            IErgastService ergastService)
        {
            Title = "Team";

            _ergastService = ergastService;

            Init = Initialize();
        }

        #endregion

        #region Private Functionality

        private async Task Initialize()
        {
            var res = await _ergastService.GetTeamStadings("current");
            TeamsList = new ObservableCollection<ConstructorStadingsModel>(res);
        }

        #endregion
    }
}
