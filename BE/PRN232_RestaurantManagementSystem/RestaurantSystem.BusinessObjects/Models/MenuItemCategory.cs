using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.BusinessObjects.Models
{
    public class MenuItemCategory
    {
        [Key, Column(Order = 0)]
        public int ItemID { get; set; }

        [Key, Column(Order = 1)]
        public int CategoryID { get; set; }

        public virtual MenuItem MenuItem { get; set; }
        public virtual Category Category { get; set; }
    }
}
