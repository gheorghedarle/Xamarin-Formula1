using Prism.Navigation;

namespace Formula1.ViewModels.TabViews
{
    public class ScheduleViewModel: BaseViewModel
    {
        #region Constructors

        public ScheduleViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Schedule";
        }

        #endregion
    }
}
