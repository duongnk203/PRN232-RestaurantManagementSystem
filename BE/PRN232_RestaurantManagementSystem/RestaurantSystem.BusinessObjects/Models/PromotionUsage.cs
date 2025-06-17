using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.BusinessObjects.Models
{
    public class PromotionUsage
    {
        [Key]
        public int PromotionUsageID { get; set; }

        [Required]
        public int PromotionID { get; set; }

        [Required]
        public int OrderID { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal DiscountApplied { get; set; }

        public DateTime? UsedAt { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("PromotionID")]
        public Promotion Promotion { get; set; }

        [ForeignKey("OrderID")]
        public Order Order { get; set; }
    }
}
