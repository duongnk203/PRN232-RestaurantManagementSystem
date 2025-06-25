using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using RestaurantSystem.Services;

namespace RestaurantSystem.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionItemController : BaseController
    {
        private readonly IPromotionItemService _promotionItemService;

        public PromotionItemController(IPromotionItemService promotionItemService)
        {
            _promotionItemService = promotionItemService;
        }
    }
}
