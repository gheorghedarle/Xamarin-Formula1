using Formula1.Models;
using Formula1.Views.Templates;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Formula1.Helpers.TemplateSelectors
{
    internal class HomeScheduleTemplateSelector : DataTemplateSelector
    {
        public DataTemplate HomeScheduleTemplate { get; set; }
        public DataTemplate HomeScheduleMoreTemplate { get; set; }

        public HomeScheduleTemplateSelector()
        {
            HomeScheduleTemplate = new DataTemplate(typeof(HomeScheduleTemplate));
            HomeScheduleMoreTemplate = new DataTemplate(typeof(HomeScheduleMoreTemplate));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item.GetType() == typeof(RaceEventModel))
            {
                var resultsItem = item as RaceEventModel;
                if (resultsItem.RaceName == null)
                {
                    return HomeScheduleMoreTemplate;
                }
                else
                {
                    return HomeScheduleTemplate;
                }
            }
            return HomeScheduleTemplate;
        }
    }
}
