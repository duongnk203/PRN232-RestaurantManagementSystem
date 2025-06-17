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
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [StringLength(15)]
        public string? PhoneNumber { get; set; }

        [StringLength(100)]
        public string? FullName { get; set; }

        [StringLength(255)]
        public string? Email { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal LoyaltyPoints { get; set; } = 0;

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public int? UpdatedBy { get; set; }

        // Navigation properties
        [ForeignKey("UpdatedBy")]
        public Staff? UpdatedByStaff { get; set; }
        public List<Order> Orders { get; set; }
        public List<QRSession> QRSessions { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
