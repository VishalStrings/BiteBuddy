using BiteBuddy.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace BiteBuddy.Web.Models
{
    public class ProductDto
    {
		public int ProductId { get; set; }

		public string Code { get; set; }

		[Required]
		public string Name { get; set; }

		public string? Description { get; set; }

		public decimal? Price { get; set; }

		public string? ImageUrl { get; set; }
		public string? ImageLocalPath { get; set; }

		[Required] // Ensures a category is selected
		public int ProductCategoryId { get; set; } // Foreign Key to ProductCategory
		// Navigation property
		public ProductCategory? ProductCategory { get; set; } // Navigation to ProductCategory

		[Range(1,100)]
		public int Count { get; set; } = 1;

	}
}
 