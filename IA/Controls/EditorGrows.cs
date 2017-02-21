using System;
using Xamarin.Forms;

namespace IA
{
	public class EditorGrows : Editor
	{
		public EditorGrows()
		{
			this.TextChanged += OnTextChanged_Grow;
		}

		private void OnTextChanged_Grow(Object sender, TextChangedEventArgs e)
		{
			this.InvalidateMeasure();
		}
	}
}
