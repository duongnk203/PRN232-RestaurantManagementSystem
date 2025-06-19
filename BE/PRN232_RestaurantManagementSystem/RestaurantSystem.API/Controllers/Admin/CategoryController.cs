using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Services;

namespace RestaurantSystem.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _categoryService.GetCategoriesAsync();
            return HandleServiceResult(result);

        }
    }
}
