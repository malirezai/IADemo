using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;
using IA.Helpers;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace IA
{
	public class FormsPage : ContentPage
	{
		
		JsonSerialize json;
		FormDefinition definition;

		protected override void OnAppearing()
		{
			base.OnAppearing();
			App.mainTabPage.Title = "Dynamic";
		}

        public FormsPage()
        {
            Title = "Dynamic";
            
            if (Device.OS == TargetPlatform.iOS)
            {	
                Icon = "form_icon.png";
            }

			json = new JsonSerialize();
			createForm("form1");

			IsBusy = false;

			var submitButtonToolbar = new ToolbarItem
			{
				Text = "Submit"
			};

			submitButtonToolbar.Clicked += async (sender, e) =>
			{
				//we will submit the form
				await AddForm();
			};

			ToolbarItems.Add(submitButtonToolbar);


			#if DEBUG
			var crashButtonToolBar = new ToolbarItem
			{
				Icon = "Crash",
				//AutomationId = AutomationIdConstants.CrashButton
			};
			crashButtonToolBar.Clicked += (sender, e) =>
			{
				//HockeyappHelpers.TrackEvent(HockeyappConstants.CrashButtonTapped);
				//throw new System.Exception(HockeyappConstants.CrashButtonTapped);
			};
			//ToolbarItems.Add(crashButtonToolBar);
			#endif

		}

		public void buildForm()
		{

			var mainLayout = new StackLayout
			{
				Padding = 20
			};

			foreach (var el in definition.Elements)
			{

				switch (el.Type)
				{
					//Add Label 
					case "Label":
						
						var label = new Label
						{
							Text = ((FormLabel)el).LabelText,
							FontAttributes = FontAttributes.Bold,
							HorizontalOptions = LayoutOptions.FillAndExpand
						};

						//label.SetBinding(Label.TextProperty,$"FormElementCollection[{i}]",BindingMode.TwoWay);

						mainLayout.Children.Add(label);
						break;
					//Add Entry	
					case "Entry":

						mainLayout.Children.Add(new Label
						{
							Text = ((FormEntryField)el).LabelText,
							HorizontalOptions = LayoutOptions.FillAndExpand
						});

						var _entry = new Entry
						{
							Placeholder = ((FormEntryField)el).PlaceHolderText,
							HorizontalOptions = LayoutOptions.FillAndExpand,
							Keyboard = el.NumKeyboard ? Keyboard.Telephone : Keyboard.Default
						};

						_entry.TextChanged += (sender, e) =>
						{
							((FormEntryField)el).Text = e.NewTextValue;
						};

						mainLayout.Children.Add(_entry);

						break;

					//Add text field	
					case "TextField":
						mainLayout.Children.Add(new Label
						{
							Text = ((FormTextField)el).LabelText,
							HorizontalOptions = LayoutOptions.FillAndExpand
						});

						var editor = new EditorGrows//Editor
						{
							//Text = ((FormTextField)el).LabelText,
							HorizontalOptions = LayoutOptions.FillAndExpand
						};

						editor.TextChanged += (sender, e) =>
						{
							((FormTextField)el).Text = e.NewTextValue;
						};

						mainLayout.Children.Add(editor);

						break;
						
					case "Picker":
						mainLayout.Children.Add(new Label
						{
							Text = ((Picker)el).LabelText,
							HorizontalOptions = LayoutOptions.FillAndExpand
						});

						var _picker = new Xamarin.Forms.Picker();
						_picker.HorizontalOptions = LayoutOptions.FillAndExpand;

						foreach (var option in ((Picker)el).Values)
						{
							_picker.Items.Add(option);
						}

						_picker.SelectedIndexChanged += (sender, e) =>
						{
							
							((Picker)el).SelectedValue = ((Picker)el).Values[((Xamarin.Forms.Picker)sender).SelectedIndex];
						};

						mainLayout.Children.Add(_picker);

						break;

					// ADD Switch
					case "Switch":

						mainLayout.Children.Add(new Label
						{
							Text = ((FormSwitch)el).LabelText,
							HorizontalOptions = LayoutOptions.FillAndExpand
						});

						var _switch = new Switch
						{
							IsToggled = ((FormSwitch)el).DefaultValue,
							HorizontalOptions = LayoutOptions.FillAndExpand
						};

						_switch.Toggled += (object sender, ToggledEventArgs e) =>
						{
							((FormSwitch)el).Value = e.Value;
						};

						mainLayout.Children.Add(_switch);

						break;

					// Add Datepicker
					case "DatePicker":

						mainLayout.Children.Add(new Label
						{
							Text = ((DatePicker)el).LabelText,
							HorizontalOptions = LayoutOptions.FillAndExpand
						});

						var datePicker = new Xamarin.Forms.DatePicker
						{
							HorizontalOptions = LayoutOptions.FillAndExpand
						};

						datePicker.DateSelected += (sender, e) => {

							((DatePicker)el).SelectedDate = e.NewDate;
						};

						mainLayout.Children.Add(datePicker);

						break;
					default:
						break;
				}

			}

			this.Content = new ScrollView
			{
				Content = mainLayout
			};

		}

		public void createForm(string path)
		{
			
			definition = json.parseJson(path);

			buildForm();

		}


		async Task AddForm()
		{

			try
			{
				IsBusy = true;

				var val = ((FormEntryField)definition.Elements[1]).Text;

				string formvalues = "";
				foreach (var el in definition.Elements)
				{
					switch (el.Type)
					{

						case "Entry":
							formvalues += $"{((FormEntryField)el).LabelText} {((FormEntryField)el).Text}" + "~";
							break;
						case "TextField":
							formvalues += $"{((FormTextField)el).LabelText} {((FormTextField)el).Text}" + "~";
							break;
						case "Picker":
							formvalues += $"{((Picker)el).LabelText} {((Picker)el).SelectedValue}" + "~";
							break;
						case "Switch":
							formvalues += $"{((FormSwitch)el).LabelText} {((FormSwitch)el).Value}" + "~";
							break;
						case "DatePicker":
							formvalues += $"{((DatePicker)el).LabelText} {((DatePicker)el).SelectedDate.ToString("yyyy-M-d dddd")} " + "~";
							break;
						default:
							break;
					}
				}

				var form = await App.azureService.AddForm(definition.Title, formvalues);

				if (form != null)
				{
					App.submittedForms.Add(form);
					App.mainTabPage.CurrentPage = App.mainTabPage.Children[2];
					App.DidSubmitNewForm = true;
					IsBusy = false;
				}
				else {
					IsBusy = false;
					await DisplayAlert("Oh No!", "Could not sync Form", "Cancel");
				}

			}
			catch (Exception ex)
			{
				Debug.WriteLine("OH NO!" + ex);
				IsBusy = false;
			}

		}


	}
}
