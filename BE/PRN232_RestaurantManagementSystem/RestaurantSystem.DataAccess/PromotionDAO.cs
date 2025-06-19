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
    public class PromotionDAO
    {
        private readonly AnJiiDbContext _context;
        public PromotionDAO(AnJiiDbContext context)
        {
            _context = context;
        }

        public async Task<List<PromotionDto>> GetPromotionsAsync()
        {
            return await _context.Promotions
                .Select(p => new PromotionDto
                {
                    PromotionID = p.PromotionID,
                    PromotionTypeID = p.PromotionTypeID,
                    PromotionName = p.PromotionName,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    DiscountValue = p.DiscountValue,
                    MaxDiscountAmount = p.MaxDiscountAmount,
                    BuyQuantity = p.BuyQuantity,
                    GetQuantity = p.GetQuantity,
                    IsActive = p.IsActive
                })
                .ToListAsync();
        }
        
    }
}
