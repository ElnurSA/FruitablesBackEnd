using System;
using Microsoft.AspNetCore.Mvc;

namespace FruitablesProject.Controllers
{
	public class ContactController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

