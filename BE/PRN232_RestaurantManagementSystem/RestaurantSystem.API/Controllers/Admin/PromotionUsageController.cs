using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.DTOs;
using RestaurantSystem.Services;

namespace RestaurantSystem.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionUsageController : BaseController
    {
        private readonly IPromotionUsageService _promotionUsageService;
        public PromotionUsageController(IPromotionUsageService promotionUsageService)
        {
            _promotionUsageService = promotionUsageService;
        }
        [HttpGet("GetPromotionUsage")]
        public async Task<IActionResult> GetPromotionUsage([FromQuery] PromotionUsageQueryDTO query)
        {
            var promotionUsageResult = await _promotionUsageService.GetPromotionUsageAsync(query);
            if (promotionUsageResult == null || promotionUsageResult.Data.PromotionUsages == null || !promotionUsageResult.Data.PromotionUsages.Any())
            {
                return NotFoundWrapper<List<PromotionUsageDto>>("No promotion usages found.");
            }
            return HandleServiceResult(promotionUsageResult);
        }
    }
}
