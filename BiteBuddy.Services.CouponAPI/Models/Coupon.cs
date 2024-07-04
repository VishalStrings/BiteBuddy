using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiteBuddy.Services.CouponAPI.Models
{
    public class Coupon
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        [MaxLength(50)]
        [Required]
        public string CouponCode { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal MinimumAmount { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; } = null;
        public DateTime CreationDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsActive { get; set; }

    }
}
 