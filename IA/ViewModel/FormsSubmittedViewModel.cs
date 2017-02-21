using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices.Sync;
using IA.Helpers;
using System.Collections.Generic;

namespace IA
{
	public class FormsSubmittedViewModel : BaseViewModel
	{

		public ObservableCollection<FormItem> FormList { get; set;}

		public FormsSubmittedViewModel(Page page):base (page)
		{
			FormList = new ObservableCollection<FormItem>();
			Title = "Submissions";
		}

		#region GET SUBMITTED FORMS

		public bool FORCE_REFRESH = false;

		private Command getFormsCommand;
		public Command GetFormsCommand
		{
			get
			{
				return getFormsCommand ??
					(getFormsCommand = new Command(async () => await ExecuteGetFormsCommand(), () =>
					{
						return !IsBusy;
					}));
			}
		}

		private async Task ExecuteGetFormsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;
			GetFormsCommand.ChangeCanExecute();
			try
			{
				FormList.Clear();

				List<FormItem> tempList = new List<FormItem>();

				if (FORCE_REFRESH)
				{
					tempList = await App.azureService.RefreshForms(Settings.Current.CurrentUser.userID);
					FORCE_REFRESH = false;
				}

				else{
					tempList = await App.azureService.GetForms(Settings.Current.CurrentUser.userID);
				}

				foreach (var form in tempList)
				{
					FormList.Add(form);
				}

			}
			catch (MobileServicePushFailedException ex)
			{
				await page.DisplayAlert("Uh Oh :(", "Unable to sync forms: " + ex.PushResult, "OK");

			}
			finally
			{
				IsBusy = false;
				GetFormsCommand.ChangeCanExecute();
			}

		}

		#endregion

		private Command deleteFormCommand;
		public Command DeleteFormCommand
		{
			get
			{
				return deleteFormCommand ??
					(deleteFormCommand = new Command(async (obj) => await ExecuteDeleteFormCommand(obj)));
			}
		}

		private async Task ExecuteDeleteFormCommand(object _itemToDelete)
		{
			if (IsBusy)
				return;

			DeleteFormCommand.ChangeCanExecute();
			var item = _itemToDelete as FormItem;

			var res = await page.DisplayAlert("Delete Item ", item.FirstName + "?", "OK", "Cancel");


			if (res)
			{
				IsBusy = true;

				var _deleted = await App.azureService.DeleteForm(item);
				if (_deleted == "SUCCESS")
				{
					FormList.Remove(item);
				}
				else {
					await page.DisplayAlert("Oh No!", "Could not delete item, please check your connection", "OK");
				}
				IsBusy = false;
			}

		}

	}
}
