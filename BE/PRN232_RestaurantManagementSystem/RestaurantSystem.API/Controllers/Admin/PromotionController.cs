using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.DTOs;
using RestaurantSystem.Services;

namespace RestaurantSystem.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : BaseController
    {
        private readonly IPromotionService _promotionService;
        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpGet("GetPromotions")]
        public async Task<IActionResult> GetPromotions([FromQuery]PromotionQueryDTO query)
        {
            var result = await _promotionService.GetPromotionsAsync(query);
            if(result == null)
            {
                return NotFoundWrapper<List<PromotionDto>>("No promotions found.");
            }
            return HandleServiceResult(result);
        }

        [HttpPost("CreatePromotion")]
        public async Task<IActionResult> CreatePromotion([FromBody] CreatePromotion createPromotion)
        {
            if (createPromotion == null)
            {
                return BadRequestWrapper<CreatePromotion>("Invalid promotion data.");
            }
            var result = await _promotionService.CreatePromotionAsync(createPromotion);
            if(result == null)
            {
                return BadRequestWrapper<int>("Failed to create promotion. Please check the data and try again.");
            }
            return HandleServiceResult(result);
        }

        [HttpPut("UpdatePromotion")]
        public async Task<IActionResult> UpdatePromotion([FromBody] UpdatePromotion updatePromotion)
        {
            if (updatePromotion == null)
            {
                return BadRequestWrapper<UpdatePromotion>("Invalid promotion data.");
            }
            var result = await _promotionService.UpdatePromotionAsync(updatePromotion);
            if(result == null)
            {
                return BadRequestWrapper<int>("Failed to update promotion. Please check the data and try again.");
            }
            return HandleServiceResult(result);
        }

        [HttpDelete("DeletePromotion/{id}")]
        public async Task<IActionResult> DeletePromotion(int id)
        {
            if (id <= 0)
            {
                return BadRequestWrapper<int>("Invalid promotion ID.");
            }
            var result = await _promotionService.DeletePromotionAsync(id);
            if(result == null)
            {
                return NotFoundWrapper<int>("Promotion not found or could not be deleted.");
            }
            return HandleServiceResult(result);
        }

    }
}
