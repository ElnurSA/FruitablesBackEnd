using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FruitablesProject.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles ="SuperAdmin, Admin")]
	public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

