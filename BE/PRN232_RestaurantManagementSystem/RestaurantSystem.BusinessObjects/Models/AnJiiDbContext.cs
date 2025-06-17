using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.BusinessObjects.Models
{
    public class AnJiiDbContext : DbContext
    {
        public AnJiiDbContext()
        {

        }

        public AnJiiDbContext(DbContextOptions<AnJiiDbContext> options) : base(options)
        {
        }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ComboDetail> ComboDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<PromotionCombo> PromotionCombos { get; set; }
        public DbSet<PromotionItem> PromotionItems { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionType> PromotionTypes { get; set; }
        public DbSet<PromotionUsage> PromotionUsages { get; set; }
        public DbSet<QRSession> QRSessions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<StaffSchedule> StaffSchedules { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<WorkShift> WorkShifts { get; set; }
        public DbSet<Zone> Zones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Bills
            modelBuilder.Entity<Bill>()
                .HasKey(b => b.BillID);
            modelBuilder.Entity<Bill>()
                .HasIndex(b => b.OrderID)
                .IsUnique();
            modelBuilder.Entity<Bill>()
                .HasIndex(b => b.BillNumber)
                .IsUnique();
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Order)
                .WithMany(o => o.Bills)
                .HasForeignKey(b => b.OrderID);
            modelBuilder.Entity<Bill>()
                .Property(b => b.SubTotal)
                .HasColumnType("decimal(12,2)");
            modelBuilder.Entity<Bill>()
                .HasCheckConstraint("CK_Bills_TotalAmount", "[TotalAmount] >= 0");

            // Add Bill-Staff relationship configuration
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Staff)
                .WithMany(s => s.Bills)
                .HasForeignKey(b => b.StaffID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bill>()
                .HasOne(b => b.UpdatedByStaff)
                .WithMany()
                .HasForeignKey(b => b.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Orders
            modelBuilder.Entity<Order>()
                .HasKey(o => o.OrderID);
            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderNumber)
                .IsUnique();
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Table)
                .WithMany(t => t.Orders)
                .HasForeignKey(o => o.TableID);
            modelBuilder.Entity<Order>()
                .HasCheckConstraint("CK_Orders_Status",
                    "[Status] IN ('Pending', 'Confirmed', 'Preparing', 'Ready', 'Served', 'Paid', 'Cancelled')");

            // Add Order-Staff relationship configuration
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Staff)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.StaffID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.UpdatedByStaff)
                .WithMany()
                .HasForeignKey(o => o.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Categories
            modelBuilder.Entity<Category>()
                .HasKey(c => c.CategoryID);
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.CategoryName)
                .IsUnique();

            // Configure ComboDetails
            modelBuilder.Entity<ComboDetail>()
                .HasKey(cd => cd.ComboDetailID);
            modelBuilder.Entity<ComboDetail>()
                .HasOne(cd => cd.PromotionCombo)
                .WithMany(pc => pc.ComboDetails)
                .HasForeignKey(cd => cd.PromotionComboID);

            // Configure Customers
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerID);
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.PhoneNumber)
                .IsUnique();

            // Configure MenuItems
            modelBuilder.Entity<MenuItem>()
                .HasKey(m => m.MenuItemID);
            modelBuilder.Entity<MenuItem>()
                .HasIndex(m => m.ItemName)
                .IsUnique();
            modelBuilder.Entity<MenuItem>()
                .Property(m => m.Price)
                .HasColumnType("decimal(10,2)");

            // Configure OrderDetails
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => od.OrderDetailID);
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(o => o.OrderID);
            // Configure PromotionCombos
            modelBuilder.Entity<PromotionCombo>()
                .HasKey(pc => pc.PromotionComboID);
            modelBuilder.Entity<PromotionCombo>()
                .HasOne(pc => pc.Promotion)
                .WithMany(p => p.PromotionCombos)
                .HasForeignKey(pc => pc.PromotionID);

            // Configure PromotionItems
            modelBuilder.Entity<PromotionItem>()
                .HasKey(pi => pi.PromotionItemID);
            modelBuilder.Entity<PromotionItem>()
                .HasOne(pi => pi.Promotion)
                .WithMany(p => p.PromotionItems)
                .HasForeignKey(pi => pi.PromotionID);

            // Configure Promotions
            modelBuilder.Entity<Promotion>()
                .HasKey(p => p.PromotionID);
            modelBuilder.Entity<Promotion>()
                .HasOne(p => p.PromotionType)
                .WithMany(pt => pt.Promotions)
                .HasForeignKey(p => p.PromotionTypeID);

            // Configure PromotionTypes
            modelBuilder.Entity<PromotionType>()
                .HasKey(pt => pt.PromotionTypeID);
            modelBuilder.Entity<PromotionType>()
                .HasIndex(pt => pt.TypeName)
                .IsUnique();

            // Configure PromotionUsages
            modelBuilder.Entity<PromotionUsage>()
                .HasKey(pu => pu.PromotionUsageID);
            modelBuilder.Entity<PromotionUsage>()
                .HasOne(pu => pu.Promotion)
                .WithMany(p => p.PromotionUsages)
                .HasForeignKey(pu => pu.PromotionID);

            // Configure QRSessions
            modelBuilder.Entity<QRSession>()
                .HasKey(q => q.QRSessionID);
            modelBuilder.Entity<QRSession>()
                .HasIndex(q => q.SessionToken)
                .IsUnique();

            // Configure Reviews
            modelBuilder.Entity<Review>()
                .HasKey(r => r.ReviewID);
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Order)
                .WithMany(o => o.Reviews)
                .HasForeignKey(r => r.OrderID);
            modelBuilder.Entity<Review>()
                .HasCheckConstraint("CK_Reviews_Rating", "[Rating] BETWEEN 0 AND 5");

            // Configure Roles
            modelBuilder.Entity<Role>()
                .HasKey(r => r.RoleID);
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.RoleName)
                .IsUnique();

            // Add Role-Staff relationship configuration
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Staffs)
                .WithOne(s => s.Role)
                .HasForeignKey(s => s.RoleID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>()
                .HasOne(r => r.UpdatedByStaff)
                .WithMany()
                .HasForeignKey(r => r.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Staff
            modelBuilder.Entity<Staff>()
                .HasKey(s => s.StaffID);
            modelBuilder.Entity<Staff>()
                .HasIndex(s => s.Email)
                .IsUnique();

            // Configure StaffSchedules
            modelBuilder.Entity<StaffSchedule>()
                .HasKey(ss => ss.ScheduleID);
            modelBuilder.Entity<StaffSchedule>()
                .HasOne(ss => ss.Staff)
                .WithMany(s => s.StaffSchedules)
                .HasForeignKey(ss => ss.StaffID);

            // Configure Tables
            modelBuilder.Entity<Table>()
                .HasKey(t => t.TableID);
            modelBuilder.Entity<Table>()
                .HasIndex(t => t.TableNumber)
                .IsUnique();
            modelBuilder.Entity<Table>()
                .HasIndex(t => t.QRCode)
                .IsUnique();

            // Configure WorkShifts
            modelBuilder.Entity<WorkShift>()
                .HasKey(ws => ws.WorkShiftID);
            modelBuilder.Entity<WorkShift>()
                .HasIndex(ws => ws.ShiftName)
                .IsUnique();

            // Configure Zones
            modelBuilder.Entity<Zone>()
                .HasKey(z => z.ZoneID);
            modelBuilder.Entity<Zone>()
                .HasIndex(z => z.ZoneName)
                .IsUnique();

            modelBuilder.Entity<Zone>().HasData(
                new Zone { ZoneID = 1, ZoneName = "Tầng 1", Description = "Khu vực ăn uống chính tầng 1", IsActive = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new Zone { ZoneID = 2, ZoneName = "Tầng 2", Description = "Khu vực ăn uống riêng tư tầng 2", IsActive = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new Zone { ZoneID = 3, ZoneName = "Phòng VIP", Description = "Phòng ăn VIP độc quyền", IsActive = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null }
            );

            // Seed data for Tables
            modelBuilder.Entity<Table>().HasData(
                new Table { TableID = 1, ZoneID = 1, TableNumber = "B01", Capacity = 4, QRCode = "QR_B01", Status = "Maintenance", IsActive = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new Table { TableID = 2, ZoneID = 1, TableNumber = "B02", Capacity = 4, QRCode = "QR_B02", Status = "Reserved", IsActive = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new Table { TableID = 3, ZoneID = 2, TableNumber = "B03", Capacity = 6, QRCode = "QR_B03", Status = "Occupied", IsActive = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new Table { TableID = 4, ZoneID = 3, TableNumber = "B04", Capacity = 8, QRCode = "QR_B04", Status = "Available", IsActive = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null }
            );

            // Seed data for Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleID = 1, RoleName = "Quản lý", Description = "Quản lý nhà hàng", CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new Role { RoleID = 2, RoleName = "Phục vụ", Description = "Nhân viên phục vụ", CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new Role { RoleID = 3, RoleName = "Đầu bếp", Description = "Đầu bếp nhà bếp", CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new Role { RoleID = 4, RoleName = "Thu ngân", Description = "Nhân viên thu ngân", CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null }
            );

            // Seed data for Staff
            modelBuilder.Entity<Staff>().HasData(
                new Staff { StaffID = 1, RoleID = 1, FullName = "Nguyễn Văn An", PhoneNumber = "0901234567", Email = "quanly@nhahang.com", PasswordHash = "hashed_password_1", IsActive = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new Staff { StaffID = 2, RoleID = 2, FullName = "Trần Thị Bình", PhoneNumber = "0901234568", Email = "phucvu1@nhahang.com", PasswordHash = "hashed_password_2", IsActive = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new Staff { StaffID = 3, RoleID = 3, FullName = "Lê Văn Cường", PhoneNumber = "0901234569", Email = "daubep1@nhahang.com", PasswordHash = "hashed_password_3", IsActive = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new Staff { StaffID = 4, RoleID = 4, FullName = "Phạm Thị Dung", PhoneNumber = "0901234570", Email = "thungan1@nhahang.com", PasswordHash = "hashed_password_4", IsActive = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null }
            );

            // Seed data for WorkShifts
            modelBuilder.Entity<WorkShift>().HasData(
                new WorkShift { WorkShiftID = 1, ShiftName = "Ca Sáng", StartTime = TimeSpan.Parse("07:00:00"), EndTime = TimeSpan.Parse("15:00:00"), Description = "Ca làm việc buổi sáng", CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new WorkShift { WorkShiftID = 2, ShiftName = "Ca Chiều", StartTime = TimeSpan.Parse("15:00:00"), EndTime = TimeSpan.Parse("23:00:00"), Description = "Ca làm việc buổi chiều", CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null }
            );

            // Seed data for StaffSchedules
            modelBuilder.Entity<StaffSchedule>().HasData(
                new StaffSchedule { ScheduleID = 1, StaffID = 1, WorkShiftID = 1, ScheduleDate = DateTime.Parse("2025-06-17"), Notes = "Quản lý ca sáng", CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new StaffSchedule { ScheduleID = 2, StaffID = 2, WorkShiftID = 2, ScheduleDate = DateTime.Parse("2025-06-17"), Notes = "Phục vụ ca chiều", CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null }
            );

            // Seed data for Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerID = 1, PhoneNumber = "0912345678", FullName = "Hoàng Văn Em", Email = "khachhang1@gmail.com", LoyaltyPoints = 50.00m, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new Customer { CustomerID = 2, PhoneNumber = "0912345679", FullName = "Nguyễn Thị Phượng", Email = "khachhang2@gmail.com", LoyaltyPoints = 20.00m, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null }
            );

            // Seed data for Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = 1, CategoryName = "Khai vị", Description = "Món ăn khai vị và nhẹ", IsActive = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new Category { CategoryID = 2, CategoryName = "Món chính", Description = "Món ăn chính", IsActive = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new Category { CategoryID = 3, CategoryName = "Tráng miệng", Description = "Món tráng miệng ngọt", IsActive = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new Category { CategoryID = 4, CategoryName = "Đồ uống", Description = "Đồ uống và giải khát", IsActive = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null }
            );

            // Seed data for MenuItems
            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem { MenuItemID = 1, CategoryID = 1, ItemName = "Chả giò", Description = "Chả giò giòn với rau củ", Price = 50000.00m, Cost = 20000.00m, ImageURL = "cha_gio.jpg", IsAvailable = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new MenuItem { MenuItemID = 2, CategoryID = 2, ItemName = "Gà nướng", Description = "Gà nướng với thảo mộc", Price = 150000.00m, Cost = 60000.00m, ImageURL = "ga_nuong.jpg", IsAvailable = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new MenuItem { MenuItemID = 3, CategoryID = 3, ItemName = "Bánh sô-cô-la", Description = "Bánh sô-cô-la đậm đà", Price = 80000.00m, Cost = 30000.00m, ImageURL = "banh_socolate.jpg", IsAvailable = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new MenuItem { MenuItemID = 4, CategoryID = 4, ItemName = "Trà đá", Description = "Trà đá giải khát", Price = 25000.00m, Cost = 5000.00m, ImageURL = "tra_da.jpg", IsAvailable = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null }
            );

            // Seed data for PromotionTypes
            modelBuilder.Entity<PromotionType>().HasData(
                new PromotionType { PromotionTypeID = 1, TypeName = "Phần trăm", Description = "Giảm giá theo phần trăm", CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new PromotionType { PromotionTypeID = 2, TypeName = "Số tiền cố định", Description = "Giảm giá bằng số tiền cố định", CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new PromotionType { PromotionTypeID = 3, TypeName = "Mua X tặng Y", Description = "Mua X món tặng Y món miễn phí", CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new PromotionType { PromotionTypeID = 4, TypeName = "Combo", Description = "Giảm giá cho combo món ăn", CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null }
            );

            // Seed data for Promotions
            modelBuilder.Entity<Promotion>().HasData(
                new Promotion { PromotionID = 1, PromotionTypeID = 1, PromotionName = "Giảm 10%", Description = "Giảm 10% trên tổng hóa đơn", StartDate = DateTime.Parse("2025-06-01"), EndDate = DateTime.Parse("2025-06-30"), DiscountValue = 10.00m, MaxDiscountAmount = 100000.00m, IsActive = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new Promotion { PromotionID = 2, PromotionTypeID = 3, PromotionName = "Mua 2 tặng 1", Description = "Mua 2 món tráng miệng tặng 1 miễn phí", StartDate = DateTime.Parse("2025-06-01"), EndDate = DateTime.Parse("2025-06-30"), BuyQuantity = 2, GetQuantity = 1, IsActive = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new Promotion { PromotionID = 3, PromotionTypeID = 4, PromotionName = "Combo Gia đình", Description = "Combo đặc biệt cho gia đình", StartDate = DateTime.Parse("2025-06-01"), EndDate = DateTime.Parse("2025-06-30"), IsActive = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null }
            );

            // Seed data for PromotionItems
            modelBuilder.Entity<PromotionItem>().HasData(
                new PromotionItem { PromotionItemID = 1, PromotionID = 2, MenuItemID = 3, CategoryID = null },
                new PromotionItem { PromotionItemID = 2, PromotionID = 1, MenuItemID = null, CategoryID = 2 }
            );

            // Seed data for PromotionCombos
            modelBuilder.Entity<PromotionCombo>().HasData(
                new PromotionCombo { PromotionComboID = 1, PromotionID = 3, ComboName = "Combo Gia đình A", ComboPrice = 350000.00m, IsActive = true, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null }
            );

            // Seed data for ComboDetails
            modelBuilder.Entity<ComboDetail>().HasData(
                new ComboDetail { ComboDetailID = 1, PromotionComboID = 1, MenuItemID = 1, Quantity = 2 },
                new ComboDetail { ComboDetailID = 2, PromotionComboID = 1, MenuItemID = 2, Quantity = 1 },
                new ComboDetail { ComboDetailID = 3, PromotionComboID = 1, MenuItemID = 4, Quantity = 2 }
            );

            // Seed data for QRSessions
            modelBuilder.Entity<QRSession>().HasData(
                new QRSession { QRSessionID = 1, TableID = 1, CustomerID = 1, SessionStart = DateTime.Parse("2025-06-17 12:00:00"), SessionEnd = null, SessionToken = "SESSION_001", IsActive = true },
                new QRSession { QRSessionID = 2, TableID = 2, CustomerID = 2, SessionStart = DateTime.Parse("2025-06-17 12:30:00"), SessionEnd = null, SessionToken = "SESSION_002", IsActive = true }
            );

            // Seed data for Orders
            modelBuilder.Entity<Order>().HasData(
                new Order { OrderID = 1, OrderNumber = "ORD001", TableID = 1, CustomerID = 1, CustomerName = "Hoàng Văn Em", CustomerPhone = "0912345678", StaffID = 2, OrderDate = DateTime.Parse("2025-06-17 12:00:00"), Status = "Cancelled", TotalAmount = 405000.00m, DiscountAmount = 0.00m, FinalAmount = 405000.00m, PaymentMethod = "QR_Pay", PaymentStatus = "Partial", Notes = null, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = 4 },
                new Order { OrderID = 2, OrderNumber = "ORD002", TableID = 2, CustomerID = 2, CustomerName = "Nguyễn Thị Phượng", CustomerPhone = "0912345679", StaffID = 2, OrderDate = DateTime.Parse("2025-06-17 12:30:00"), Status = "Served", TotalAmount = 255000.00m, DiscountAmount = 25000.00m, FinalAmount = 230000.00m, PaymentMethod = "QR_Pay", PaymentStatus = "Partial", Notes = "Ưu tiên nhanh", CreatedAt = DateTime.Parse("2025-06-17 12:30:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:30:00"), UpdatedBy = 4 },
                new Order { OrderID = 3, OrderNumber = "ORD003", TableID = 3, CustomerID = null, CustomerName = "Khách lẻ", CustomerPhone = null, StaffID = 2, OrderDate = DateTime.Parse("2025-06-17 13:00:00"), Status = "Ready", TotalAmount = 280000.00m, DiscountAmount = 0.00m, FinalAmount = 280000.00m, PaymentMethod = "QR_Pay", PaymentStatus = "Partial", Notes = null, CreatedAt = DateTime.Parse("2025-06-17 13:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 13:00:00"), UpdatedBy = 4 },
                new Order { OrderID = 4, OrderNumber = "ORD004", TableID = 4, CustomerID = 1, CustomerName = "Hoàng Văn Em", CustomerPhone = "0912345678", StaffID = 2, OrderDate = DateTime.Parse("2025-06-17 13:30:00"), Status = "Ready", TotalAmount = 500000.00m, DiscountAmount = 0.00m, FinalAmount = 500000.00m, PaymentMethod = "Card", PaymentStatus = "Partial", Notes = "Bàn VIP", CreatedAt = DateTime.Parse("2025-06-17 13:30:00"), UpdatedAt = DateTime.Parse("2025-06-17 13:30:00"), UpdatedBy = 4 },
                new Order { OrderID = 5, OrderNumber = "ORD005", TableID = 1, CustomerID = 2, CustomerName = "Nguyễn Thị Phượng", CustomerPhone = "0912345679", StaffID = 2, OrderDate = DateTime.Parse("2025-06-17 14:00:00"), Status = "Ready", TotalAmount = 368000.00m, DiscountAmount = 0.00m, FinalAmount = 368000.00m, PaymentMethod = "Card", PaymentStatus = "Partial", Notes = null, CreatedAt = DateTime.Parse("2025-06-17 14:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 14:00:00"), UpdatedBy = 4 }
            );

            // Seed data for OrderDetails
            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail { OrderDetailID = 1, OrderID = 1, MenuItemID = 1, PromotionComboID = null, Quantity = 3, UnitPrice = 50000.00m, DiscountAmount = 0.00m, TotalPrice = 150000.00m, Notes = null, Status = "Preparing", CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new OrderDetail { OrderDetailID = 2, OrderID = 1, MenuItemID = 2, PromotionComboID = null, Quantity = 1, UnitPrice = 150000.00m, DiscountAmount = 0.00m, TotalPrice = 150000.00m, Notes = "Preparing", Status = "Preparing", CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new OrderDetail { OrderDetailID = 3, OrderID = 1, MenuItemID = 4, PromotionComboID = null, Quantity = 3, UnitPrice = 25000.00m, DiscountAmount = 0.00m, TotalPrice = 75000.00m, Notes = null, Status = "Preparing", CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = null },
                new OrderDetail { OrderDetailID = 4, OrderID = 2, MenuItemID = 1, PromotionComboID = null, Quantity = 2, UnitPrice = 50000.00m, DiscountAmount = 0.00m, TotalPrice = 100000.00m, Notes = null, Status = "Preparing", CreatedAt = DateTime.Parse("2025-06-17 12:30:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:30:00"), UpdatedBy = null },
                new OrderDetail { OrderDetailID = 5, OrderID = 2, MenuItemID = 3, PromotionComboID = null, Quantity = 2, UnitPrice = 80000.00m, DiscountAmount = 25000.00m, TotalPrice = 135000.00m, Notes = "Ưu tiên nhanh", Status = "Preparing", CreatedAt = DateTime.Parse("2025-06-17 12:30:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:30:00"), UpdatedBy = null },
                new OrderDetail { OrderDetailID = 6, OrderID = 3, MenuItemID = 2, PromotionComboID = null, Quantity = 1, UnitPrice = 150000.00m, DiscountAmount = 0.00m, TotalPrice = 150000.00m, Notes = null, Status = "Preparing", CreatedAt = DateTime.Parse("2025-06-17 13:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 13:00:00"), UpdatedBy = null },
                new OrderDetail { OrderDetailID = 7, OrderID = 3, MenuItemID = 4, PromotionComboID = null, Quantity = 2, UnitPrice = 25000.00m, DiscountAmount = 0.00m, TotalPrice = 50000.00m, Notes = null, Status = "Ordered", CreatedAt = DateTime.Parse("2025-06-17 13:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 13:00:00"), UpdatedBy = null },
                new OrderDetail { OrderDetailID = 8, OrderID = 3, MenuItemID = null, PromotionComboID = 1, Quantity = 1, UnitPrice = 350000.00m, DiscountAmount = 0.00m, TotalPrice = 350000.00m, Notes = null, Status = "Served", CreatedAt = DateTime.Parse("2025-06-17 13:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 13:00:00"), UpdatedBy = null },
                new OrderDetail { OrderDetailID = 9, OrderID = 4, MenuItemID = 2, PromotionComboID = null, Quantity = 2, UnitPrice = 150000.00m, DiscountAmount = 0.00m, TotalPrice = 300000.00m, Notes = "Thêm rau", Status = "Served", CreatedAt = DateTime.Parse("2025-06-17 13:30:00"), UpdatedAt = DateTime.Parse("2025-06-17 13:30:00"), UpdatedBy = null },
                new OrderDetail { OrderDetailID = 10, OrderID = 4, MenuItemID = 3, PromotionComboID = null, Quantity = 2, UnitPrice = 80000.00m, DiscountAmount = 0.00m, TotalPrice = 160000.00m, Notes = null, Status = "Served", CreatedAt = DateTime.Parse("2025-06-17 13:30:00"), UpdatedAt = DateTime.Parse("2025-06-17 13:30:00"), UpdatedBy = null },
                new OrderDetail { OrderDetailID = 11, OrderID = 5, MenuItemID = 1, PromotionComboID = null, Quantity = 2, UnitPrice = 50000.00m, DiscountAmount = 0.00m, TotalPrice = 100000.00m, Notes = null, Status = "Served", CreatedAt = DateTime.Parse("2025-06-17 14:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 14:00:00"), UpdatedBy = null },
                new OrderDetail { OrderDetailID = 12, OrderID = 5, MenuItemID = 2, PromotionComboID = null, Quantity = 1, UnitPrice = 150000.00m, DiscountAmount = 0.00m, TotalPrice = 150000.00m, Notes = null, Status = "Served", CreatedAt = DateTime.Parse("2025-06-17 14:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 14:00:00"), UpdatedBy = null },
                new OrderDetail { OrderDetailID = 13, OrderID = 5, MenuItemID = 3, PromotionComboID = null, Quantity = 1, UnitPrice = 80000.00m, DiscountAmount = 0.00m, TotalPrice = 80000.00m, Notes = null, Status = "Ready", CreatedAt = DateTime.Parse("2025-06-17 14:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 14:00:00"), UpdatedBy = null }
            );

            // Seed data for Bills
            modelBuilder.Entity<Bill>().HasData(
                new Bill { BillID = 1, OrderID = 1, BillNumber = "BILL001", BillDate = DateTime.Parse("2025-06-17 12:00:00"), SubTotal = 405000.00m, TaxAmount = 40500.00m, ServiceCharge = 20250.00m, DiscountAmount = 0.00m, TotalAmount = 465750.00m, PaymentMethod = "QR_Pay", PaidAmount = 465750.00m, ChangeAmount = 0.00m, StaffID = 4, CreatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:00:00"), UpdatedBy = 4 },
                new Bill { BillID = 2, OrderID = 2, BillNumber = "BILL002", BillDate = DateTime.Parse("2025-06-17 12:30:00"), SubTotal = 235000.00m, TaxAmount = 23500.00m, ServiceCharge = 11750.00m, DiscountAmount = 25000.00m, TotalAmount = 231250.00m, PaymentMethod = "QR_Pay", PaidAmount = 231250.00m, ChangeAmount = 0.00m, StaffID = 4, CreatedAt = DateTime.Parse("2025-06-17 12:30:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:30:00"), UpdatedBy = 4 },
                new Bill { BillID = 3, OrderID = 3, BillNumber = "BILL003", BillDate = DateTime.Parse("2025-06-17 13:00:00"), SubTotal = 280000.00m, TaxAmount = 28000.00m, ServiceCharge = 14000.00m, DiscountAmount = 0.00m, TotalAmount = 294000.00m, PaymentMethod = "Transfer", PaidAmount = 294000.00m, ChangeAmount = 0.00m, StaffID = 4, CreatedAt = DateTime.Parse("2025-06-17 13:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 13:00:00"), UpdatedBy = 4 },
                new Bill { BillID = 4, OrderID = 4, BillNumber = "BILL004", BillDate = DateTime.Parse("2025-06-17 13:30:00"), SubTotal = 500000.00m, TaxAmount = 50000.00m, ServiceCharge = 25000.00m, DiscountAmount = 0.00m, TotalAmount = 567500.00m, PaymentMethod = "QR_Pay", PaidAmount = 567500.00m, ChangeAmount = 0.00m, StaffID = 4, CreatedAt = DateTime.Parse("2025-06-17 13:30:00"), UpdatedAt = DateTime.Parse("2025-06-17 13:30:00"), UpdatedBy = 4 },
                new Bill { BillID = 5, OrderID = 5, BillNumber = "BILL005", BillDate = DateTime.Parse("2025-06-17 14:00:00"), SubTotal = 368000.00m, TaxAmount = 36800.00m, ServiceCharge = 18400.00m, DiscountAmount = 0.00m, TotalAmount = 423200.00m, PaymentMethod = "Card", PaidAmount = 423200.00m, ChangeAmount = 0.00m, StaffID = 4, CreatedAt = DateTime.Parse("2025-06-17 14:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 14:00:00"), UpdatedBy = 4 }
            );

            // Seed data for PromotionUsage
            modelBuilder.Entity<PromotionUsage>().HasData(
                new PromotionUsage { PromotionUsageID = 1, PromotionID = 1, OrderID = 2, DiscountApplied = 25000.00m, UsedAt = DateTime.Parse("2025-06-17 12:30:00") }
            );

            // Seed data for Reviews
            modelBuilder.Entity<Review>().HasData(
                new Review { ReviewID = 1, OrderID = 1, CustomerID = 1, Rating = 5, Comment = "Dịch vụ tuyệt vời, món ăn ngon!", CreatedAt = DateTime.Parse("2025-06-17 12:30:00"), UpdatedAt = DateTime.Parse("2025-06-17 12:30:00"), UpdatedBy = null },
                new Review { ReviewID = 2, OrderID = 2, CustomerID = 2, Rating = 4, Comment = "Món ăn ngon nhưng phục vụ hơi chậm.", CreatedAt = DateTime.Parse("2025-06-17 13:00:00"), UpdatedAt = DateTime.Parse("2025-06-17 13:00:00"), UpdatedBy = null }
            );
        }
    }
}
