using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

    }
}
