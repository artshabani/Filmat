﻿using Filmat.Areas.Identity.Data;
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

		public async Task AddAsync(Movie movie)
		{
			await _context.Movies.AddAsync(movie);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var result = await _context.Movies.FirstOrDefaultAsync(n => n.Id == id);
			_context.Movies.Remove(result);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Movie>> GetAllAsync()
		{
			var result = await _context.Movies.ToListAsync();
			return result;
		}

		public async Task<Movie> GetByIdAsync(int id)
		{
			var result = await _context.Movies.FirstOrDefaultAsync(n => n.Id == id);
			return result;
		}

		

		public async Task<Movie> UpdateAsync(int id, Movie newMovie)
		{
			_context.Update(newMovie);
			await _context.SaveChangesAsync();
			return newMovie;
		}
	}
}