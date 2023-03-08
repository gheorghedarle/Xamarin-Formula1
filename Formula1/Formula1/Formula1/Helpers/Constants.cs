using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Formula1.Helpers
{
    public static class Constants
    {
        #region BaseURL

        #if DEBUG
                public static string ImageApiBaseUrl = "http://10.0.2.2:4000/";
                public static string InformationsApiBaseUrl = "http://10.0.2.2:5000/";
        #else
                public static string ImageApiBaseUrl = "Github Url";
                public static string InformationsApiBaseUrl = "Github Url";
        #endif

        #endregion

        #region PopupSize

        public static Size PopupSizeSmall => new Size(0.8 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density), 0.5 * (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density));
        
        public static Size PopupSizeMedium => new Size(0.8 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density), 0.7 * (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density));

        public static Size PopupSizeLarge => new Size(0.9 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density), 0.8 * (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density));

        #endregion

        #region Seasons

        public static List<int> GetSeasonsList()
        {
            var seasons = new List<int>();
            for(int i = DateTime.Now.Year; i >= 1950; i--)
            {
                seasons.Add(i);
            }
            return seasons;
        }

        #endregion

        #region Race Types

        public static List<string> GetRaceTypesList()
        {
            return new List<string>() { "Race", "Qualification", "Sprint" };
        }

        #endregion

        #region Team Color

        public static Dictionary<string, string> TeamColors = new Dictionary<string, string>()
        {
            { "alfa", "#C92D4B"},
            { "alphatauri", "#5E8FAA"},
            { "alpine", "#2293D1"},
            { "aston_martin", "#358C75"},
            { "ferrari", "#ED1C24"},
            { "haas", "#B6BABD"},
            { "mclaren", "#F58020"},
            { "mercedes", "#6CD3BF"},
            { "red_bull", "#1E5BC6"},
            { "williams", "#37BEDD"},
        };

        #endregion
    }
}
