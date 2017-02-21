using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IA.Helpers;
using Xamarin.Forms;
using System.Linq;

namespace IA
{
	public class StaticFormViewModel : BaseViewModel
	{

		public List<string> imageB64List = new List<string>();

		public StaticFormViewModel(Page page) : base(page)
		{
			Title = "IA";
		}


		private string typeOfService, formName, feesSubmitted;
		public string FormTitle { get; } = "Static Form";
		public DateTime dateOfService = DateTime.Now;


		/// <summary>
		/// Gets or sets the "Title" property
		/// </summary>
		/// <value>The title.</value>
		public string DateOfServiceLabel { get; } = "Date Of Service:";
		public DateTime DateOfService
		{
			get { return dateOfService; }
			set { SetProperty(ref dateOfService, value); }
		}


		public string TypeOfServiceLabel { get; } = "Type Of Service:";
		public string TypeOfService
		{
			get { return typeOfService; }
			set { SetProperty(ref typeOfService, value); }
		}

		public string NameLabel { get; } = "Enter Your Name:";
		public string Name
		{
			get { return formName; }
			set { SetProperty(ref formName, value); }
		}

		public string FeesSubmittedLabel { get; } = "Fees Submitted:";
		public string FeesSubmitted
		{
			get { return feesSubmitted; }
			set { SetProperty(ref feesSubmitted, value); }
		}


		#region SUBMIT FORM

		private Command submitFormsCommand;
		public Command SubmitFormsCommand
		{
			get
			{
				return submitFormsCommand ??
					(submitFormsCommand = new Command(async () => await ExecuteSubmitFormsCommand(), () =>
					{
						return !IsBusy;
					}));
			}
		}

		private async Task ExecuteSubmitFormsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;
			page.IsBusy = true;
			SubmitFormsCommand.ChangeCanExecute();
			try
			{
				var formvalues =  DateOfServiceLabel + DateOfService.ToString() + "~" + TypeOfServiceLabel + TypeOfService + "~" + NameLabel + Name + "~" + FeesSubmittedLabel + FeesSubmitted;
				var form = await App.azureService.AddForm(
					FormTitle, 
					formvalues, 
					imageB64List.FirstOrDefault());

				if (form != null)
				{
					App.submittedForms.Add(form);
					App.mainTabPage.CurrentPage = App.mainTabPage.Children[2];
					App.DidSubmitNewForm = true;
					IsBusy = false;
					page.IsBusy = false;
				}
				else {
					IsBusy = false;
					page.IsBusy = false;
					await page.DisplayAlert("Oh No!", "Could not sync Form", "Cancel");
				}

			}
			catch (Exception ex)
			{
				await page.DisplayAlert("Uh Oh :(", "Unable to submit form: " + ex, "OK");

			}
			finally
			{
				IsBusy = false;
				page.IsBusy = false;
				SubmitFormsCommand.ChangeCanExecute();
			}

		}

		#endregion


		#region CAMERA

		private Command takePhotoCommand;

		//public Command TakePhotoCommand => takePhotoCommand ?? (takePhotoCommand = new Command( async() => await ExecuteTakePhotoCommand()));

		public Command TakePhotoCommand
		{
			get
			{
				return takePhotoCommand ??
					(takePhotoCommand = new Command(async () => await ExecuteTakePhotoCommand(), () =>
					{
						return !IsBusy;
					}));
			}
		}

		private async Task ExecuteTakePhotoCommand()
		{

			TakePhotoCommand.ChangeCanExecute();




		}
		#endregion



	}
}
