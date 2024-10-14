using System.Collections;
using System.Collections.Generic;

namespace Demo.PL.ViewModels
{
	public class UserViewModel
	{
		public string Id { get; set; }
		public string FName { get; set; }
		public string LName { get; set; }

		public string UserName { get; set; }
		public string Email { get; set; }

		public IEnumerable<string> Role { get; set; }
		

	}
}
