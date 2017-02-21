using System;
namespace IA
{
	public class UserModel
	{
		public string firstName { get; set; }
		public string lastName { get; set; }
		public string AuthToken { get; set; }
		public DateTimeOffset authExpiry;
		public string userID { get; set; }

		public UserModel()
		{
			authExpiry = new DateTimeOffset();

		}
	}
}
