using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.BusinessObjects.Models
{
    public class PromotionItem
    {
        [Key]
        public int PromotionItemID { get; set; }

        [Required]
        public int PromotionID { get; set; }

        public int? MenuItemID { get; set; }

        public int? CategoryID { get; set; }

        // Navigation properties
        [ForeignKey("PromotionID")]
        public Promotion Promotion { get; set; }

        [ForeignKey("MenuItemID")]
        public MenuItem? MenuItem { get; set; }

        [ForeignKey("CategoryID")]
        public Category? Category { get; set; }
    }
}
