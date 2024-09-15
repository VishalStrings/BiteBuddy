using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiteBuddy.Services.ShoppingCartAPI.Models.Dto
{
    public class CouponDto
    {
        public int Id { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal MinimumAmount { get; set; }
        public string? Description { get; set; } = null;
        public DateTime? ExpirationDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
 