using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.BusinessObjects.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        public int? TableID { get; set; }

        public int? CustomerID { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; } = 0;

        [Required]
        [StringLength(50)]
        public string OrderType { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentStatus { get; set; } = "Pending";

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        public virtual Table? Table { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
