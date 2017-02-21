using System;
namespace IA
{
	public class FormSwitch: FormElement
	{
		public bool DefaultValue;
		public bool Value;
		public string Text;

		public FormSwitch()
		{
			DefaultValue = false;
			Value = true;
			Text = "";
		}
	}
}
