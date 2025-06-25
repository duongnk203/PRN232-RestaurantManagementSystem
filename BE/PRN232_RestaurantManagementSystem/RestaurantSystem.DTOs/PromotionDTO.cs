using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.DTOs
{
    public class PromotionDto
    {
        public int PromotionId { get; set; }
        public int PromotionTypeId { get; set; }
        public string PromotionName { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal? MaxDiscountAmount { get; set; }
        public int? BuyQuantity { get; set; }
        public int? GetQuantity { get; set; }
        public bool IsActive { get; set; }

    }
    public class CreatePromotion
    {
        [Required]
        public int PromotionTypeId { get; set; }
        [Required]
        public string PromotionName { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal? MaxDiscountAmount { get; set; }
        public int? BuyQuantity { get; set; }
        public int? GetQuantity { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class UpdatePromotion : CreatePromotion
    {
        [Required(ErrorMessage = "ID khuyến mãi là bắt buộc")]
        public int PromotionId { get; set; } // ID của khuyến mãi để cập nhật
    }
    public class PromotionQueryDTO
    {
        public string? SearchPromotion { get; set; }
        public int? PromotionTypeId { get; set; }
        public bool? IsActive { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }
    public class PromotionDTO
    {
        public List<PromotionDto> Promotions { get; set; } = new List<PromotionDto>();
        public PromotionQueryDTO Query { get; set; } = new PromotionQueryDTO(); // Thông tin truy vấn
    }
}
