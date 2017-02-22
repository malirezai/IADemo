using System;
using System.Collections.Generic;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Plugin.Media.Abstractions;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace IA
{
	public partial class StaticFormPage : ContentPage
	{
		StaticFormViewModel viewModel;

		public StaticFormPage()
		{
			BindingContext = viewModel = new StaticFormViewModel(this);

			InitializeComponent();

			if (Device.OS == TargetPlatform.iOS)
			{
				Icon = "form_icon.png";
			}

			var submitButtonToolbar = new ToolbarItem
			{
				Text = "Submit"
			};

			submitButtonToolbar.Clicked += (sender, e) =>
			{
				//we will submit the form
				viewModel.SubmitFormsCommand.Execute(true);
			};

			ToolbarItems.Add(submitButtonToolbar);




			#region Photo Source 

			//PhotoButton.Clicked += (sender, e) =>
			//{
			//	viewModel.TakePhotoCommand.Execute(null);
			//};

			#endregion


			PhotoButton.Clicked += async (sender, args) =>
			{
				await CrossMedia.Current.Initialize();

				if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
				{
					await DisplayAlert("No Camera", ":( No camera available.", "OK");
					return;
				}

				var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
				{
					Directory = "Sample",
					Name = "test.jpg",
					CompressionQuality = 80,
					PhotoSize = PhotoSize.Custom,
					CustomPhotoSize = 20
				});

				if (file == null)
					return;

				//imagePreview.Source = ImageSource.FromStream(() =>
				//{
				//	var stream = file.GetStream();
				//	file.Dispose();
				//	return stream;
				//});

				var stream = file.GetStream();
				var bytes = new byte[stream.Length];
				await stream.ReadAsync(bytes, 0, (int)stream.Length);
				var b64string = Convert.ToBase64String(bytes);

				viewModel.imageB64List.Add(b64string);

				imagePreview.Source = ImageSource.FromStream(() =>
				{
					return new MemoryStream(Convert.FromBase64String(b64string));
				});

				//or:
				//imagePreview.Source = ImageSource.FromFile(file.Path);
				file.Dispose();

			};


		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			App.mainTabPage.Title = "Static";
		}
	}
}
