using Microsoft.EntityFrameworkCore;
using Restaurant.Helpers;
using RestaurantSystem.BusinessObjects.Models;
using RestaurantSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.DataAccess
{
    public class StaffDAO
    {
        private readonly AnJiiDbContext _context;
        public StaffDAO(AnJiiDbContext context)
        {
            _context = context;
        }

        public async Task<List<StaffDto>> GetStaffs()
        {
            var query = await _context.Staffs
                .Select(s => new StaffDto
                {
                    StaffId = s.StaffID,
                    FullName = s.FullName,
                    Email = s.Email,
                    RoleId = s.Role.RoleID,
                    PhoneNumber = s.PhoneNumber,
                    IsActive = s.IsActive
                })
                .ToListAsync();

            return query;
        }

        public async Task<StaffDto> GetStaffById(int id)
        {
            var query = await _context.Staffs
                .Where(s => s.StaffID == id)
                .Select(s => new StaffDto
                {
                    StaffId = s.StaffID,
                    FullName = s.FullName,
                    Email = s.Email,
                    RoleId = s.Role.RoleID,
                    PhoneNumber = s.PhoneNumber,
                    IsActive = s.IsActive
                })
                .FirstOrDefaultAsync();
            return query;
        }

        public async Task<int> CreateStaff(CreateStaff createStaff)
        {
            await _context.Staffs.AddAsync(new Staff
            {
                FullName = createStaff.FullName,
                PhoneNumber = createStaff.PhoneNumber,
                Email = createStaff.Email,
                RoleID = createStaff.RoleId,
                PasswordHash = createStaff.Password,
                IsActive = createStaff.IsActive
            });
            return await _context.SaveChangesAsync();
        }
        public async Task<bool> IsStaffDuplicateAsync(string email, string phoneNumber)
        {
            return await _context.Staffs.AnyAsync(s =>
                (s.Email != null && s.Email == email) ||
                (s.PhoneNumber != null && s.PhoneNumber == phoneNumber)
            );
        }

        public async Task<int> UpdateStaff(UpdateStaff updateStaff)
        {
            var staff = await _context.Staffs.FindAsync(updateStaff.StaffId);
            if (staff == null) return 0;
            staff.FullName = updateStaff.FullName;
            staff.PhoneNumber = updateStaff.PhoneNumber;
            staff.Email = updateStaff.Email;
            staff.RoleID = updateStaff.RoleId;
            staff.IsActive = updateStaff.IsActive;
            staff.UpdatedAt = DateTime.Now;
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> IsCheckDupilcateEmailAndPhoneNumberAsync(int id, string email, string phoneNumber)
        {
            var staff = await _context.Staffs.FindAsync(id);
            if (staff == null) return false;
            if(staff.Email != email)
            {
                var isEmailExists = await _context.Staffs.AnyAsync(s => s.Email == email);
                if (isEmailExists) return true;
            }
            if(staff.PhoneNumber != phoneNumber)
            {
                var isPhoneExists = await _context.Staffs.AnyAsync(s => s.PhoneNumber == phoneNumber);
                if (isPhoneExists) return true;
            }
            return false;   
        }

        public async Task<int> DeleteStaff(int id)
        {
            var staff = await _context.Staffs.FindAsync(id);
            if (staff == null) return 0;
            staff.IsActive = false; // Soft delete
            staff.UpdatedAt = DateTime.Now;
            return await _context.SaveChangesAsync();
        }
    }
}
