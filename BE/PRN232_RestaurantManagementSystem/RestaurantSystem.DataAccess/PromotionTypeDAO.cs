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
    public class PromotionTypeDAO
    {
        private readonly AnJiiDbContext _context;
        public PromotionTypeDAO(AnJiiDbContext context)
        {
            _context = context;
        }
        public async Task<List<PromotionTypeModel>> GetPromotionTypesAsync()
        {
            return await _context.PromotionTypes
                .Select(pt => new PromotionTypeModel
                {
                    PromotionTypeId = pt.PromotionTypeID,
                    PromotionTypeName = pt.TypeName
                })
                .ToListAsync();
        }
    }
}
