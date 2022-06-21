using Android.Content;
using Android.Widget;
using Formula1;
using Formula1.Droid.Renderers;
using Google.Android.Material.BottomNavigation;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AppShell), typeof(ShellCustomRenderer))]
namespace Formula1.Droid.Renderers
{
    public class ShellCustomRenderer : ShellRenderer

    {
        public ShellCustomRenderer(Context context) : base(context)
        { }


        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        {
            return new MarginedTabBarAppearance();
        }
    }

    public class ToolbarAppearance : IShellToolbarAppearanceTracker
    {
        public void Dispose()
        { }

        public void ResetAppearance(Toolbar toolbar, IShellToolbarTracker toolbarTracker)
        {
            throw new NotImplementedException();
        }

        public void ResetAppearance(AndroidX.AppCompat.Widget.Toolbar toolbar, IShellToolbarTracker toolbarTracker)
        {
            throw new NotImplementedException();
        }

        public void SetAppearance(Toolbar toolbar, IShellToolbarTracker toolbarTracker, ShellAppearance appearance)
        {
            throw new NotImplementedException();
        }

        public void SetAppearance(AndroidX.AppCompat.Widget.Toolbar toolbar, IShellToolbarTracker toolbarTracker, ShellAppearance appearance)
        {
            throw new NotImplementedException();
        }
    }

    public class MarginedTabBarAppearance : IShellBottomNavViewAppearanceTracker
    {
        public void Dispose()
        { }

        public void ResetAppearance(BottomNavigationView bottomView)
        { }


        public void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            bottomView.LabelVisibilityMode = LabelVisibilityMode.LabelVisibilityUnlabeled;
        }
    }
}