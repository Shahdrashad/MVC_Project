using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class ForgetPasswordViewModel
	{
		[EmailAddress(ErrorMessage = "Invalid Email")]
		[Required(ErrorMessage ="Email is Required")]
		public string Email { get; set; }
	}
}
