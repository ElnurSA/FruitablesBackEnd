using System;
using FruitablesProject.Helpers.Enums;
using FruitablesProject.Models;
using FruitablesProject.Services.Interfaces;
using FruitablesProject.ViewModels.Account;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace FruitablesProject.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IEmailService _emailService;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IEmailService emailService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_emailService = emailService;
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

			string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

			
            string url = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token }, Request.Scheme, Request.Host.ToString());


            string html = await System.IO.File.ReadAllTextAsync("wwwroot/templates/register.html");


            html = html.Replace("{link}", url);

            html = html.Replace("{UserFullName}", user.FullName);

            string subject = "Confirm you email";

			_emailService.Send(user.Email, subject, html);


            await _userManager.AddToRoleAsync(user, nameof(Roles.Member));



			return RedirectToAction(nameof(VerifyEmail));
		}

		[HttpGet]
		public IActionResult VerifyEmail()
		{
			return View();
		}


		[HttpGet]
		public async Task<IActionResult> ConfirmEmail(string userId, string token)
		{
			var user = await _userManager.FindByIdAsync(userId);
			await _userManager.ConfirmEmailAsync(user, token);
			bool IsTrue = await _userManager.IsEmailConfirmedAsync(user);
			return RedirectToAction(nameof(SignIn));
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

		[HttpGet]
		public async Task<IActionResult> CreateRoles()
		{
			foreach (var role in Enum.GetValues(typeof(Roles)))
			{
				if(!await _roleManager.RoleExistsAsync(nameof(role)))
				{
					await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
				}
			}
			return Ok();
		}
	}
}

