using Filmat.Models;

namespace Filmat.Data.Services
{
	public interface IMoviesService
	{
		Task<IEnumerable<Movie>> GetAll();
		Movie GetById(int id);	
		void Add(Movie movie);
		Movie Update(int id, Movie newMovie);
		void Delete(int id);


	}
}
