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
    public class PromotionComboDAO
    {
        private readonly AnJiiDbContext _context;
        public PromotionComboDAO(AnJiiDbContext context)
        {
            _context = context;
        }

        public async Task<List<PromotionComboDto>> GetPromotionComboAsync()
        {
            return await _context.PromotionCombos
                .Include(pc => pc.Promotion)
                .Select(pc => new PromotionComboDto
                {
                    PromotionComboId = pc.PromotionComboID,
                    PromotionComboName =  pc.ComboName,
                    PromotionName = pc.Promotion.PromotionName,
                    PromotionId = pc.PromotionID,
                    Price = pc.ComboPrice,
                    IsActive = pc.IsActive,
                    CreatedAt = (DateTime)pc.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<int> CreatePromotionCombo(PromotionComboBase createPromotionCombo)
        {
            var promotionCombo = new PromotionCombo
            {
                ComboName = createPromotionCombo.PromotionComboName,
                PromotionID = createPromotionCombo.PromotionId,
                ComboPrice = createPromotionCombo.Price,
                IsActive = createPromotionCombo.IsActive,
                CreatedAt = DateTime.Now
            };
            _context.PromotionCombos.Add(promotionCombo);
            await _context.SaveChangesAsync();
            return promotionCombo.PromotionComboID;
        }

        public async Task<int> UpdatePromotionCombo(PromotionComboBase updatePromotionCombo)
        {
            var promotionCombo = await _context.PromotionCombos.FindAsync(updatePromotionCombo.PromotionComboId);
            if (promotionCombo == null)
            {
                throw new KeyNotFoundException("Promotion Combo not found");
            }
            promotionCombo.ComboName = updatePromotionCombo.PromotionComboName;
            promotionCombo.PromotionID = updatePromotionCombo.PromotionId;
            promotionCombo.ComboPrice = updatePromotionCombo.Price;
            promotionCombo.IsActive = updatePromotionCombo.IsActive;
            promotionCombo.UpdatedAt = DateTime.Now;
            _context.PromotionCombos.Update(promotionCombo);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeletePromotionCombo(int id)
        {
            var promotionCombo = await _context.PromotionCombos.FindAsync(id);
            if (promotionCombo == null)
            {
                return 0;
            }
            promotionCombo.IsActive = false; // Soft delete
            _context.PromotionCombos.Update(promotionCombo);
            return await _context.SaveChangesAsync(); // Returns true if any rows were affected
        }

    }
}
