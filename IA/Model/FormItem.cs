using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace IA
{
	public class FormItem 
	{
		string id;
		string name;
		bool done;

		[JsonProperty(PropertyName = "id")]
		public string Id
		{
			get { return id; }
			set { id = value; }
		}

		public string UserID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FormType { get; set; }
		public string FormData { get; set; }
		public string EnteredDateUTC { get; set;}
		public string Byte64StringImage { get; set; }

		[Version]
		public string Version { get; set; }


		// we're using these two for display purposes
		[JsonIgnore]
		public string DateDisplay { get { return DateTime.Parse(EnteredDateUTC).ToLocalTime().ToString("g"); } }

		[JsonIgnore]
		public string TimeDisplay { get { return DateTime.Parse(EnteredDateUTC).ToLocalTime().ToString("t"); } }

		[JsonIgnore]
		public string FullName { get { return FirstName + " " + LastName; } }

		[JsonIgnore]
		public string DetailDisplay { get { return FullName + " - submitted: " + DateDisplay; } }

		[JsonIgnore]
		public string FullNameDisplay
		{
			get
			{
				return "Type - " + FormType;//"Title: " + FormData.Split('~')[0].Split(':')[1].Trim();
			}
		}

	}

}
