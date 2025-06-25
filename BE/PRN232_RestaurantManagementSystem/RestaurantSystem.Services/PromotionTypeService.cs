using RestaurantSystem.DataAccess;
using RestaurantSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services
{
    public interface IPromotionTypeService
    {
        Task<ServiceResult<List<PromotionTypeModel>>> GetPromotionTypesAsync();
    }
    public class PromotionTypeService : IPromotionTypeService
    {
        private readonly PromotionTypeDAO _promotionTypeDAO;
        public PromotionTypeService(PromotionTypeDAO promotionTypeDAO)
        {
            _promotionTypeDAO = promotionTypeDAO;
        }
        
        public async Task<ServiceResult<List<PromotionTypeModel>>> GetPromotionTypesAsync()
        {
            try
            {
                var promotionTypes = await _promotionTypeDAO.GetPromotionTypesAsync();
                if (promotionTypes == null || !promotionTypes.Any())
                {
                    return ServiceResult<List<PromotionTypeModel>>.NotFound("No promotion types found.");
                }
                return ServiceResult<List<PromotionTypeModel>>.Success(promotionTypes);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<PromotionTypeModel>>.Error($"An error occurred while retrieving promotion types: {ex.Message}");
            }
        }
    }
}
