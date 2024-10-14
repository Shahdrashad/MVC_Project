using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class LoginViewModel
	{
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Password Doesn't match")]
		public string ConfirmPassword { get; set; }

		public bool RememberMe { get; set; }

		
	}
}
