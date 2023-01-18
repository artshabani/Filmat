using Filmat.Models;
using Microsoft.EntityFrameworkCore;

namespace Filmat.Data.Services
{
	public class MoviesService : IMoviesService
	{
		private readonly AppDbContext _context;

		public MoviesService(AppDbContext context)
		{
			_context = context;
		}
		public void Add(Movie movie)
		{
			throw new NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Movie>> GetAll()
		{
			var result = await _context.Movies.ToListAsync();
			return result;
		}

		public Movie GetById(int id)
		{
			throw new NotImplementedException();
		}

		public Movie Update(int id, Movie newMovie)
		{
			throw new NotImplementedException();
		}
	}
}
