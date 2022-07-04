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
        #else
                    public static string ImageApiBaseUrl = "Github Url";
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
            for(int i = 2021; i >= 1950; i--)
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
    }
}
