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
    public class ComboDetail
    {
        [Key]
        public int ComboDetailID { get; set; }

        [Required]
        public int PromotionComboID { get; set; }

        [Required]
        public int MenuItemID { get; set; }

        [Required]
        public int Quantity { get; set; }

        // Navigation properties
        [ForeignKey("PromotionComboID")]
        public PromotionCombo PromotionCombo { get; set; }

        [ForeignKey("MenuItemID")]
        public MenuItem MenuItem { get; set; }
    }
}
