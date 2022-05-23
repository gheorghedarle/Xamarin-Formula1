using Formula1.Models;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace Formula1.ViewModels.TabViews
{
    public class TeamsViewModel: BaseViewModel
    {
        #region Properties

        public ObservableCollection<TeamModel> TeamsList { get; set; }

        #endregion

        #region Constructors

        public TeamsViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Teams";

            TeamsList = new ObservableCollection<TeamModel>()
            {
                new TeamModel()
                {
                    Position = 1,
                    Name = "Red Bull",
                    Points = 195,
                },
                new TeamModel()
                {
                    Position = 2,
                    Name = "Ferrari",
                    Points = 169,
                },
                new TeamModel()
                {
                    Position = 3,
                    Name = "Mercedes",
                    Points = 120,
                }
            };
        }

        #endregion
    }
}
