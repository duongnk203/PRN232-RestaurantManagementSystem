using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.BusinessObjects.Models
{
    public class StaffSchedule
    {
        [Key]
        public int ScheduleID { get; set; }

        [Required]
        public int StaffID { get; set; }

        [Required]
        public int WorkShiftID { get; set; }

        [Required]
        public DateTime ScheduleDate { get; set; }

        [StringLength(255)]
        public string? Notes { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public int? UpdatedBy { get; set; }

        // Navigation properties
        [ForeignKey("StaffID")]
        public Staff Staff { get; set; }

        [ForeignKey("WorkShiftID")]
        public WorkShift WorkShift { get; set; }

        [ForeignKey("UpdatedBy")]
        public Staff? UpdatedByStaff { get; set; }
    }
}
