using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage ="First Name is Required")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Last Name is Required")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "User Name is Required")]
		public string UserName { get; set; }

		[DataType(DataType.Password)]
		public string Password { get; set; }
		[DataType(DataType.Password)]
		[Compare(nameof(Password) ,ErrorMessage ="Password Doesn't match")]
		public string ConfirmPassword { get; set; }
		[EmailAddress(ErrorMessage ="Invalid Email")]
		public string Email { get; set; }
		public bool IsAgree { get; set; }


	}
}
