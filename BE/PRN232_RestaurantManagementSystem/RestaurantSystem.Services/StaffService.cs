using Microsoft.EntityFrameworkCore;
using Restaurant.Helpers;
using RestaurantSystem.DataAccess;
using RestaurantSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services
{
    public interface IStaffService
    {
        Task<ServiceResult<StaffDTO>> GetStaffs(StaffQueryDTO query);
        Task<ServiceResult<StaffDto>> CreateStaff(CreateStaff createStaff);
        Task<ServiceResult<UpdateStaff>> UpdateStaff(UpdateStaff updateStaff);
        Task<ServiceResult<int>> DeleteStaff(int staffId);
    }
    public class StaffService : IStaffService
    {
        private readonly StaffDAO _staffDAO;
        private readonly IEmailService _emailService;
        public StaffService(StaffDAO staffDAO, IEmailService emailService)
        {
            _staffDAO = staffDAO;
            _emailService = emailService;
        }

        public async Task<ServiceResult<StaffDTO>> GetStaffs(StaffQueryDTO query)
        {
            try
            {
                StaffDTO staffDTO = new StaffDTO();
                var staffs = await _staffDAO.GetStaffs();

                if (staffs == null || !staffs.Any())
                {
                    return ServiceResult<StaffDTO>.NotFound("No staff members found");
                }
                if (query != null)
                {
                    if (query.RoleId != null)
                    {
                        staffs = staffs.Where(s => s.RoleId == query.RoleId).ToList();
                    }
                    if (!string.IsNullOrEmpty(query.FullName))
                    {
                        staffs = staffs.Where(s => s.FullName.Contains(query.FullName, StringComparison.OrdinalIgnoreCase) ||
                                                   s.Email.Contains(query.FullName, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                    if (query.IsActive.HasValue)
                    {
                        staffs = staffs.Where(s => s.IsActive == query.IsActive.Value).ToList();
                    }
                    if (query.Page < 1) query.Page = 1;
                    if (query.Limit < 1) query.Limit = 10;
                    int skip = (query.Page - 1) * query.Limit;
                    staffs = staffs.Skip(skip).Take(query.Limit).ToList();
                }
                staffDTO.Staffs = staffs;
                staffDTO.Query = query;

                return ServiceResult<StaffDTO>.Success(staffDTO, "Staff list retrieved successfully");
            }
            catch (Exception ex)
            {
                return ServiceResult<StaffDTO>.Error(
                    "An error occurred while retrieving staff list",
                    new List<string> { ex.Message }
                );
            }
        }

        public async Task<ServiceResult<StaffDto>> CreateStaff(CreateStaff createStaff)
        {
            try
            {
                if (createStaff == null)
                {
                    return ServiceResult<StaffDto>.Fail("CreateStaff object cannot be null", HttpStatusCode.BadRequest);
                }
                if (string.IsNullOrWhiteSpace(createStaff.FullName) || string.IsNullOrWhiteSpace(createStaff.Email) || string.IsNullOrWhiteSpace(createStaff.PhoneNumber))
                {
                    return ServiceResult<StaffDto>.Fail("FullName, Email, and PhoneNumber are required", HttpStatusCode.BadRequest);
                }
                if (_staffDAO.IsStaffDuplicateAsync(createStaff.Email, createStaff.PhoneNumber).Result)
                {
                    return ServiceResult<StaffDto>.Fail("A staff member with the same email or phone number already exists", HttpStatusCode.Conflict);
                }
                var staff = new StaffDto
                {
                    FullName = createStaff.FullName,
                    PhoneNumber = createStaff.PhoneNumber,
                    Email = createStaff.Email,
                    IsActive = createStaff.IsActive,
                    RoleId = createStaff.RoleId
                };
                var password = createStaff.Password ?? GeneratePassword();
                createStaff.Password = SecurePasswordHasher.Hash(password);
                var numberCreateStaff = await _staffDAO.CreateStaff(createStaff);
                if (numberCreateStaff == 0)
                {
                    return ServiceResult<StaffDto>.Fail("Failed to create staff", HttpStatusCode.InternalServerError);
                }
                var bodyHtml = BodyHtml(staff.FullName, password);
                await _emailService.SendEmailAsync(staff.Email, "Welcome to AnJii Restaurant", bodyHtml, true);
                return ServiceResult<StaffDto>.Success(staff, "Staff created successfully");
            }
            catch (Exception ex)
            {
                return ServiceResult<StaffDto>.Error(
                    "An error occurred while retrieving staff list", new List<string> { ex.Message }
                );
            }
        }

        private string GeneratePassword(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private string BodyHtml(string fullName, string password)
        {
            return $@"
                <html>
                    <body>
                        <h1>Welcome {fullName}!</h1>
                        <p>Your account has been created successfully.</p>
                        <p><strong>Email:</strong> {fullName}</p>
                        <p><strong>Password:</strong> {password}</p>
                        <p>Please log in to your account.</p>
                    </body>
                </html>";
        }

        public async Task<ServiceResult<UpdateStaff>> UpdateStaff(UpdateStaff updateStaff)
        {
            if (updateStaff.StaffId <= 0)
            {
                return ServiceResult<UpdateStaff>.Fail("Invalid staff ID", HttpStatusCode.BadRequest);
            }
            if (updateStaff == null)
            {
                return ServiceResult<UpdateStaff>.Fail("UpdateStaff object cannot be null", HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(updateStaff.FullName) || string.IsNullOrWhiteSpace(updateStaff.Email) || string.IsNullOrWhiteSpace(updateStaff.PhoneNumber))
            {
                return ServiceResult<UpdateStaff>.Fail("FullName, Email, and PhoneNumber are required", HttpStatusCode.BadRequest);
            }
            if (_staffDAO.IsCheckDupilcateEmailAndPhoneNumberAsync(updateStaff.StaffId, updateStaff.Email, updateStaff.PhoneNumber).Result)
            {
                return ServiceResult<UpdateStaff>.Fail("A staff member with the same email or phone number already exists", HttpStatusCode.Conflict);
            }

            var numberUpdate = await _staffDAO.UpdateStaff(updateStaff);
            if(numberUpdate == 0)
            {
                return ServiceResult<UpdateStaff>.Fail("Staff not found", HttpStatusCode.NotFound);
            }
            return ServiceResult<UpdateStaff>.Success(updateStaff, "Staff updated successfully");

        }

        public async Task<ServiceResult<int>> DeleteStaff(int staffId)
        {
            if (staffId <= 0)
            {
                return ServiceResult<int>.Fail("Invalid staff ID", HttpStatusCode.BadRequest);
            }
            var numberDelete = await _staffDAO.DeleteStaff(staffId);
            if (numberDelete == 0)
            {
                return ServiceResult<int>.Fail("Staff not found", HttpStatusCode.NotFound);
            }
            return ServiceResult<int>.Success(numberDelete, "Staff deleted successfully");
        }
    }
}
