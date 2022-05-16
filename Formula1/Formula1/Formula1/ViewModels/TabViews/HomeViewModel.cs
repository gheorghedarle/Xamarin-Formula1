using Prism.Navigation;

namespace Formula1.ViewModels.TabViews
{
    public class HomeViewModel: BaseViewModel
    {
        #region Constructors

        public HomeViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Home";
        }

        #endregion
    }
}
