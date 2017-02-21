using System;
using Xamarin.Forms;
namespace IA
{
	public class MainTabPage : TabbedPage
	{
		public MainTabPage()
		{
			
			Children.Add(new FormsPage());
			Children.Add(new StaticFormPage());
			Children.Add(new FormsSubmittedListPageXaml());

		}
	}
}
