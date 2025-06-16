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
        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuItemCategory> MenuItemCategories { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<EmployeeShift> EmployeeShifts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Category
            modelBuilder.Entity<Category>()
                .HasKey(c => c.CategoryID);

            // Configure MenuItem
            modelBuilder.Entity<MenuItem>()
                .HasKey(m => m.ItemID);

            // Configure MenuItemCategory (Many-to-Many)
            modelBuilder.Entity<MenuItemCategory>()
                .HasKey(mc => new { mc.ItemID, mc.CategoryID });

            modelBuilder.Entity<MenuItemCategory>()
                .HasOne(mc => mc.MenuItem)
                .WithMany(m => m.MenuItemCategories)
                .HasForeignKey(mc => mc.ItemID);

            modelBuilder.Entity<MenuItemCategory>()
                .HasOne(mc => mc.Category)
                .WithMany(c => c.MenuItemCategories)
                .HasForeignKey(mc => mc.CategoryID);

            // Configure Table
            modelBuilder.Entity<Table>()
                .HasKey(t => t.TableID);

            // Configure Customer
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerID);

            // Configure Employee
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmployeeID);

            // Configure Shift
            modelBuilder.Entity<Shift>()
                .HasKey(s => s.ShiftID);

            // Configure EmployeeShift
            modelBuilder.Entity<EmployeeShift>()
                .HasKey(es => es.EmployeeShiftID);

            modelBuilder.Entity<EmployeeShift>()
                .HasOne(es => es.Employee)
                .WithMany(e => e.EmployeeShifts)
                .HasForeignKey(es => es.EmployeeID);

            modelBuilder.Entity<EmployeeShift>()
                .HasOne(es => es.Shift)
                .WithMany(s => s.EmployeeShifts)
                .HasForeignKey(es => es.ShiftID);

            // Configure Order
            modelBuilder.Entity<Order>()
                .HasKey(o => o.OrderID);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Table)
                .WithMany(t => t.Orders)
                .HasForeignKey(o => o.TableID)
                .IsRequired(false);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerID)
                .IsRequired(false);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Employee)
                .WithMany(e => e.Orders)
                .HasForeignKey(o => o.EmployeeID);

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderType)
                .HasConversion<string>()
                .HasMaxLength(50);

            modelBuilder.Entity<Order>()
                .HasCheckConstraint("CHK_OrderType", "OrderType IN ('DineIn', 'TakeAway')");

            modelBuilder.Entity<Order>()
                .Property(o => o.PaymentMethod)
                .HasConversion<string>()
                .HasMaxLength(50);

            modelBuilder.Entity<Order>()
                .HasCheckConstraint("CHK_PaymentMethod", "PaymentMethod IN ('Cash', 'Online')");

            // Configure OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => od.OrderDetailID);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.MenuItem)
                .WithMany(m => m.OrderDetails)
                .HasForeignKey(od => od.ItemID);

          
        }



    }
}
