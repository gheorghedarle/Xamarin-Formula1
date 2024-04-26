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

        public static Size PopupSizeSmall => new(0.8 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density), 0.5 * (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density));

        public static Size PopupSizeMedium => new(0.8 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density), 0.7 * (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density));

        public static Size PopupSizeLarge => new(0.9 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density), 0.8 * (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density));

        #endregion

        #region Seasons

        public static List<int> GetSeasonsList()
        {
            var seasons = new List<int>();
            for (int i = DateTime.Now.Year; i >= 1950; i--)
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

        public static Dictionary<string, string> TeamColors = new()
        {
            { "sauber", "#52e252"},
            { "rb", "#6692FF"},
            { "alpine", "#0093CC"},
            { "aston_martin", "#229971"},
            { "ferrari", "#E80020"},
            { "haas", "#B6BABD"},
            { "mclaren", "#FF8000"},
            { "mercedes", "#27F4D2"},
            { "red_bull", "#3671C6"},
            { "williams", "#64C4FF"},
        };

        #endregion
    }
}
