using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.BusinessObjects.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public virtual ICollection<MenuItemCategory> MenuItemCategories { get; set; } = new List<MenuItemCategory>();
    }
}
