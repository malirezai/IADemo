using System;
namespace IA
{
	public class FormTextField: FormElement
	{
		public string PlaceHolderText;
		public int NumLines;
		public string Text;

		public FormTextField()
		{
			PlaceHolderText = "";
			NumLines = 1;
			Text = "";
		}

	}
}
