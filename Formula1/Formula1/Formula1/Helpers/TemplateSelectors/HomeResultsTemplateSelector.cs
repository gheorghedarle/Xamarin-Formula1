using Formula1.Models;
using Formula1.Views.Templates;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Formula1.Helpers.TemplateSelectors
{
    public class HomeResultsTemplateSelector : DataTemplateSelector
    {
        public DataTemplate HomeResultsTemplate { get; set; }
        public DataTemplate HomeResultsMoreTemplate { get; set; }

        public HomeResultsTemplateSelector()
        {
            HomeResultsTemplate = new DataTemplate(typeof(HomeResultsTemplate));
            HomeResultsMoreTemplate = new DataTemplate(typeof(HomeResultsMoreTemplate));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item.GetType() == typeof(RaceResultModel))
            {
                var resultsItem = item as RaceResultModel;
                if (resultsItem.Driver == null)
                {
                    return HomeResultsMoreTemplate;
                }
                else
                {
                    return HomeResultsTemplate;
                }
            }
            return HomeResultsTemplate;
        }
    }
}
