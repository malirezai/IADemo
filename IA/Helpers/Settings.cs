// Helpers/Settings.cs
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace IA.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public class Settings : INotifyPropertyChanged
	{

		static Settings settings;
		public static Settings Current
		{
			get { return settings ?? (settings = new Settings()); }
		}

		#region MOBILEAPPID & CLIENTAPPID & REDIRECTURI

		const string MobileAppUrlKey = nameof(MobileAppUrlKey);
		const string DefaultMobileAppUrl = "https://iademoservice.azurewebsites.net/.auth/login/done";
		public string MobileAppUrl
		{
			get { return AppSettings.GetValueOrDefault<string>(MobileAppUrlKey, DefaultMobileAppUrl); }

			set { AppSettings.AddOrUpdateValue<string>(MobileAppUrlKey, value); }
		}

		const string MobileAppIDKey = nameof(MobileAppIDKey);
		const string DefaultMobileAppID = "386818ff-0c39-4bbb-9072-b920eda934f2";
		public string MobileAppID 
		{
			get { return AppSettings.GetValueOrDefault<string>(MobileAppIDKey, DefaultMobileAppID); }

		}

		const string ServerAppIDKey = nameof(ServerAppIDKey);
		const string DefaultServerAppID = "15c6402c-140c-4962-8927-8e6b0d3afe3d"; 

		public string ServerAppID
		{
			get { return AppSettings.GetValueOrDefault<string>(ServerAppIDKey, DefaultServerAppID); }

		}



		#endregion

		#region USER INFO

		private const string CurrentUserIdKey = nameof(CurrentUserIdKey);
		public const string DefaultCurrentUserId = "";

		public UserModel CurrentUser
		{
			get
			{
				string obj = AppSettings.GetValueOrDefault<string>(CurrentUserIdKey, DefaultCurrentUserId);
				if (obj == "null" || obj == "")
				{
					return new UserModel();
				}

				return JsonConvert.DeserializeObject<UserModel>(obj);
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>(CurrentUserIdKey, JsonConvert.SerializeObject(value));
			}
		}

		#endregion

		#region Setting Constants

		private const string SettingsKey = "settings_key";
		private static readonly string SettingsDefault = string.Empty;
		#endregion

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged([CallerMemberName]string name = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		#endregion


		public static string GeneralSettings
		{
			get
			{
				return AppSettings.GetValueOrDefault<string>(SettingsKey, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>(SettingsKey, value);
			}
		}

		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}
	}
}