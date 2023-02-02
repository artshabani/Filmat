using Filmat.Models;

namespace Filmat.Data.Services
{
	public interface IMoviesService
	{
		public IEnumerable<Movie> SearchMovies(string searchQuery);
		void LogAction(string action, string? User = null, string details = null, string item = null);
		Task<IEnumerable<Movie>> GetAllAsync();
		Task<Movie> GetByIdAsync(int id);
		Task AddAsync(Movie movie);
		Task<Movie> UpdateAsync(int id, Movie newMovie);
		Task DeleteAsync(int id);

        IEnumerable<Movie> GetMostViewedMovies();




        //Task<IEnumerable<Client>> GetAllAsync();
        //Task<Client> GetByIdAsync(int id);
        //Task AddAsync(Client client);
        //Task<Client> UpdateAsync(int id, Client newClient);
        //Task DeleteAsync(int id);

    }
}
