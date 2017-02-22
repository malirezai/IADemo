using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace IA
{
	public partial class FormsSubmittedListPageXaml : ContentPage
	{

		FormsSubmittedViewModel viewModel;

		protected override void OnAppearing()
		{
			base.OnAppearing();
			App.mainTabPage.Title = "Submitted Forms";

			if (viewModel.FormList.Count > 0 || viewModel.IsBusy)
			{
				if (!App.DidSubmitNewForm)
				{
					return;
				}
			}
			App.DidSubmitNewForm = false;
			viewModel.GetFormsCommand.Execute(null);
		}

		public FormsSubmittedListPageXaml()
		{
			
			InitializeComponent();

			if (Device.OS == TargetPlatform.iOS)
			{
				Icon = "submitted.png";
			}

			BindingContext = viewModel = new FormsSubmittedViewModel(this);

			FormsSubmittedList.IsPullToRefreshEnabled = true;

			FormsSubmittedList.Refreshing += (sender, e) =>
			{
				viewModel.FORCE_REFRESH = true;
				viewModel.GetFormsCommand.Execute(true);
				FormsSubmittedList.IsRefreshing = false;
			};

		}

		public void OnMore(object sender, EventArgs e)
		{
			var mi = (FormItem)((MenuItem)sender).CommandParameter;
			DisplayAlert("More Context Action", mi.FirstName, "OK");
		}

		public async void OnDelete(object sender, EventArgs e)
		{
			var mi = (FormItem)((MenuItem)sender).CommandParameter;
			viewModel.DeleteFormCommand.Execute(mi);
		}
	}
}
