using System;
namespace IA
{
	public class DatePicker:FormElement
	{
		public DateTime SelectedDate;

		public DatePicker()
		{
			SelectedDate = DateTime.Now;
		}
	}
}
