using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.DTOs;
using RestaurantSystem.Services;

namespace RestaurantSystem.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : BaseController
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("GetMenus")]
        public async Task<IActionResult> GetMenus([FromQuery] MenuQueryDTO menuQuery)
        {
            var result = await _menuService.GetMenusAsync(menuQuery);
            return HandleServiceResult(result);
        }
        [HttpPost("CreateMenuItem")]
        public async Task<IActionResult> CreateMenuItem([FromBody] CreateMenu createMenu)
        {
            if (createMenu == null)
            {
                return BadRequestWrapper<CreateMenu>("Invalid menu item data");
            }
            var result = await _menuService.CreateMenuItemAsync(createMenu);
            return HandleServiceResult(result);
        }

        [HttpPut("UpdateMenuItem")]
        public async Task<IActionResult> UpdateMenuItem([FromBody] UpdateMenu updateMenu)
        {
            if (updateMenu == null)
            {
                return BadRequestWrapper<int>("Invalid menu item data");
            }
            var result = await _menuService.UpdateMenuItemAsync(updateMenu);
            return HandleServiceResult(result);
        }

        [HttpDelete("DeleteMenuItem/{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            if (id <= 0)
            {
                return BadRequestWrapper<int>("Invalid menu item ID");
            }
            var result = await _menuService.DeleteMenuItemAsync(id);
            return HandleServiceResult(result);
        }

        [HttpGet("SearchMenuItems")]
        public async Task<IActionResult> SearchMenuItems([FromQuery] string? searchMenu, int? categoryId)
        {
            var result = await _menuService.GetMenuItemBySearch(searchMenu, categoryId);
            if (result == null || result.Data == null || !result.Data.Any())
            {
                return NotFoundWrapper<List<MenuItemModel>>("No menu items found matching the search criteria.");
            }
            return HandleServiceResult(result);
        }
    }
}
