using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.BusinessObjects.Models
{
    public class QRSession
    {
        [Key]
        public int QRSessionID { get; set; }

        [Required]
        public int TableID { get; set; }

        public int? CustomerID { get; set; }

        [Required]
        public DateTime SessionStart { get; set; } = DateTime.Now;

        public DateTime? SessionEnd { get; set; }

        [Required]
        [StringLength(50)]
        public string SessionToken { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        // Navigation properties
        [ForeignKey("TableID")]
        public Table Table { get; set; }

        [ForeignKey("CustomerID")]
        public Customer? Customer { get; set; }
    }
}
