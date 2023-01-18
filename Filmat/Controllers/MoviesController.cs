using Filmat.Data;
using Filmat.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Filmat.Controllers
{
	public class MoviesController : Controller
	{
		private readonly IMoviesService _service;


		public MoviesController(IMoviesService service)
		{
			_service = service;	
		}

		public async Task<IActionResult> Index()
		{
			var data = await _service.GetAll();
			return View(data);
		}
	}
	
			
			
	}


