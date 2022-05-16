using Prism.Navigation;

namespace Formula1.ViewModels.TabViews
{
    public class HistoryViewModel: BaseViewModel
    {
        #region Constructors

        public HistoryViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "History";
        }

        #endregion
    }
}
