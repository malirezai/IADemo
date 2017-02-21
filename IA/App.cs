using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;
using IA.Helpers;

namespace IA
{
	public class App : Application
	{
		public static AzureDataService azureService;
		public static List<FormItem> submittedForms;

		public static bool USING_AUTH = true;
		public static bool DidSubmitNewForm = false;
		public static MainTabPage mainTabPage  = new MainTabPage();

		public App()
		{
			
			//create Azure Data Service
			App.azureService = new AzureDataService();
			App.submittedForms = new List<FormItem>();

			// The root page of your application
			//mainTabPage = new MainTabPage();
			var _navigationPage = new NavigationPage(mainTabPage);

			MainPage = _navigationPage;
		}

		protected async override void OnStart()
		{
			// Handle when your app starts

			//TODO: Check to see if user is logged in already or not. 
			if (String.IsNullOrEmpty(Settings.Current.CurrentUser.AuthToken))
			{
				await MainPage.Navigation.PushAsync(new LoginPage(),true);
			}
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
