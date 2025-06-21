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
    public class PromotionUsageDAO
    {
        private readonly AnJiiDbContext _context;
        public PromotionUsageDAO(AnJiiDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsPromotionUsedAsync(int promotionId)
        {
            return await _context.PromotionUsages
                .AnyAsync(pu => pu.PromotionID == promotionId);
        }

        public async Task<List<PromotionUsageDto>> GetPromotionUsageAsync()
        {
            var promotionUsages = await _context.PromotionUsages
                .Include(pu => pu.Promotion)
                .Include(pu => pu.Order)
                .Select(pu => new PromotionUsageDto
                {
                    PromotionUsageID = pu.PromotionUsageID,
                    PromotionName = pu.Promotion.PromotionName,
                    PromotionTypeID = pu.Promotion.PromotionTypeID,
                    OrderNumber = pu.Order.OrderNumber, 
                    CustomerName = pu.Order.CustomerName,
                    UsageDate = pu.UsedAt ?? DateTime.Now,
                    DiscountAmount = pu.DiscountApplied,
                    IsActive = pu.Promotion.IsActive
                })
                .ToListAsync();
            return promotionUsages;
        }
    }
}
