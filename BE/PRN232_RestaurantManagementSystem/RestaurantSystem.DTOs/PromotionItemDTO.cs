using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.DTOs
{
    public class PromotionItemDto
    {
        public int PromotionItemID { get; set; }
        public int PromotionID { get; set; }
        public string ItemName { get; set; }
        public string CategoryName { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
