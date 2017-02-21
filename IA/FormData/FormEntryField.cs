using System;
namespace IA
{
	public class FormEntryField: FormElement
	{
		public string PlaceHolderText;
		public string Text;

		public FormEntryField()
		{
			PlaceHolderText = "";
			Text = "";
		}
	}
}
