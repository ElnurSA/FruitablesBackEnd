using System;
using System.ComponentModel.DataAnnotations;

namespace FruitablesProject.ViewModels.Account
{
	public class RegisterVM
	{
		[Required]
		public string FullName { get; set; }
		[Required]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; }
		[Required]
		public string Username { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
        [DataType(DataType.Password)]
		[Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
	}
}

