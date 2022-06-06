using Formula1.Models;
using Formula1.Services.Ergast;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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

        #region Constructors

        public ScheduleViewModel(
            INavigationService navigationService,
            IErgastService ergastService) : base(navigationService)
        {
            Title = "Schedule";

            _ergastService = ergastService;

            Init = Initialize();
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
