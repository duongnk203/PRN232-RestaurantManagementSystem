using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.DTOs
{
    public class StaffDto
    {
        public int StaffId { get; set; }
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; } = true;
        public int RoleId { get; set; }  // Default role
    }

    public class StaffDTO
    {
        public List<StaffDto> Staffs { get; set; } = new List<StaffDto>();
        public StaffQueryDTO Query { get; set; } = new StaffQueryDTO(); // Thông tin truy vấn

    }


    public class StaffQueryDTO
    {
        public int Page { get; set; } = 1; // Mặc định trang 1
        public int Limit { get; set; } = 10; // Mặc định 10 bản ghi/trang
        public string? FullName { get; set; } // Lọc theo tên
        public bool? IsActive { get; set; } // Lọc theo trạng thái
        public int? RoleId { get; set; } // Lọc theo vai trò
    }

    public class CreateStaff
    {
        [Required(ErrorMessage = "Họ và tên là bắt buộc")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vai trò là bắt buộc")]
        public int RoleId { get; set; } // Ví dụ: "Admin", "Staff", v.v.
        public string? Password { get; set; }
        public bool IsActive { get; set; } = true; // Mặc định là hoạt động
    }

    public class UpdateStaff : CreateStaff
    {
        [Required(ErrorMessage = "ID nhân viên là bắt buộc")]
        public int StaffId { get; set; } // ID của nhân viên để cập nhật
    }
}
