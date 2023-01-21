using Filmat.Areas.Identity.Data;
using Filmat.Models;

namespace Filmat.Data
{
	public class AppDbInitializer
	{
		public static void Seed(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

				context.Database.EnsureCreated();

				//Filmat

				if (!context.Movies.Any())
				{
					context.Movies.AddRange(new List<Movie>()
					{
						new Movie()
						{
							Name = "Wednesday",
							Description = "The most trending movie of all time",
							Duration = 60,
							MovieCategory = MovieCategory.horror, 
							ImageURL ="https://ntvb.tmsimg.com/assets/p23063500_b_h8_ad.jpg?w=960&h=540"
						}
					});
					context.SaveChanges();

				}
			}
		} 
	}
}
