using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.BusinessObjects.Models
{
    public class Table
    {
        [Key]
        public int TableID { get; set; }

        [Required]
        public int ZoneID { get; set; }

        [Required]
        [StringLength(50)]
        public string TableNumber { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        [StringLength(50)]
        public string QRCode { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Available";

        [Required]
        public bool IsActive { get; set; } = true;

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public int? UpdatedBy { get; set; }

        // Navigation properties
        [ForeignKey("ZoneID")]
        public Zone Zone { get; set; }

        [ForeignKey("UpdatedBy")]
        public Staff? UpdatedByStaff { get; set; }
        public List<Order> Orders { get; set; }
        public List<QRSession> QRSessions { get; set; }
    }
}
