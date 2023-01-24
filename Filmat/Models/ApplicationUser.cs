using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Filmat.Models
{
	public class ApplicationUser : IdentityUser
	{
		
		public string FullName { get; set; } // username

		public string Emri { get; set; }
		
		
		
	}
}
