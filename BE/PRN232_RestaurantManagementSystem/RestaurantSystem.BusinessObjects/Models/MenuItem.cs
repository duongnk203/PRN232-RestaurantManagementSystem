using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.BusinessObjects.Models
{
    public class MenuItem
    {
        public int ItemID { get; set; }

        [Required]
        [StringLength(100)]
        public string ItemName { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public bool IsAvailable { get; set; } = true;

        public virtual ICollection<MenuItemCategory> MenuItemCategories { get; set; } = new List<MenuItemCategory>();
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }

}
