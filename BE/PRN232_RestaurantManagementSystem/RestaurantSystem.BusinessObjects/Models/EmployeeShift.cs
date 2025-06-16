using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.BusinessObjects.Models
{
    public class EmployeeShift
    {
        public int EmployeeShiftID { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        [Required]
        public int ShiftID { get; set; }

        [Required]
        public DateTime ShiftDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Shift Shift { get; set; }
    }
}
