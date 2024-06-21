using System;
using Microsoft.AspNetCore.Identity;

namespace FruitablesProject.Models
{
	public class AppUser : IdentityUser
	{
		public string FullName { get; set; }
	}
}

