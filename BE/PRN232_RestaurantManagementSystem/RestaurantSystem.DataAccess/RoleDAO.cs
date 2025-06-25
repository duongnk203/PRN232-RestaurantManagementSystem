using Microsoft.EntityFrameworkCore;
using RestaurantSystem.BusinessObjects.Models;
using RestaurantSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.DataAccess
{
    public class RoleDAO
    {
        private readonly AnJiiDbContext _context;
        public RoleDAO(AnJiiDbContext context)
        {
            _context = context;
        }

        public async Task<List<RoleModel>> GetRolesAsync()
        {
            return await _context.Roles
                .Select(r => new RoleModel
                {
                    Id = r.RoleID,
                    Name = r.RoleName
                })
                .ToListAsync();
        }
    }
}
