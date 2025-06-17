using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantSystem.BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryName", "CreatedAt", "Description", "IsActive", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "Khai vị", new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Món ăn khai vị và nhẹ", true, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, "Món chính", new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Món ăn chính", true, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, "Tráng miệng", new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Món tráng miệng ngọt", true, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 4, "Đồ uống", new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Đồ uống và giải khát", true, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "CreatedAt", "Email", "FullName", "LoyaltyPoints", "PhoneNumber", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "khachhang1@gmail.com", "Hoàng Văn Em", 50.00m, "0912345678", new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "khachhang2@gmail.com", "Nguyễn Thị Phượng", 20.00m, "0912345679", new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "PromotionTypes",
                columns: new[] { "PromotionTypeID", "CreatedAt", "Description", "TypeName", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Giảm giá theo phần trăm", "Phần trăm", new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Giảm giá bằng số tiền cố định", "Số tiền cố định", new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Mua X món tặng Y món miễn phí", "Mua X tặng Y", new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 4, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Giảm giá cho combo món ăn", "Combo", new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "CreatedAt", "Description", "RoleName", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Quản lý nhà hàng", "Quản lý", new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Nhân viên phục vụ", "Phục vụ", new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Đầu bếp nhà bếp", "Đầu bếp", new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 4, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Nhân viên thu ngân", "Thu ngân", new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "WorkShifts",
                columns: new[] { "WorkShiftID", "CreatedAt", "Description", "EndTime", "ShiftName", "StartTime", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Ca làm việc buổi sáng", new TimeSpan(0, 15, 0, 0, 0), "Ca Sáng", new TimeSpan(0, 7, 0, 0, 0), new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Ca làm việc buổi chiều", new TimeSpan(0, 23, 0, 0, 0), "Ca Chiều", new TimeSpan(0, 15, 0, 0, 0), new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "Zones",
                columns: new[] { "ZoneID", "CreatedAt", "Description", "IsActive", "UpdatedAt", "UpdatedBy", "ZoneName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Khu vực ăn uống chính tầng 1", true, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null, "Tầng 1" },
                    { 2, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Khu vực ăn uống riêng tư tầng 2", true, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null, "Tầng 2" },
                    { 3, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Phòng ăn VIP độc quyền", true, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null, "Phòng VIP" }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "MenuItemID", "CategoryID", "Cost", "CreatedAt", "Description", "ImageURL", "IsAvailable", "ItemName", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, 20000.00m, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Chả giò giòn với rau củ", "cha_gio.jpg", true, "Chả giò", 50000.00m, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 2, 60000.00m, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Gà nướng với thảo mộc", "ga_nuong.jpg", true, "Gà nướng", 150000.00m, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, 3, 30000.00m, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Bánh sô-cô-la đậm đà", "banh_socolate.jpg", true, "Bánh sô-cô-la", 80000.00m, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 4, 4, 5000.00m, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Trà đá giải khát", "tra_da.jpg", true, "Trà đá", 25000.00m, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "Promotions",
                columns: new[] { "PromotionID", "BuyQuantity", "CreatedAt", "Description", "DiscountValue", "EndDate", "GetQuantity", "IsActive", "MaxDiscountAmount", "PromotionName", "PromotionTypeID", "StartDate", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Giảm 10% trên tổng hóa đơn", 10.00m, new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 100000.00m, "Giảm 10%", 1, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 2, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Mua 2 món tráng miệng tặng 1 miễn phí", null, new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, true, null, "Mua 2 tặng 1", 3, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, null, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Combo đặc biệt cho gia đình", null, new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, "Combo Gia đình", 4, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "Staffs",
                columns: new[] { "StaffID", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "PhoneNumber", "RoleID", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "quanly@nhahang.com", "Nguyễn Văn An", true, "hashed_password_1", "0901234567", 1, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "phucvu1@nhahang.com", "Trần Thị Bình", true, "hashed_password_2", "0901234568", 2, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "daubep1@nhahang.com", "Lê Văn Cường", true, "hashed_password_3", "0901234569", 3, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 4, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "thungan1@nhahang.com", "Phạm Thị Dung", true, "hashed_password_4", "0901234570", 4, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "TableID", "Capacity", "CreatedAt", "IsActive", "QRCode", "Status", "TableNumber", "UpdatedAt", "UpdatedBy", "ZoneID" },
                values: new object[,]
                {
                    { 1, 4, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), true, "QR_B01", "Maintenance", "B01", new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 2, 4, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), true, "QR_B02", "Reserved", "B02", new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 3, 6, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), true, "QR_B03", "Occupied", "B03", new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { 4, 8, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), true, "QR_B04", "Available", "B04", new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 3 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderID", "CreatedAt", "CustomerID", "CustomerName", "CustomerPhone", "DiscountAmount", "FinalAmount", "Notes", "OrderDate", "OrderNumber", "PaymentMethod", "PaymentStatus", "StaffID", "Status", "TableID", "TotalAmount", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, "Hoàng Văn Em", "0912345678", 0.00m, 405000.00m, null, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "ORD001", "QR_Pay", "Partial", 2, "Cancelled", 1, 405000.00m, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 2, new DateTime(2025, 6, 17, 12, 30, 0, 0, DateTimeKind.Unspecified), 2, "Nguyễn Thị Phượng", "0912345679", 25000.00m, 230000.00m, "Ưu tiên nhanh", new DateTime(2025, 6, 17, 12, 30, 0, 0, DateTimeKind.Unspecified), "ORD002", "QR_Pay", "Partial", 2, "Served", 2, 255000.00m, new DateTime(2025, 6, 17, 12, 30, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 3, new DateTime(2025, 6, 17, 13, 0, 0, 0, DateTimeKind.Unspecified), null, "Khách lẻ", null, 0.00m, 280000.00m, null, new DateTime(2025, 6, 17, 13, 0, 0, 0, DateTimeKind.Unspecified), "ORD003", "QR_Pay", "Partial", 2, "Ready", 3, 280000.00m, new DateTime(2025, 6, 17, 13, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 4, new DateTime(2025, 6, 17, 13, 30, 0, 0, DateTimeKind.Unspecified), 1, "Hoàng Văn Em", "0912345678", 0.00m, 500000.00m, "Bàn VIP", new DateTime(2025, 6, 17, 13, 30, 0, 0, DateTimeKind.Unspecified), "ORD004", "Card", "Partial", 2, "Ready", 4, 500000.00m, new DateTime(2025, 6, 17, 13, 30, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 5, new DateTime(2025, 6, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), 2, "Nguyễn Thị Phượng", "0912345679", 0.00m, 368000.00m, null, new DateTime(2025, 6, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), "ORD005", "Card", "Partial", 2, "Ready", 1, 368000.00m, new DateTime(2025, 6, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), 4 }
                });

            migrationBuilder.InsertData(
                table: "PromotionCombos",
                columns: new[] { "PromotionComboID", "ComboName", "ComboPrice", "CreatedAt", "IsActive", "PromotionID", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, "Combo Gia đình A", 350000.00m, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), true, 3, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.InsertData(
                table: "PromotionItems",
                columns: new[] { "PromotionItemID", "CategoryID", "MenuItemID", "PromotionID" },
                values: new object[,]
                {
                    { 1, null, 3, 2 },
                    { 2, 2, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "QRSessions",
                columns: new[] { "QRSessionID", "CustomerID", "IsActive", "SessionEnd", "SessionStart", "SessionToken", "TableID" },
                values: new object[,]
                {
                    { 1, 1, true, null, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "SESSION_001", 1 },
                    { 2, 2, true, null, new DateTime(2025, 6, 17, 12, 30, 0, 0, DateTimeKind.Unspecified), "SESSION_002", 2 }
                });

            migrationBuilder.InsertData(
                table: "StaffSchedules",
                columns: new[] { "ScheduleID", "CreatedAt", "Notes", "ScheduleDate", "StaffID", "UpdatedAt", "UpdatedBy", "WorkShiftID" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Quản lý ca sáng", new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 2, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Phục vụ ca chiều", new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 2 }
                });

            migrationBuilder.InsertData(
                table: "Bills",
                columns: new[] { "BillID", "BillDate", "BillNumber", "ChangeAmount", "CreatedAt", "DiscountAmount", "OrderID", "PaidAmount", "PaymentMethod", "ServiceCharge", "StaffID", "SubTotal", "TaxAmount", "TotalAmount", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "BILL001", 0.00m, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, 1, 465750.00m, "QR_Pay", 20250.00m, 4, 405000.00m, 40500.00m, 465750.00m, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 2, new DateTime(2025, 6, 17, 12, 30, 0, 0, DateTimeKind.Unspecified), "BILL002", 0.00m, new DateTime(2025, 6, 17, 12, 30, 0, 0, DateTimeKind.Unspecified), 25000.00m, 2, 231250.00m, "QR_Pay", 11750.00m, 4, 235000.00m, 23500.00m, 231250.00m, new DateTime(2025, 6, 17, 12, 30, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 3, new DateTime(2025, 6, 17, 13, 0, 0, 0, DateTimeKind.Unspecified), "BILL003", 0.00m, new DateTime(2025, 6, 17, 13, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, 3, 294000.00m, "Transfer", 14000.00m, 4, 280000.00m, 28000.00m, 294000.00m, new DateTime(2025, 6, 17, 13, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 4, new DateTime(2025, 6, 17, 13, 30, 0, 0, DateTimeKind.Unspecified), "BILL004", 0.00m, new DateTime(2025, 6, 17, 13, 30, 0, 0, DateTimeKind.Unspecified), 0.00m, 4, 567500.00m, "QR_Pay", 25000.00m, 4, 500000.00m, 50000.00m, 567500.00m, new DateTime(2025, 6, 17, 13, 30, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 5, new DateTime(2025, 6, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), "BILL005", 0.00m, new DateTime(2025, 6, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, 5, 423200.00m, "Card", 18400.00m, 4, 368000.00m, 36800.00m, 423200.00m, new DateTime(2025, 6, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), 4 }
                });

            migrationBuilder.InsertData(
                table: "ComboDetails",
                columns: new[] { "ComboDetailID", "MenuItemID", "PromotionComboID", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 2 },
                    { 2, 2, 1, 1 },
                    { 3, 4, 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderDetailID", "CreatedAt", "DiscountAmount", "MenuItemID", "Notes", "OrderID", "PromotionComboID", "Quantity", "Status", "TotalPrice", "UnitPrice", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, 1, null, 1, null, 3, "Preparing", 150000.00m, 50000.00m, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, 2, "Preparing", 1, null, 1, "Preparing", 150000.00m, 150000.00m, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, 4, null, 1, null, 3, "Preparing", 75000.00m, 25000.00m, new DateTime(2025, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 4, new DateTime(2025, 6, 17, 12, 30, 0, 0, DateTimeKind.Unspecified), 0.00m, 1, null, 2, null, 2, "Preparing", 100000.00m, 50000.00m, new DateTime(2025, 6, 17, 12, 30, 0, 0, DateTimeKind.Unspecified), null },
                    { 5, new DateTime(2025, 6, 17, 12, 30, 0, 0, DateTimeKind.Unspecified), 25000.00m, 3, "Ưu tiên nhanh", 2, null, 2, "Preparing", 135000.00m, 80000.00m, new DateTime(2025, 6, 17, 12, 30, 0, 0, DateTimeKind.Unspecified), null },
                    { 6, new DateTime(2025, 6, 17, 13, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, 2, null, 3, null, 1, "Preparing", 150000.00m, 150000.00m, new DateTime(2025, 6, 17, 13, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 7, new DateTime(2025, 6, 17, 13, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, 4, null, 3, null, 2, "Ordered", 50000.00m, 25000.00m, new DateTime(2025, 6, 17, 13, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 8, new DateTime(2025, 6, 17, 13, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, null, null, 3, 1, 1, "Served", 350000.00m, 350000.00m, new DateTime(2025, 6, 17, 13, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 9, new DateTime(2025, 6, 17, 13, 30, 0, 0, DateTimeKind.Unspecified), 0.00m, 2, "Thêm rau", 4, null, 2, "Served", 300000.00m, 150000.00m, new DateTime(2025, 6, 17, 13, 30, 0, 0, DateTimeKind.Unspecified), null },
                    { 10, new DateTime(2025, 6, 17, 13, 30, 0, 0, DateTimeKind.Unspecified), 0.00m, 3, null, 4, null, 2, "Served", 160000.00m, 80000.00m, new DateTime(2025, 6, 17, 13, 30, 0, 0, DateTimeKind.Unspecified), null },
                    { 11, new DateTime(2025, 6, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, 1, null, 5, null, 2, "Served", 100000.00m, 50000.00m, new DateTime(2025, 6, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 12, new DateTime(2025, 6, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, 2, null, 5, null, 1, "Served", 150000.00m, 150000.00m, new DateTime(2025, 6, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 13, new DateTime(2025, 6, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, 3, null, 5, null, 1, "Ready", 80000.00m, 80000.00m, new DateTime(2025, 6, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "PromotionUsages",
                columns: new[] { "PromotionUsageID", "DiscountApplied", "OrderID", "PromotionID", "UsedAt" },
                values: new object[] { 1, 25000.00m, 2, 1, new DateTime(2025, 6, 17, 12, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewID", "Comment", "CreatedAt", "CustomerID", "OrderID", "Rating", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "Dịch vụ tuyệt vời, món ăn ngon!", new DateTime(2025, 6, 17, 12, 30, 0, 0, DateTimeKind.Unspecified), 1, 1, 5, new DateTime(2025, 6, 17, 12, 30, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, "Món ăn ngon nhưng phục vụ hơi chậm.", new DateTime(2025, 6, 17, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 4, new DateTime(2025, 6, 17, 13, 0, 0, 0, DateTimeKind.Unspecified), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bills",
                keyColumn: "BillID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bills",
                keyColumn: "BillID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bills",
                keyColumn: "BillID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Bills",
                keyColumn: "BillID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Bills",
                keyColumn: "BillID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ComboDetails",
                keyColumn: "ComboDetailID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ComboDetails",
                keyColumn: "ComboDetailID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ComboDetails",
                keyColumn: "ComboDetailID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "PromotionItems",
                keyColumn: "PromotionItemID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PromotionItems",
                keyColumn: "PromotionItemID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PromotionTypes",
                keyColumn: "PromotionTypeID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PromotionUsages",
                keyColumn: "PromotionUsageID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "QRSessions",
                keyColumn: "QRSessionID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "QRSessions",
                keyColumn: "QRSessionID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StaffSchedules",
                keyColumn: "ScheduleID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StaffSchedules",
                keyColumn: "ScheduleID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Staffs",
                keyColumn: "StaffID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PromotionCombos",
                keyColumn: "PromotionComboID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "PromotionID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "PromotionID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Staffs",
                keyColumn: "StaffID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkShifts",
                keyColumn: "WorkShiftID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkShifts",
                keyColumn: "WorkShiftID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PromotionTypes",
                keyColumn: "PromotionTypeID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PromotionTypes",
                keyColumn: "PromotionTypeID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "PromotionID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Staffs",
                keyColumn: "StaffID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Staffs",
                keyColumn: "StaffID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PromotionTypes",
                keyColumn: "PromotionTypeID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Zones",
                keyColumn: "ZoneID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Zones",
                keyColumn: "ZoneID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Zones",
                keyColumn: "ZoneID",
                keyValue: 3);
        }
    }
}
