using Filmat.Data.Services;
using Filmat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Filmat.Controllers
{
	public class HomeController : Controller
	{

        private readonly IMoviesService _service;

        public HomeController(IMoviesService service)
		{
			_service = service;
		}

		public async Task<IActionResult> Index()  //most viewed movie
		{
            var mostViewed = await _service.GetMostViewedMovie();
			return View(mostViewed);
        }
		
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}