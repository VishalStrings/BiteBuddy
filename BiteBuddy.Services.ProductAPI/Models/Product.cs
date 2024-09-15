using System.ComponentModel.DataAnnotations;

namespace BiteBuddy.Services.ProductAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Code {get; set;}

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }

        [Required] // Ensures a category is selected
        public int ProductCategoryId { get; set; } // Foreign Key to ProductCategory

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        // Navigation property
        public ProductCategory? ProductCategory { get; set; } // Navigation to ProductCategory

    }
}
