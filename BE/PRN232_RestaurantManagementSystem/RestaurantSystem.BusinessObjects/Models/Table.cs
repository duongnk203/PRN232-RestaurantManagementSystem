using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.BusinessObjects.Models
{
    public class Table
    {
        public int TableID { get; set; }

        [Required]
        [StringLength(10)]
        public string TableNumber { get; set; }

        [Required]
        public int Capacity { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Available";

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
