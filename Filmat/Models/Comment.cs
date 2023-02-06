using System.ComponentModel.DataAnnotations.Schema;
using Filmat.Models;



namespace Filmat.Models
{
	public class Comment
	{
		
		
		public int Id { get; set; }

		public string Text { get; set; }

		public string UserID { get; set; }

		public DateTime TimeStamp { get; set; }

	}
}
