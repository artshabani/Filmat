using System.ComponentModel.DataAnnotations;

namespace Filmat.ViewModels
{
	public class CreateRoleViewModel
	{	
		[Required]
		public string RoleName { get; set; }

	}
}
