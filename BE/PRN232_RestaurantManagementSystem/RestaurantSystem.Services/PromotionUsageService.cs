using RestaurantSystem.DataAccess;
using RestaurantSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services
{
    public interface IPromotionUsageService
    {
        Task<ServiceResult<PromotionUsageDTO>> GetPromotionUsageAsync(PromotionUsageQueryDTO query);
    }
    public class PromotionUsageService : IPromotionUsageService
    {
        private readonly PromotionUsageDAO _promotionUsageDAO;
        public PromotionUsageService(PromotionUsageDAO promotionUsageDAO)
        {
            _promotionUsageDAO = promotionUsageDAO;
        }
        public async Task<ServiceResult<PromotionUsageDTO>> GetPromotionUsageAsync(PromotionUsageQueryDTO query)
        {
            
            try
            {
                var promotionUsages = await _promotionUsageDAO.GetPromotionUsageAsync();
                if (promotionUsages == null || !promotionUsages.Any())
                {
                    return ServiceResult<PromotionUsageDTO>.Fail("No promotion usages found.");
                }
                // Apply filtering based on query parameters
                if (!string.IsNullOrEmpty(query.OrderNumber))
                {
                    promotionUsages = promotionUsages.Where(pu => pu.OrderNumber.Contains(query.OrderNumber)).ToList();
                }
                if (query.PromotionTypeID > 0)
                {
                    promotionUsages = promotionUsages.Where(pu => pu.PromotionTypeID == query.PromotionTypeID).ToList();
                }
                if (!string.IsNullOrEmpty(query.SearchCustomer))
                {
                    promotionUsages = promotionUsages.Where(pu => pu.CustomerName.Contains(query.SearchCustomer)).ToList();
                }
                if (query.StartDate.HasValue)
                {
                    promotionUsages = promotionUsages.Where(pu => pu.UsageDate >= query.StartDate.Value).ToList();
                }
                if (query.EndDate.HasValue)
                {
                    promotionUsages = promotionUsages.Where(pu => pu.UsageDate <= query.EndDate.Value).ToList();
                }
                // Pagination
                int totalCount = promotionUsages.Count;
                int skip = (query.Page - 1) * query.Limit;
                var paginatedUsages = promotionUsages.Skip(skip).Take(query.Limit).ToList();
                return ServiceResult<PromotionUsageDTO>.Success(new PromotionUsageDTO
                {
                    PromotionUsages = paginatedUsages,
                    Query = query
                });
            }
            catch (Exception ex)
            {
                return ServiceResult<PromotionUsageDTO>.Fail($"An error occurred while retrieving promotion usages: {ex.Message}");
            }
        }
    }
}
