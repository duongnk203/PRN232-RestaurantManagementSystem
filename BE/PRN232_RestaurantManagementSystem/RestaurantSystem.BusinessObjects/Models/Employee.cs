using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.BusinessObjects.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Position { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        public virtual ICollection<EmployeeShift> EmployeeShifts { get; set; } = new List<EmployeeShift>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

}
