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
    public class PromotionCombo
    {
        [Key]
        public int PromotionComboID { get; set; }

        [Required]
        public int PromotionID { get; set; }

        [Required]
        [StringLength(100)]
        public string ComboName { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ComboPrice { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public int? UpdatedBy { get; set; }

        // Navigation properties
        [ForeignKey("PromotionID")]
        public Promotion Promotion { get; set; }

        [ForeignKey("UpdatedBy")]
        public Staff? UpdatedByStaff { get; set; }
        public List<ComboDetail> ComboDetails { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
