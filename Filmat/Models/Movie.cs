using Filmat.Data;
using System.ComponentModel.DataAnnotations;

namespace Filmat.Models
{
	public class Movie
	{
		
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public int Duration { get; set; }	

		public MovieCategory MovieCategory { get; set; }

		public string ImageURL { get; set; }

		public string VideoURL { get; set; }

		public int ViewCount { get; set; }


		


	}
}
