using System.ComponentModel.DataAnnotations.Schema;

namespace Filmat.Models
{
	public class Comment
	{

		public int CommentId { get; set; }

		public string CommentText { get; set; }

		public DateTime? CreatedOn { get; set; }

		
		public ApplicationUser User { get; set; }

	}
}
