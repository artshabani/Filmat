using Filmat.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Filmat.Controllers
{
	[Authorize(Roles = "Admin")]
	public class LogsatController : Controller
	{

		private readonly AppDbContext _context;

		public LogsatController(AppDbContext context)
		{
			_context =	context;
		}
		public async Task<IActionResult> Index()
		{
			var logs = await _context.Logs.ToListAsync();

			return View(logs);
		}
	}
}
