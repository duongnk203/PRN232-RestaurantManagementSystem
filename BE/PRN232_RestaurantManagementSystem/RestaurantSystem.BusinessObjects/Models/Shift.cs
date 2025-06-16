using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.BusinessObjects.Models
{
    public class Shift
    {
        public int ShiftID { get; set; }

        [Required]
        [StringLength(50)]
        public string ShiftName { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public virtual ICollection<EmployeeShift> EmployeeShifts { get; set; } = new List<EmployeeShift>();
    }
}
