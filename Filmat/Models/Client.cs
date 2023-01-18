using Filmat.Data;
using System.ComponentModel.DataAnnotations;

namespace Filmat.Models
{
	public class Client
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Name cannot be blank!")]
		[StringLength(30,MinimumLength = 3,ErrorMessage = "Name must containt 3 to 50 characters!")]
		public string ?Name { get; set; }
		[Required(ErrorMessage = "Age cannot be blank!")]
		public int ?Age { get; set; }
		[Required(ErrorMessage = "Username cannot be blank!")]
		public string ?Username { get; set; }
		[Required(ErrorMessage = "Password cannot be blank!")]
		public string ?Password { get; set; }
	}

	


}