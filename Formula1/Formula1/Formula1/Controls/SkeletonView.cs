using System;
using Xamarin.Forms;

namespace Formula1.Controls
{
    public class SkeletonView : BoxView
    {
        public SkeletonView()
        {
            this.WidthRequest = new Random().Next(100, 150);
            Device.StartTimer(TimeSpan.FromSeconds(1.5), () =>
            {
                this.FadeTo(0.5, 750, Easing.CubicInOut).ContinueWith((x) =>
                {
                    this.FadeTo(1, 750, Easing.CubicInOut);
                });

                return true;
            });
        }
    }
}
