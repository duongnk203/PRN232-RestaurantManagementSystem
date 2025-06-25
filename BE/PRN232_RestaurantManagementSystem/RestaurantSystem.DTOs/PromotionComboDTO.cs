using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.DTOs
{
    public class PromotionComboBase
    {
        public int PromotionComboId { get; set; }
        public string PromotionComboName { get; set; }
        public int PromotionId { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class PromotionComboDto : PromotionComboBase
    {
        public string PromotionName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class PromotionComboQueryDTO
    {
        public string? SearchName { get; set; }
        public bool? IsActive { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }
    public class PromotionComboDTO
    {
        public List<PromotionComboDto> PromotionCombos { get; set; } = new List<PromotionComboDto>();
        public PromotionComboQueryDTO Query { get; set; } = new PromotionComboQueryDTO(); // Thông tin truy vấn
    }

}
