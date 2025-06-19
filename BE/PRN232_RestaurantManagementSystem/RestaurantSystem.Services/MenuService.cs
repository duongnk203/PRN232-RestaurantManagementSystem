using RestaurantSystem.DataAccess;
using RestaurantSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services
{
    public interface IMenuService
    {
        Task<ServiceResult<MenuDTO>> GetMenusAsync(MenuQueryDTO menuQuery);
        Task<ServiceResult<int>> CreateMenuItemAsync(CreateMenu menu);
        Task<ServiceResult<int>> UpdateMenuItemAsync(UpdateMenu menu);
        Task<ServiceResult<int>> DeleteMenuItemAsync(int id);
    }
    public class MenuService : IMenuService
    {
        private readonly MenuDAO _menuDAO;
        public MenuService(MenuDAO menuDAO)
        {
            _menuDAO = menuDAO;
        }

        public async Task<ServiceResult<MenuDTO>> GetMenusAsync(MenuQueryDTO menuQuery)
        {
            try
            {
                MenuDTO menuDTO = new MenuDTO();
                var menus = await _menuDAO.GetMenus();
                if (menus == null || !menus.Any())
                {
                    return ServiceResult<MenuDTO>.NotFound("No menu items found");
                }
                if (menuQuery != null)
                {
                    if (menuQuery.CategoryId != null)
                    {
                        menus = menus.Where(m => m.CategoryId == menuQuery.CategoryId).ToList();
                    }
                    if (!string.IsNullOrEmpty(menuQuery.SearchItem))
                    {
                        menus = menus.Where(m => m.ItemName.Contains(menuQuery.SearchItem, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                    if (menuQuery.IsAvailable.HasValue)
                    {
                        menus = menus.Where(m => m.IsAvailable == menuQuery.IsAvailable.Value).ToList();
                    }
                    if (menuQuery.Page <= 1) menuQuery.Page = 1;
                    if (menuQuery.Limit <= 1) menuQuery.Limit = 10;
                    int skip = (menuQuery.Page - 1) * menuQuery.Limit;
                    menus = menus.Skip(skip).Take(menuQuery.Limit).ToList();

                }
                menuDTO.Menus = menus;
                menuDTO.Query = menuQuery;
                return ServiceResult<MenuDTO>.Success(menuDTO, "Menus retrieved successfully");
            }
            catch (Exception ex)
            {
                return ServiceResult<MenuDTO>.Error("An error occurred while retrieving staff list", new List<string> { ex.Message });
            }
        }

        public async Task<ServiceResult<int>> CreateMenuItemAsync(CreateMenu menu)
        {
            try
            {
                if (await _menuDAO.IsMenuItemDuplicateAsync(menu.ItemName, menu.CategoryId))
                {
                    return ServiceResult<int>.Error("Menu item already exists in this category");
                }
                if (menu.Price <= menu.Cost)
                {
                    return ServiceResult<int>.Error("Price must be greater than cost");
                }
                int result = await _menuDAO.CreateMenuItem(menu);
                if (result > 0)
                {
                    return ServiceResult<int>.Success(result, "Menu item created successfully");
                }
                return ServiceResult<int>.Error("Failed to create menu item");
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.Error("An error occurred while creating menu item", new List<string> { ex.Message });
            }
        }

        public async Task<ServiceResult<int>> UpdateMenuItemAsync(UpdateMenu menu)
        {
            try
            {
                if (await _menuDAO.IsMenuItemNameExistAsync(menu.MenuItemId, menu.ItemName, menu.CategoryId))
                {
                    return ServiceResult<int>.Error("Menu item already exists in this category");
                }
                if (menu.Price <= menu.Cost)
                {
                    return ServiceResult<int>.Error("Price must be greater than cost");
                }
                int result = await _menuDAO.UpdateMenuItem(menu.MenuItemId, menu);
                if (result > 0)
                {
                    return ServiceResult<int>.Success(result, "Menu item updated successfully");
                }
                return ServiceResult<int>.Error("Failed to update menu item");
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.Error("An error occurred while updating menu item", new List<string> { ex.Message });
            }
        }

        public async Task<ServiceResult<int>> DeleteMenuItemAsync(int id)
        {
            try
            {
                var numberDelete = await _menuDAO.DeleteMenuItem(id);
                if (numberDelete == 0)
                {
                    return ServiceResult<int>.Error("Failed to delete menu item");
                }
                return ServiceResult<int>.Success(numberDelete, "Menu item deleted successfully");

            }
            catch (Exception ex)
            {
                return ServiceResult<int>.Error("An error occurred while deleting menu item", new List<string> { ex.Message });
            }
        }
    }
}
