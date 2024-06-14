using System;
using System.ComponentModel.DataAnnotations;

namespace FruitablesProject.Models
{
	public class Category : BaseEntity
	{
		[Required(ErrorMessage = "This input cannot be empty"), StringLength(20)]
		public string Name { get; set; }
		public ICollection<Product> Products { get; set; }
	}
}

