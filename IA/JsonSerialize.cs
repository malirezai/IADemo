using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IA.Interfaces;
using Xamarin.Forms;

namespace IA
{
	public class JsonSerialize
	{

		JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
		public string _json;

		public JsonSerialize()
		{

			//FormDefinition testDef = new FormDefinition();

			//testDef.Elements.Add(
			//	new FormLabel { LabelText = "This is a Sample Form" }
			//);
			//testDef.Elements.Add(
			//	new FormEntryField { LabelText = "Full Name:", PlaceHolderText = "First and last name" }
			//);
			//testDef.Elements.Add(
			//	new FormTextField { LabelText = "Job Description", PlaceHolderText = "Job Desc", NumLines = 4 }
			//);
			//testDef.Elements.Add(
			//	new Picker { LabelText = "Years of Experiencce", Values = new List<string> { "1", "2", "3", "4", "5" } }
			//);
			//testDef.Elements.Add(
			//	new FormSwitch { LabelText = "Are You Happy?", DefaultValue = true }
			//);
			//testDef.Elements.Add(
			//	new DatePicker { LabelText = "Date of Birth:" }
			//);


			//var json = JsonConvert.SerializeObject(testDef,settings);

			//_json = json;

			//var k = 10;

		}


		public  FormDefinition parseJson(string _path)
		{

			var _string = DependencyService.Get<IResourceFinder>().getFormString(_path) ;

			var obj = JsonConvert.DeserializeObject<FormDefinition>(_string,settings);

			return obj;

		}
	}
}
