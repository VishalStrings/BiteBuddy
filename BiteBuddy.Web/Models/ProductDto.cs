namespace BiteBuddy.Web.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal Name { get; set; }
        public decimal? Price { get; set; }
        public string? Category { get; set; } = null;
        public bool IsActive { get; set; } = true;
    }
}
 