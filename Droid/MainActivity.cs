using System;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Java.Interop;


[assembly: MetaData("net.hockeyapp.android.appIdentifier", Value = "VALUE")]

namespace IA.Droid
{
	[Activity(Label = "IA.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{

			string HOCKEYAPP_ID = "VALUE";

			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

			InitializeHockeyApp(HOCKEYAPP_ID);

			LoadApplication(new App());
		}


		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);

		}


		//BACKDOOR METHOD FOR SETTING THE USER ID TO TEST CLOUD
		[Export("enableTestCloudUser")]
		public void EnableTestCloudBackdoor()
		{
			App.ON_TESTCLOUD = true;
		}


		void InitializeHockeyApp(string hockeyAppID)
		{
			CrashManager.Register(this, hockeyAppID);
			UpdateManager.Register(this, hockeyAppID, true);
			FeedbackManager.Register(this, hockeyAppID, null);
			MetricsManager.Register(Application);
		}


	}
}
