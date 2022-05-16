using Prism.Navigation;

namespace Formula1.ViewModels.TabViews
{
    public class TeamsViewModel: BaseViewModel
    {
        #region Constructors

        public TeamsViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Teams";
        }

        #endregion
    }
}
