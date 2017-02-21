using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace IA.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

			UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);


			UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(7, 62, 164); //bar background
			UINavigationBar.Appearance.TintColor = UIColor.White; //Tint color of button items
			UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes { ForegroundColor = UIColor.White };

			UITabBar.Appearance.TintColor = UIColor.FromRGB(54, 82, 113);

			UIBarButtonItem.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.White }, UIControlState.Normal);




			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
