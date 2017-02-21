namespace IA
{
	public class FormElement
	{
		public string Type;
		public string LabelText;
		public bool Visibile;
		public bool NumKeyboard;

		public FormElement()
		{
			Type = "";
			LabelText = "";
			Visibile = true;
			NumKeyboard = false;
		}
	}
}