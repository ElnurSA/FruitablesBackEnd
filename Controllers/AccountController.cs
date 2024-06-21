using System;
using FruitablesProject.Models;
using FruitablesProject.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FruitablesProject.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		//SignUp

		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SignUp(RegisterVM request)
		{
			if (!ModelState.IsValid)
			{
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    // Log or debug the error messages
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(request);
			}

			AppUser user = new()
			{
				UserName = request.Username,
				Email = request.Email,
				FullName = request.FullName,
			};

			var result = await _userManager.CreateAsync(user, request.Password);

			if (!result.Succeeded)
			{
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError(string.Empty, item.Description);
					
				}
                return View(request);
            }

			await _signInManager.SignInAsync(user, false);


			return RedirectToAction("Index", "Home");
		}

		//SignIn
		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SignIn(LoginVM request)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var existUser = await _userManager.FindByEmailAsync(request.EmailOrUsername);

			if(existUser is null)
			{
				existUser = await _userManager.FindByNameAsync(request.EmailOrUsername);
			}

			if(existUser is null)
			{
				ModelState.AddModelError(string.Empty, "Login Failed");
				return View();
			}

			var result = await _signInManager.PasswordSignInAsync(existUser, request.Password, false, false);

			if (!result.Succeeded)
			{
				ModelState.AddModelError(string.Empty, "Login Failed");
				return View();
			}

            return RedirectToAction("Index", "Home");
        }

		//Logout
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
	}
}

