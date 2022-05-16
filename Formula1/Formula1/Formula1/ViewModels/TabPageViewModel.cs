using Formula1.ViewModels.TabViews;
using Prism.Navigation;

namespace Formula1.ViewModels
{
    public class TabPageViewModel : BaseViewModel
    {
        public static DriversViewModel DriversViewModel { get; set; }

        #region Constructors

        public TabPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            DriversViewModel = new DriversViewModel(navigationService);
        }

        #endregion
    }
}
