using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.BusinessObjects.Models
{
    public class Promotion
    {
        [Key]
        public int PromotionID { get; set; }

        [Required]
        public int PromotionTypeID { get; set; }

        [Required]
        [StringLength(100)]
        public string PromotionName { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? DiscountValue { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? MaxDiscountAmount { get; set; }

        public int? BuyQuantity { get; set; }

        public int? GetQuantity { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public int? UpdatedBy { get; set; }

        // Navigation properties
        [ForeignKey("PromotionTypeID")]
        public PromotionType PromotionType { get; set; }

        [ForeignKey("UpdatedBy")]
        public Staff? UpdatedByStaff { get; set; }
        public List<PromotionCombo> PromotionCombos { get; set; }
        public List<PromotionItem> PromotionItems { get; set; }
        public List<PromotionUsage> PromotionUsages { get; set; }
    }
}
