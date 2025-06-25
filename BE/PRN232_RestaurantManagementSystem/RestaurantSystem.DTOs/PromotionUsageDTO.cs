using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.DTOs
{
    public class PromotionUsageDto
    {
        public int PromotionUsageID { get; set; }
        public string PromotionName { get; set; }
        public int PromotionTypeID { get; set; }
        public string OrderNumber { get; set; } 
        public string CustomerName { get; set; }
        public DateTime UsageDate { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class PromotionUsageQueryDTO
    {
        public string? OrderNumber { get; set; }
        public int? PromotionTypeID { get; set; }
        public string? SearchCustomer { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }
    public class PromotionUsageDTO
    {
        public List<PromotionUsageDto> PromotionUsages { get; set; } = new List<PromotionUsageDto>();
        public PromotionUsageQueryDTO Query { get; set; } = new PromotionUsageQueryDTO(); // Thông tin truy vấn
    }
}
