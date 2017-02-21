using System;
using System.Collections.Generic;

namespace IA
{
	public class Picker:FormElement
	{
		public int DefaultIndex;
		public string SelectedValue;
		public List<string> Values;

		public Picker()
		{
			DefaultIndex = 0;
			SelectedValue = "";
			Values = new List<string>();
		}
	}
}
