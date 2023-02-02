using Filmat.Areas.Identity.Data;
using Filmat.Data;
using Filmat.Data.Services;
using Filmat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Filmat.Controllers
{




	[Authorize]
	public class MoviesController : Controller
	{
		private readonly IMoviesService _service;
		private readonly SignInManager<ApplicationUser> signInManager;

		public MoviesController(IMoviesService service, SignInManager<ApplicationUser> signInManager)
		{
			_service = service;
			this.signInManager = signInManager;
		}



		public async Task<IActionResult> Index(int pg = 1)
		{
			var data = await _service.GetAllAsync();

			const int pageSize = 5;

			if (pg < 1)
			{
				pg = 1;
			}

			int recsCount = data.Count(); // sa rekorde jon

			var pager = new Pager(recsCount, pg, pageSize);

			int recSkip = (pg - 1) * pageSize;

			var data2 = data.Skip(recSkip).Take(pager.PageSize).ToList(); // sa recorde kan mu bo skip (per me display recorded n'faqen perkatese)

			this.ViewBag.Pager = pager;


			return View(data2);



		}
		[Authorize]
		//Get: Clients/Create
		public IActionResult Create()
		{
			return View();
		}
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Create(Movie movie)
		{
			if (!ModelState.IsValid)
			{
				return View(movie);
			}
			_service.LogAction("Create", User: User.Identity.Name, details: movie.Name, item: nameof(MoviesController));
			await _service.AddAsync(movie);





			return RedirectToAction(nameof(Index));
		}

		//Get:  Clients/Details/1

		public async Task<IActionResult> Details(int id)
		{
			var movieDetails = await _service.GetByIdAsync(id);

			if (movieDetails == null) return View("NotFound");
			return View(movieDetails);
		}


		//Get: Clients/Edit
		public async Task<IActionResult> Edit(int id)
		{
			var movieDetails = await _service.GetByIdAsync(id);
			if (movieDetails == null) return View("NotFound");
			return View(movieDetails);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, [Bind("Name,Description,MovieCategory,ImageURL,VideoURL")] Movie movie)
		{
			if (!ModelState.IsValid)
			{
				return View(movie);
			}
			await _service.UpdateAsync(id, movie);
			_service.LogAction("Update", User: User.Identity.Name, details: movie.Name, item: nameof(MoviesController));
			return RedirectToAction(nameof(Index));
		}


		public async Task<IActionResult> Delete(int id)
		{
			var movieDetails = await _service.GetByIdAsync(id);
			if (movieDetails == null) return View("NotFound");
			return View(movieDetails);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var movieDetails = await _service.GetByIdAsync(id);
			if (movieDetails == null) return View("NotFound");

			_service.LogAction("Delete", User: User.Identity.Name, details: movieDetails.Name, item: nameof(MoviesController));
			await _service.DeleteAsync(id);


			return RedirectToAction(nameof(Index));
		}




		public async Task<IActionResult> PlayMovie(int id)
		{
			var movie = await _service.GetByIdAsync(id);



			if (movie == null)
			{
				ViewBag.ErrorMessage = $"Movie with Id = {id} cannot be found";
				return View("NotFound");
			}
			else
			{

				movie.ViewCount++;
				await _service.UpdateAsync(id, movie);
				return View(movie);

			}

		}

		[HttpGet]
		public async Task<ActionResult> MoviesByCategory(string category)
		{
			var movies = await _service.GetAllAsync();
			var mc = movies.Where(m => m.MovieCategory.ToString().Equals(category));
			return View(mc);
		}


		[HttpPost]
		public IActionResult Search(string searchQuery)
		{

			var movies = _service.SearchMovies(searchQuery);
			return View(movies);

		}



	}
	
			
			
}


