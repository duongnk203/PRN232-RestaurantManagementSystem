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
                    PromotionId = p.PromotionID,
                    PromotionTypeId = p.PromotionTypeID,
                    PromotionName = p.PromotionName,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Description = p.Description,
                    DiscountValue = p.DiscountValue,
                    MaxDiscountAmount = p.MaxDiscountAmount,
                    BuyQuantity = p.BuyQuantity,
                    GetQuantity = p.GetQuantity,
                    IsActive = p.IsActive
                })
                .ToListAsync();
        }

        public async Task<int> CreatePromotion(CreatePromotion createPromotion)
        {
            var promotion = new Promotion
            {
                PromotionTypeID = createPromotion.PromotionTypeId,
                PromotionName = createPromotion.PromotionName,
                Description = createPromotion.Description,
                StartDate = createPromotion.StartDate,
                EndDate = createPromotion.EndDate,
                DiscountValue = createPromotion.DiscountValue,
                MaxDiscountAmount = createPromotion.MaxDiscountAmount,
                BuyQuantity = createPromotion.BuyQuantity,
                GetQuantity = createPromotion.GetQuantity,
                IsActive = createPromotion.IsActive,
                CreatedAt = DateTime.Now,
            };
            _context.Promotions.Add(promotion);
            await _context.SaveChangesAsync();
            return promotion.PromotionID;
        }
        public async Task<int> UpdatePromotion(UpdatePromotion updatePromotion)
        {
            var promotion = await _context.Promotions.FindAsync(updatePromotion.PromotionId);
            if (promotion == null)
            {
                return 0;
            }
            promotion.PromotionTypeID = updatePromotion.PromotionTypeId;
            promotion.PromotionName = updatePromotion.PromotionName;
            promotion.Description = updatePromotion.Description;
            promotion.StartDate = updatePromotion.StartDate;
            promotion.EndDate = updatePromotion.EndDate;
            promotion.DiscountValue = updatePromotion.DiscountValue;
            promotion.MaxDiscountAmount = updatePromotion.MaxDiscountAmount;
            promotion.BuyQuantity = updatePromotion.BuyQuantity;
            promotion.GetQuantity = updatePromotion.GetQuantity;
            promotion.IsActive = updatePromotion.IsActive;
            promotion.UpdatedAt = DateTime.Now;

            _context.Promotions.Update(promotion);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeletePromotion(int promotionId)
        {
            var promotion = await _context.Promotions.FindAsync(promotionId);
            if (promotion == null)
            {
                return 0;
            }
            promotion.IsActive = false;
            promotion.UpdatedAt = DateTime.Now;
            _context.Update(promotion);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> IsDuplicateNameAndTypeInTimeRange(string promotionName, int promotionTypeId, DateTime startDate, DateTime endDate)
        {
            return await _context.Promotions.AnyAsync(p =>
                p.PromotionName == promotionName &&
                p.PromotionTypeID == promotionTypeId &&
                p.IsActive &&
                ((p.StartDate <= endDate && p.EndDate >= startDate) || (p.StartDate >= startDate && p.EndDate <= endDate)));
        }

        public async Task<bool> IsPromotionOverlapAsync(int promotionId, int promotionTypeId, DateTime startDate, DateTime endDate)
        {
            return await _context.Promotions.AnyAsync(p =>
                p.PromotionID != promotionId &&
                p.PromotionTypeID == promotionTypeId &&
                p.IsActive &&
                ((p.StartDate <= endDate && p.EndDate >= startDate) || (p.StartDate >= startDate && p.EndDate <= endDate)));
        }
        public async Task<Promotion> GetPromotionByIdAsync(int promotionId)
        {
            return await _context.Promotions
                .Include(p => p.PromotionType)
                .FirstOrDefaultAsync(p => p.PromotionID == promotionId);
        }

    }
}
