using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        
    }
}
