using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.DTOs
{
    public class PromotionDto
    {
        public int PromotionID { get; set; }
        public int PromotionTypeID { get; set; }
        public string PromotionName { get; set; }
        public string? Description { get; set; }    
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal? MaxDiscountAmount { get; set; }
        public int? BuyQuantity { get; set; }
        public int? GetQuantity { get; set; }
        public bool IsActive{ get; set; }

    }
}
