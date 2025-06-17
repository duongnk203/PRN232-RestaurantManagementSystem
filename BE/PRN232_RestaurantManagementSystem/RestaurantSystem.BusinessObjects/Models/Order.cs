using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderNumber { get; set; }

        [Required]
        public int TableID { get; set; }

        public int? CustomerID { get; set; }

        [StringLength(100)]
        public string? CustomerName { get; set; }

        [StringLength(15)]
        public string? CustomerPhone { get; set; }

        public int? StaffID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Pending";

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal TotalAmount { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal DiscountAmount { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal FinalAmount { get; set; } = 0;

        [StringLength(30)]
        public string? PaymentMethod { get; set; }

        [Required]
        [StringLength(20)]
        public string PaymentStatus { get; set; } = "Unpaid";

        [StringLength(500)]
        public string? Notes { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public int? UpdatedBy { get; set; }

        // Navigation properties
        [ForeignKey("TableID")]
        public Table Table { get; set; }

        [ForeignKey("CustomerID")]
        public Customer? Customer { get; set; }

        [ForeignKey("StaffID")]
        public Staff? Staff { get; set; }

        [ForeignKey("UpdatedBy")]
        public Staff? UpdatedByStaff { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
        public List<Bill> Bills { get; set; }
        public List<Review> Reviews { get; set; }
        public List<PromotionUsage> PromotionUsages { get; set; }
    }
}
