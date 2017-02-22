using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITEST
{
	[TestFixture(Platform.Android)]
	[TestFixture(Platform.iOS)]
	public class Tests
	{
		IApp app;
		Platform platform;

		public Tests(Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest()
		{
			app = AppInitializer.StartApp(platform);
		}

		[Test]
		public void ReplTest()
		{
			app.Repl();
		}

		[Test]
		public void SubmitStaticFormTest()
		{
			app.Screenshot("First screen.");

			if (platform == Platform.iOS)
			{
				app.Invoke("enableTestCloudUser:","");
			}
			if (platform == Platform.Android)
			{
				app.Invoke("enableTestCloudUser");
			}

			app.Screenshot("Tap Login Button");

			app.Tap("loginButton");

			app.Tap("Static");


			app.Screenshot("Entering 'test cloud' into Type of Service field");
			app.EnterText(x => x.Marked("typeOfServiceField"), "test cloud");
			app.DismissKeyboard();

			app.Screenshot("Entering 'test cloud name' into Name Field");
			app.EnterText(x => x.Marked("nameField"), "test cloud name");
			app.DismissKeyboard();

			app.Screenshot("Entering '$100' into Submitted Fees field");
			app.EnterText(x => x.Marked("feesSubmittedField"), "$100");
			app.DismissKeyboard();

			app.Screenshot("Adding Photo");
			app.Tap("takePhotoButton");

			app.Tap("PhotoCapture");


			app.Tap("Use Photo");


			app.Screenshot("Submitting Form");
			app.Tap("Submit");

			app.WaitForElement(x => x.Marked("Submitted Forms"),"Time out waiting for form to submit",TimeSpan.FromSeconds(30));

			app.Screenshot("On Submitted forms page");

		}
	}
}
