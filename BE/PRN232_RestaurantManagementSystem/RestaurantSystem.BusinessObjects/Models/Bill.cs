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
    public class Bill
    {
        [Key]
        public int BillID { get; set; }

        [Required]
        public int OrderID { get; set; }

        [Required]
        [StringLength(50)]
        public string BillNumber { get; set; }

        [Required]
        public DateTime BillDate { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal SubTotal { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TaxAmount { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ServiceCharge { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal DiscountAmount { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal TotalAmount { get; set; }

        [StringLength(30)]
        public string? PaymentMethod { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal PaidAmount { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ChangeAmount { get; set; } = 0;

        [Required]
        public int StaffID { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public int? UpdatedBy { get; set; }

        // Navigation properties
        [ForeignKey("OrderID")]
        public Order Order { get; set; }

        [ForeignKey("StaffID")]
        public Staff Staff { get; set; }

        [ForeignKey("UpdatedBy")]
        public Staff? UpdatedByStaff { get; set; }
    }
}
