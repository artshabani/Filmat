using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Filmat.Models;
using Filmat.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace Filmat.Controllers
{
	public class CommentsController : Controller
	{

		private readonly AppDbContext _context;

		public CommentsController(AppDbContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Index()
		{
			var comments = await _context.Comments.ToListAsync();

			return View(comments);	
		}


		[HttpPost]
		public IActionResult LeaveComment(string text)
		{
			var comment = new Comment
			{

				Text = text,

				UserID = User.Identity.Name,

				TimeStamp = DateTime.Now



			};

			_context.Comments.Add(comment);
			_context.SaveChanges();

			return RedirectToAction("Index", "Comments");
		

	}
	}
}
