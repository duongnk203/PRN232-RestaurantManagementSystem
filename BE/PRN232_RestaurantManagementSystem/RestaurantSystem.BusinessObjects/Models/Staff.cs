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
    public class Staff
    {
        [Key]
        public int StaffID { get; set; }

        [Required]
        public int RoleID { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(15)]
        public string? PhoneNumber { get; set; }

        [StringLength(255)]
        public string? Email { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public int? UpdatedBy { get; set; }

        // Navigation properties
        [ForeignKey("RoleID")]
        public Role Role { get; set; }

        [ForeignKey("UpdatedBy")]
        public Staff? UpdatedByStaff { get; set; }
        public List<Bill> Bills { get; set; }
        public List<Order> Orders { get; set; }
        public List<StaffSchedule> StaffSchedules { get; set; }
    }
}
