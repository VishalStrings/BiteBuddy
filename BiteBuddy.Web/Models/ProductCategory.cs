using System.ComponentModel.DataAnnotations;

namespace BiteBuddy.Web.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }              // Primary Key
        public string Name { get; set; }          // Category name
        public string Description { get; set; }   // Category description

        // Navigation property
        public ICollection<ProductDto> Products { get; set; }  // Collection of Products in this category
    }

}
