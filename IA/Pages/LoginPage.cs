using System;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;
using IA.Interfaces;
using IA.Helpers;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.MobileServices;
using System.Net.Http;

namespace IA
{
	public class LoginPage : ContentPage
	{
		public static string authority = "https://login.windows.net/common";
		public static string ResourceID = Settings.Current.ServerAppID;
		public static string clientId = Settings.Current.MobileAppID; 
		public static string returnUri = Settings.Current.MobileAppUrl;

		private AuthenticationResult authResult = null;


		public LoginPage()
		{
			NavigationPage.SetHasBackButton(this, false);

			Title = "Login";

			var image = new Image
			{
				Source = ImageSource.FromFile("Logo_iA_Groupe.png")

			};

			var loginButton = new Button
			{
				Text = "Login",
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				BackgroundColor = Color.Transparent,
				TextColor = Color.FromRgb(7, 62, 164),
				FontSize = 18,
				BorderRadius = 5,
				BorderWidth = 2,
				BorderColor = Color.FromRgb(7, 62, 164),
				WidthRequest = 140,
				Margin = 50

			};

			loginButton.Clicked += async (sender, e) =>
			{

				if (String.IsNullOrEmpty(Settings.Current.CurrentUser.AuthToken))
				{

					App.USING_AUTH = true;
					var auth = DependencyService.Get<IAuthenticator>();

					// USING CLIENT SIDE AUTH FLOW THEN POSTING THE INFORMATION INTO AZURE 
					IsBusy = true;
					var data = await auth.Authenticate(authority, ResourceID, clientId, returnUri);

					if (data != null)
					{
						var _firstName = data.UserInfo.GivenName;
						var _lastName = data.UserInfo.FamilyName;
						var _token = data.AccessToken;
						var _expiry = data.ExpiresOn;
						var _userID = data.UserInfo.DisplayableId;


						if (!String.IsNullOrEmpty(_token))
						{
							Settings.Current.CurrentUser = new UserModel
							{
								firstName = _firstName,
								lastName = _lastName,
								AuthToken = _token,
								authExpiry = _expiry,
								userID = _userID
							};
						}

						if (App.USING_AUTH)
						{
							JObject payload = new JObject();
							payload["access_token"] = Settings.Current.CurrentUser.AuthToken;

							Dictionary<string, string> _vals = new Dictionary<string, string>
							{
								{"mobileServiceID",Settings.Current.CurrentUser.userID}
							};

							try
							{
								await App.azureService.Initialize();

								var _res = await App.azureService.MobileService.LoginAsync(MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory, payload);

								//var res = await App.azureService.MobileService.InvokeApiAsync<string>("/api/v1.0/getUser", HttpMethod.Get, _vals);

								if (_res!=null)
								{
									await Navigation.PopAsync();
								}
								IsBusy = false;
							}
							catch (Exception exc)
							{
								IsBusy = false;
								await DisplayAlert("Uh-Oh :(", "We were unable to authenticate you", "Cancel");
							}
						}
					}
					IsBusy = false;
				}

			};

			Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.Center,
				Children = {
						image,
						loginButton
					},
				Padding = 40
			};



		}
	}
}
