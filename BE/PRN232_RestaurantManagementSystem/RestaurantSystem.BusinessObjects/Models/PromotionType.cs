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
    public class PromotionType
    {
        [Key]
        public int PromotionTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeName { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public int? UpdatedBy { get; set; }

        // Navigation properties
        [ForeignKey("UpdatedBy")]
        public Staff? UpdatedByStaff { get; set; }
        public List<Promotion> Promotions { get; set; }
    }
}
