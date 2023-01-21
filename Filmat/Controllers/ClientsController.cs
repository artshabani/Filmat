using Filmat.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Filmat.Data.Services;
using Filmat.Models;
using Microsoft.AspNetCore.Authorization;

namespace Filmat.Controllers
{
	public class ClientsController : Controller
	{
		private readonly IClientsService _service;


		public ClientsController(IClientsService service)
		{
			_service = service;
		}

		public async Task<IActionResult> Index(int pg=1)
		{
			var data = await _service.GetAllAsync();

			const int pageSize = 5;

			if(pg < 1)
			{
				pg = 1;
			}

			int recsCount = data.Count(); // sa rekorde jon

			var pager = new Pager(recsCount, pg, pageSize);

			int recSkip = (pg -1) * pageSize;

			var data2 = data.Skip(recSkip).Take(pager.PageSize).ToList(); // sa recorde kan mu bo skip (per me display recorded n'faqen perkatese)

			this.ViewBag.Pager = pager;


			//return  View(data);

			return View(data2);

		}

		//Get: Clients/Create
		public IActionResult Create()
		{
			return View();
		}
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Create([Bind("Name,Age,Username,Password")] Client client)
		{
			if(!ModelState.IsValid)
			{
				return View(client);
			}
			await _service.AddAsync(client);
			return  RedirectToAction(nameof(Index));
		}

		//Get:  Clients/Details/1

		public async Task<IActionResult> Details(int id)
		{
			var clientDetails = await _service.GetByIdAsync(id);

			if (clientDetails == null) return View("NotFound");
			return View(clientDetails);
		}


		//Get: Clients/Edit
		public async Task<IActionResult> Edit(int id)
		{
			var clientDetails = await _service.GetByIdAsync(id);
			if (clientDetails == null) return View("NotFound");
			return View(clientDetails);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, Client client)
		{
			if (!ModelState.IsValid)
			{
				return View(client);
			}
			await _service.UpdateAsync(id, client);
			return RedirectToAction(nameof(Index));
		}


		//Get: Clients/Delete/1
		public async Task<IActionResult> Delete(int id)
		{
			var clientDetails = await _service.GetByIdAsync(id);
			if (clientDetails == null) return View("NotFound");
			return View(clientDetails);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id, Client client)
		{
			var clientDetails = await _service.GetByIdAsync(id);
			if (clientDetails == null) return View("NotFound");

			await _service.DeleteAsync(id);
			
			return RedirectToAction(nameof(Index));
		}






	}




}


