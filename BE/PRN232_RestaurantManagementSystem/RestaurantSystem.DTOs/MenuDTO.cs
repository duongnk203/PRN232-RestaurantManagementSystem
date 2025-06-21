using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.DTOs
{
    public class MenuDto
    {
        public int MenuItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public decimal Cost { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; }

    }

    public class MenuQueryDTO
    {
        public string? SearchItem { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsAvailable { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }

    public class MenuDTO
    {
        public List<MenuDto> Menus { get; set; } = new List<MenuDto>();
        public MenuQueryDTO Query { get; set; } = new MenuQueryDTO(); // Thông tin truy vấn
    }

    public class CreateMenu
    {
        [Required]
        [StringLength(100, ErrorMessage = "Tên món ăn không được vượt quá 100 ký tự")]
        public string ItemName { get; set; }
        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
        public string Description { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Danh mục là bắt buộc")]
        public int CategoryId { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Chi phí phải lớn hơn 0")]
        public decimal Cost { get; set; }
        [Url(ErrorMessage = "Đường dẫn hình ảnh không hợp lệ")]
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; } = true;
    }

    public class UpdateMenu : CreateMenu
    {
        [Required(ErrorMessage = "ID món ăn là bắt buộc")]
        public int MenuItemId { get; set; } // ID của món ăn để cập nhật
    }

    public class MenuItemModel
    {
        public int MenuItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; }
    }
}