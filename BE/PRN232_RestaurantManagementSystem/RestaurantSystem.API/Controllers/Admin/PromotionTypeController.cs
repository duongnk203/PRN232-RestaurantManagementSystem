using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.DTOs;
using RestaurantSystem.Services;

namespace RestaurantSystem.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionTypeController : BaseController
    {
        private readonly IPromotionTypeService _promotionTypeService;
        public PromotionTypeController(IPromotionTypeService promotionTypeService)
        {
            _promotionTypeService = promotionTypeService;
        }

        [HttpGet("GetPromotionTypes")]
        public async Task<IActionResult> GetPromtionTypes()
        {
            var result = await _promotionTypeService.GetPromotionTypesAsync();
            if (result == null)
            {
                return NotFoundWrapper<List<PromotionTypeModel>>("No promotion types found.");
            }
            return HandleServiceResult(result);
        }
    }
}
