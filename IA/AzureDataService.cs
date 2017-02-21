using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;
using IA.Helpers;
using System.Net.Http;
using System.Diagnostics;

namespace IA
{
	public class AzureDataService
	{
		public MobileServiceClient MobileService { get; set; }
		IMobileServiceSyncTable<FormItem> formTable;
        

		public async Task Initialize()
		{

			if (MobileService?.SyncContext?.IsInitialized ?? false)
				return;
			//Create our client
			MobileService = new MobileServiceClient("https://iademoservice.azurewebsites.net");

			var path = "syncstore.db";
			path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);

			//setup our local sqlite store and intialize our table
			var store = new MobileServiceSQLiteStore(path);
			store.DefineTable<FormItem>();


			//Get our sync table that will call out to azure
			formTable = MobileService.GetSyncTable<FormItem>();

			// Add images handler
			//this.MobileService.InitializeFileSyncContext(new ImagesFileSyncHandler(this.saleItemsTable), store);

			await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

		}

		public async Task<List<FormItem>> GetForms(string userId)
		{
			await Initialize();
			//the following is an incremental sync and is called when we DONT pull to refresh. 
			await SyncForm();
			return await formTable.OrderByDescending(c => c.EnteredDateUTC).ToListAsync();
		}

		public async Task<List<FormItem>> RefreshForms(string userId)
		{
			await Initialize();
			await formTable.PurgeAsync();
			await formTable.PullAsync(null,formTable.CreateQuery().Where(c => c.UserID == userId));
			return await formTable.OrderByDescending(c => c.EnteredDateUTC).ToListAsync();
		}

		public async Task<string> DeleteForm(FormItem _item)
		{
			await Initialize();

			try
			{
				await formTable.DeleteAsync(_item);
				await SyncForm();
				await MobileService.SyncContext.PushAsync();
				return "SUCCESS";
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"*********{ex}*****");
				return "FAILED";
			}

		}

		public async Task<FormItem> AddForm(string type, string data, string imageSource = "")
		{
			await Initialize();

			var form = new FormItem
			{
				EnteredDateUTC = DateTime.UtcNow.ToString(),
				FirstName = Settings.Current.CurrentUser.firstName,
				LastName = Settings.Current.CurrentUser.lastName,
				UserID = Settings.Current.CurrentUser.userID,
				FormType = type,
				FormData = data,
				Byte64StringImage = imageSource
			};
			try
			{
				await formTable.InsertAsync(form);

				//Synchronize form
				await SyncForm();
				return form;
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"*********{ex}*****");
			}

			return null;
		}

		public async Task SyncForm()
		{
			await formTable.PullAsync("allForms", formTable.CreateQuery().Where(c => c.UserID == Settings.Current.CurrentUser.userID));
			await MobileService.SyncContext.PushAsync();
		}
	}
}
