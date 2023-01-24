using System.ComponentModel.DataAnnotations;

namespace Filmat.ViewModels
{
	public class LoginViewModel
	{
		
		//[EmailAddress]
		//public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Remember me")]
		public bool RememberMe { get; set; }
		

		[Required]
		[Display(Name = "Username")]
		public string FullName { get; set; } 


	}
}
