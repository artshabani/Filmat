using Filmat.Models;
using Microsoft.EntityFrameworkCore;

namespace Filmat.Data.Services

{
	public class ClientsService : IClientsService
	{
		private readonly AppDbContext _context;

		public ClientsService(AppDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Client client)
		{
			await _context.Clients.AddAsync(client);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var result = await _context.Clients.FirstOrDefaultAsync(n => n.Id == id);
			_context.Clients.Remove(result);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Client>> GetAllAsync()
		{
			var result = await _context.Clients.ToListAsync();
			return result;
		}

		public async Task<Client> GetByIdAsync(int id)
		{
			var result = await _context.Clients.FirstOrDefaultAsync(n => n.Id == id);
			return result;
		}

		public async Task<Client> UpdateAsync(int id, Client newClient)
		{
			_context.Update(newClient);
			await _context.SaveChangesAsync();
			return newClient;
		}
	}
}
