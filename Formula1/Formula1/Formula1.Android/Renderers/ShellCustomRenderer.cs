﻿using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Widget;
using Formula1;
using Formula1.Droid.Renderers;
using Google.Android.Material.BottomNavigation;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using AColor = Android.Graphics.Color;
using R = Android.Resource;

[assembly: ExportRenderer(typeof(AppShell), typeof(ShellCustomRenderer))]
namespace Formula1.Droid.Renderers
{
    public class ShellCustomRenderer : ShellRenderer
    {
        public ShellCustomRenderer(Context context) : base(context)
        { }


        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        {
            return new ShellBottomNavViewAppearanceTracker(this, shellItem);
        }
	}

	public class ShellBottomNavViewAppearanceTracker : IShellBottomNavViewAppearanceTracker
	{
		IShellContext _shellContext;
		ShellItem _shellItem;
		ColorStateList _defaultList;
		bool _disposed;
		ColorStateList _colorStateList;

		public ShellBottomNavViewAppearanceTracker(IShellContext shellContext, ShellItem shellItem)
		{
			_shellItem = shellItem;
			_shellContext = shellContext;
		}

		public virtual void ResetAppearance(BottomNavigationView bottomView)
		{
			if (_defaultList != null)
			{
				bottomView.ItemTextColor = _defaultList;
				bottomView.ItemIconTintList = _defaultList;
			}

			SetBackgroundColor(bottomView, Color.White);
		}

		public virtual void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
		{
			IShellAppearanceElement controller = appearance;
			var backgroundColor = controller.EffectiveTabBarBackgroundColor;
			var foregroundColor = controller.EffectiveTabBarForegroundColor; // currently unused
			var disabledColor = controller.EffectiveTabBarDisabledColor;
			var unselectedColor = controller.EffectiveTabBarUnselectedColor;
			var titleColor = controller.EffectiveTabBarTitleColor;

			if (_defaultList == null)
			{
#if __ANDROID_28__
				_defaultList = bottomView.ItemTextColor ?? bottomView.ItemIconTintList
					?? MakeColorStateList(titleColor.ToAndroid().ToArgb(), disabledColor.ToAndroid().ToArgb(), unselectedColor.ToAndroid().ToArgb());
#else
				_defaultList = bottomView.ItemTextColor ?? bottomView.ItemIconTintList;
#endif
			}

			_colorStateList = MakeColorStateList(titleColor, disabledColor, unselectedColor);
			bottomView.ItemTextColor = _colorStateList;
			bottomView.ItemIconTintList = _colorStateList;

			SetBackgroundColor(bottomView, backgroundColor);
			bottomView.LabelVisibilityMode = LabelVisibilityMode.LabelVisibilityUnlabeled;
		}

		protected virtual void SetBackgroundColor(BottomNavigationView bottomView, Color color)
		{
			var menuView = bottomView.GetChildAt(0) as BottomNavigationMenuView;
			var oldBackground = bottomView.Background;
			var colorDrawable = oldBackground as ColorDrawable;
			var colorChangeRevealDrawable = oldBackground as ColorChangeRevealDrawable;
			AColor lastColor = colorDrawable?.Color ?? Color.Default.ToAndroid();
			AColor newColor;

			if (color == Color.Default)
				newColor = Color.White.ToAndroid();
			else
				newColor = color.ToAndroid();

			if (menuView == null)
			{
				if (colorDrawable != null && lastColor == newColor)
					return;

				if (lastColor != newColor || colorDrawable == null)
				{
					bottomView.SetBackground(new ColorDrawable(newColor));
				}
			}
			else
			{
				if (colorChangeRevealDrawable != null && lastColor == newColor)
					return;

				var index = ((IShellItemController)_shellItem).GetItems().IndexOf(_shellItem.CurrentItem);
				var menu = bottomView.Menu;
				index = Math.Min(index, menu.Size() - 1);

				var child = menuView.GetChildAt(index);
				if (child == null)
					return;

				var touchPoint = new Point(child.Left + (child.Right - child.Left) / 2, child.Top + (child.Bottom - child.Top) / 2);

				bottomView.SetBackground(new ColorChangeRevealDrawable(lastColor, newColor, touchPoint));
			}
		}

		ColorStateList MakeColorStateList(Color titleColor, Color disabledColor, Color unselectedColor)
		{
			var disabledInt = disabledColor.IsDefault ?
				_defaultList.GetColorForState(new[] { -R.Attribute.StateEnabled }, AColor.Gray) :
				disabledColor.ToAndroid().ToArgb();

			var checkedInt = titleColor.IsDefault ?
				_defaultList.GetColorForState(new[] { R.Attribute.StateChecked }, AColor.Black) :
				titleColor.ToAndroid().ToArgb();

			var defaultColor = unselectedColor.IsDefault ?
				_defaultList.DefaultColor :
				unselectedColor.ToAndroid().ToArgb();

			return MakeColorStateList(checkedInt, disabledInt, defaultColor);
		}

		ColorStateList MakeColorStateList(int titleColorInt, int disabledColorInt, int defaultColor)
		{
			var states = new int[][] {
				new int[] { -R.Attribute.StateEnabled },
				new int[] {R.Attribute.StateChecked },
				new int[] { }
			};

			var colors = new[] { disabledColorInt, titleColorInt, defaultColor };

			return new ColorStateList(states, colors);
		}

		#region IDisposable

		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed)
				return;

			_disposed = true;

			if (disposing)
			{
				_defaultList?.Dispose();
				_colorStateList?.Dispose();

				_shellItem = null;
				_shellContext = null;
				_defaultList = null;
				_colorStateList = null;
			}
		}

		#endregion IDisposable
	}
}