using RestaurantSystem.DataAccess;
using RestaurantSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services
{
    public interface IRoleService
    {
        Task<ServiceResult<List<RoleModel>>> GetRolesAsync();
    }
    public class RoleService : IRoleService
    {
        private readonly RoleDAO _roleDAO;
        public RoleService(RoleDAO roleDAO)
        {
            _roleDAO = roleDAO;
        }
        public async Task<ServiceResult<List<RoleModel>>> GetRolesAsync()
        {
            try
            {
                var roles = await _roleDAO.GetRolesAsync();
                if(roles == null || !roles.Any())
                {
                    return ServiceResult<List<RoleModel>>.NotFound("No roles found");
                }
                return ServiceResult<List<RoleModel>>.Success(roles, "Roles retrieved successfully");
            }
            catch (Exception ex)
            {
                return ServiceResult<List<RoleModel>>.Error(
                    "An error occurred while retrieving roles",
                    new List<string> { ex.Message }
                );
            }
        }
    }
}
