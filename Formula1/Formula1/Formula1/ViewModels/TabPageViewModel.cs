using Formula1.ViewModels.TabViews;
using Prism.Navigation;

namespace Formula1.ViewModels
{
    public class TabPageViewModel : BaseViewModel
    {

        #region Constructors

        public TabPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        #endregion
    }
}
