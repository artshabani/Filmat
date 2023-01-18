using Filmat.Models;

namespace Filmat.Data.Services
{
	public interface IClientsService
	{
		Task<IEnumerable<Client>> GetAllAsync();
		Task<Client> GetByIdAsync(int id);
		Task AddAsync(Client client);
		Task<Client> UpdateAsync(int id, Client newClient);
		Task DeleteAsync(int id);


	}
}
  