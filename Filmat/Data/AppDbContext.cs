using Filmat.Models;
using Microsoft.EntityFrameworkCore;

namespace Filmat.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		public DbSet<Movie> Movies { get; set; }
		public DbSet<Client> Clients { get; set; }
		
	}
}
