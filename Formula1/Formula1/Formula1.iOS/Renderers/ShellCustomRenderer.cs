using Formula1;
using Formula1.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AppShell), typeof(ShellCustomRenderer))]
namespace Formula1.iOS.Renderers
{
    public class ShellCustomRenderer : ShellRenderer
    {
        protected override IShellSectionRenderer CreateShellSectionRenderer(ShellSection shellSection)
        {
            var renderer = base.CreateShellSectionRenderer(shellSection);
            if (renderer != null)
            {

            }
            return renderer;
        }

        protected override IShellTabBarAppearanceTracker CreateTabBarAppearanceTracker()
        {
            return new CustomTabbarAppearance();
        }
    }

    public class CustomTabbarAppearance : IShellTabBarAppearanceTracker
    {
        public void Dispose()
        { }

        public void ResetAppearance(UITabBarController controller)
        { }

        public void SetAppearance(UITabBarController controller, ShellAppearance appearance)
        {
            UITabBar myTabBar = controller.TabBar;

            if (myTabBar.Items != null)
            {
                foreach (UITabBarItem item in myTabBar.Items)
                {
                    item.Title = null;
                    item.ImageInsets = new UIEdgeInsets(10, 0, 0, 0);
                }
            }
        }

        public void UpdateLayout(UITabBarController controller)
        { }
    }
}