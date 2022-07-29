using Formula1.Models;
using Formula1.Views.Views;
using Xamarin.Forms;

namespace Formula1.Helpers.TemplateSelectors
{
    public class HomeResultsTemplateSelector : DataTemplateSelector
    {
        public DataTemplate HomeResultsTemplateView { get; set; }
        public DataTemplate HomeResultsMoreTemplateView { get; set; }

        public HomeResultsTemplateSelector()
        {
            HomeResultsTemplateView = new DataTemplate(typeof(HomeResultsView));
            HomeResultsMoreTemplateView = new DataTemplate(typeof(HomeResultsMoreView));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item.GetType() == typeof(RaceResultModel))
            {
                var resultsItem = item as RaceResultModel;
                if (resultsItem.Driver == null)
                {
                    return HomeResultsMoreTemplateView;
                }
                else
                {
                    return HomeResultsTemplateView;
                }
            }
            return HomeResultsTemplateView;
        }
    }
}
