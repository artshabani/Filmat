using Filmat.Areas.Identity.Data;
using Filmat.Models;
using Filmat.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Filmat.Controllers
{
	
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public AccountController(UserManager<ApplicationUser> userManager,
								 SignInManager<ApplicationUser> signInManager) 
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}
		
		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("index", "home");
		}
		
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[AcceptVerbs("Get","Post")]
		public async Task<IActionResult> IsEmailInUse(string email)
		{
			var user = await userManager.FindByEmailAsync(email);

			if (user == null)
			{
				return Json(true);
			}
			else
			{
				return Json($"Email {email} is already in use");
			}

		}

			[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser {

					UserName = model.FullName,
					FullName = model.FullName,  // username
					Email = model.Email,			
					Emri = model.Emri //fullName
					
					

				};
				var result = await userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
					{
						return RedirectToAction("ListUsers", "Administration");
						
					}
						await signInManager.SignInAsync(user, isPersistent: false);
						return RedirectToAction("Index", "Movies");
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}
			return View(model);
		}
		
		//////////////////////////////////////////////////////////////
		
		
		public IActionResult Login()
		{
			return View();
		}


		[HttpPost]
		
		public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = default)
		{	
			if (ModelState.IsValid)
			{
				var result = await signInManager.PasswordSignInAsync(model.FullName, model.Password,
																	 model.RememberMe, false);

				//var result2 = await signInManager.PasswordSignInAsync(model.Email, model.Password,
				//													 model.RememberMe, false);

				if (result.Succeeded /* && result2.Succeeded*/)
				{
					if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
					{
						return Redirect(returnUrl);

					}
					else
					{
						return RedirectToAction("Index", "Movies");
					}
						
					
				}

					ModelState.AddModelError("", "Invalid Login Attempt");
				
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
