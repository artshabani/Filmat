using Filmat.Models;
using Filmat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Filmat.Controllers
{
	[Authorize(Roles = "Admin")]		
	public class AdministrationController : Controller
	{
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly UserManager<ApplicationUser> userManager;

		public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
		{
			this.roleManager = roleManager;
			this.userManager = userManager;
		}

		public async Task<IActionResult> UserDetails(string id)
		{
			var user = await userManager.FindByIdAsync(id);


			if (user == null)
			{
				ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
				return View("NotFound");
			}
			else
			{
				return View(user);
			}
		}

		
		public async Task<IActionResult> DeleteUser(string id)
		{
			var user = await userManager.FindByIdAsync(id);

			if (user == null)
			{
				ViewBag.ErrorMessage = $"User with Id = {id} cannot be found;";
				return View("NotFound");
			}
			else
			{
				var result = await userManager.DeleteAsync(user);

				if (result.Succeeded)
				{
					return RedirectToAction("ListUsers");
				}

				foreach(var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}

				return View("ListUsers");
			}

		}

		public async Task<IActionResult> DeleteRole(string id)
		{
			var role = await roleManager.FindByIdAsync(id);

			if (role == null)
			{
				ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found;";
				return View("NotFound");
			}
			else
			{
				var result = await roleManager.DeleteAsync(role);

				if (result.Succeeded)
				{
					return RedirectToAction("ListRoles");
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}

				return View("ListUsers");
			}

		}




		public async Task<IActionResult> ListUsers(int pg = 1)
			{
				var data = userManager.Users;

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


				//return  View(data);

				return View(data2);

			}


			[HttpGet]
			public async Task<IActionResult> EditUser(string id)
			{
				var user = await userManager.FindByIdAsync(id);

				if (user == null)
				{
					ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
					return View("NotFound");
				}

				var userRoles = await userManager.GetRolesAsync(user);

				var model = new EditUserViewModel
				{
					Id = user.Id,
					Email = user.Email,
					FullName = user.FullName,
					Emri = user.Emri

				};

				return View(model);
			}

			[HttpPost]
			public async Task<IActionResult> EditUser(EditUserViewModel model)
			{
				var user = await userManager.FindByIdAsync(model.Id);

				if (user == null)
				{
					ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
					return View("NotFound");
				}
				else
				{
					user.Email = model.Email;
					user.FullName = model.FullName;
					user.Emri = model.Emri;

					var result = await userManager.UpdateAsync(user);

					if (result.Succeeded)
					{
						return RedirectToAction("ListUsers");
					}

					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
					return View(model);
				}



			}






			[HttpGet]
			public IActionResult CreateRole()
			{
				return View();
			}

			[HttpPost]
			public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
			{

				if (ModelState.IsValid)
				{
					IdentityRole identityRole = new IdentityRole
					{
						Name = model.RoleName
					};
					IdentityResult result = await roleManager.CreateAsync(identityRole);

					if (result.Succeeded)
					{
						return RedirectToAction("ListRoles", "Administration");
					}

					foreach (IdentityError error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}

				return View(model);
			}

			[HttpGet]
			public IActionResult ListRoles()
			{
				var roles = roleManager.Roles;
				return View(roles);
			}

			[HttpGet]
			public async Task<IActionResult> EditRole(string id)
			{
				var role = await roleManager.FindByIdAsync(id);

				if (role == null)
				{
					ViewBag.ErrorMessager = $"Role with Id = {id} cannot be found";
					return View("NotFound");
				}

				var model = new EditRoleViewModel
				{
					Id = role.Id,
					RoleName = role.Name
				};

				foreach (var user in userManager.Users)
				{
					if (await userManager.IsInRoleAsync(user, role.Name))
					{
						model.Users.Add(user.UserName);
					}
				}
				return View(model);
			}


			[HttpPost]
			public async Task<IActionResult> EditRole(EditRoleViewModel model)
			{
				var role = await roleManager.FindByIdAsync(model.Id);

				if (role == null)
				{
					ViewBag.ErrorMessager = $"Role with Id = {model.Id} cannot be found";
					return View("NotFound");
				}
				else
				{
					role.Name = model.RoleName;
					var result = await roleManager.UpdateAsync(role);

					if (result.Succeeded)
					{
						return RedirectToAction("ListRoles");
					}
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}


				return View(model);
			}


			[HttpGet]
			public async Task<IActionResult> EditUsersInRole(string roleId)
			{
				ViewBag.roleId = roleId;

				var role = await roleManager.FindByIdAsync(roleId);

				if (role == null)
				{
					ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
					return View("NotFound");
				}

				var model = new List<UserRoleViewModel>();

				foreach (var user in userManager.Users)
				{
					var userRoleViewModel = new UserRoleViewModel
					{
						UserId = user.Id,
						UserName = user.UserName
					};

					if (await userManager.IsInRoleAsync(user, role.Name))
					{
						userRoleViewModel.IsSelected = true;
					}
					else
					{
						userRoleViewModel.IsSelected = false;
					}

					model.Add(userRoleViewModel);
				}

				return View(model);
			}

			[HttpPost]
			public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
			{
				var role = await roleManager.FindByIdAsync(roleId);

				if (role == null)
				{
					ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
					return View("NotFound");
				}

				for (int i = 0; i < model.Count; i++)
				{
					var user = await userManager.FindByIdAsync(model[i].UserId);

					IdentityResult result = null;

					if (model[i].IsSelected & !(await userManager.IsInRoleAsync(user, role.Name)))
					{
						result = await userManager.AddToRoleAsync(user, role.Name);
					}
					else if (!model[i].IsSelected & await userManager.IsInRoleAsync(user, role.Name))
					{
						result = await userManager.RemoveFromRoleAsync(user, role.Name);
					}
					else
					{
						continue;
					}
					if (result.Succeeded)
					{


						if (i < (model.Count - 1)) {
							continue;
						}
						else {
							return RedirectToAction("EditRole", new { Id = roleId });
						}
					}
				}

				return RedirectToAction("EditRole", new { Id = roleId });

			}


		}
	}


