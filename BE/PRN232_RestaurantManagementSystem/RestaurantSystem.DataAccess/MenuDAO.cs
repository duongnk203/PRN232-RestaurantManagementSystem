using Microsoft.EntityFrameworkCore;
using RestaurantSystem.BusinessObjects.Models;
using RestaurantSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.DataAccess
{
    public class MenuDAO
    {
        private readonly AnJiiDbContext _context;
        public MenuDAO(AnJiiDbContext context)
        {
            _context = context;
        }

        public async Task<List<MenuDto>> GetMenus()
        {
            var menus = await _context.MenuItems
                .Select(m => new MenuDto
                {
                    MenuItemId = m.MenuItemID,
                    ItemName = m.ItemName,
                    Description = m.Description,
                    Price = m.Price,
                    CategoryId = m.CategoryID,
                    Cost = m.Cost,
                    ImageUrl = m.ImageURL,
                    IsAvailable = m.IsAvailable
                }).ToListAsync();
            return menus;
        }

        public async Task<List<MenuItemModel>> GetMenuBySearch(string? searchMenu, int? categoryId)
        {
            var query = _context.MenuItems.AsQueryable();

            // Lọc món khả dụng
            query = query.Where(m => m.IsAvailable);

            // Nếu có từ khóa tìm kiếm
            if (!string.IsNullOrWhiteSpace(searchMenu))
            {
                string trimmed = searchMenu.Trim();
                query = query.Where(m => m.ItemName.Contains(trimmed));
            }

            // Nếu có lọc theo danh mục
            if (categoryId.HasValue)
            {
                query = query.Where(m => m.CategoryID == categoryId.Value);
            }

            var result = await query.Select(m => new MenuItemModel
            {
                MenuItemId = m.MenuItemID,
                ItemName = m.ItemName,
                Price = m.Price,
                CategoryId = m.CategoryID,
                ImageUrl = m.ImageURL,
            }).ToListAsync();

            return result;
        }


        public async Task<int> CreateMenuItem(CreateMenu menu)
        {
            var newMenu = new MenuItem
            {
                ItemName = menu.ItemName,
                Description = menu.Description,
                Price = menu.Price,
                CategoryID = menu.CategoryId,
                Cost = menu.Cost,
                ImageURL = menu.ImageUrl,
                IsAvailable = menu.IsAvailable,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
            };
            _context.MenuItems.Add(newMenu);
            return await _context.SaveChangesAsync();
        }
        public async Task<bool> IsMenuItemDuplicateAsync(string itemName, int categoryId)
        {
            return await _context.MenuItems
                .AnyAsync(m => m.ItemName.ToLower() == itemName.ToLower() && m.CategoryID == categoryId);
        }
        public async Task<bool> IsMenuItemNameExistAsync(int id, string itemName, int categoryId)
        {
            return await _context.MenuItems.AnyAsync(m =>
                m.MenuItemID != id && m.ItemName.ToLower() == itemName.ToLower() && m.CategoryID == categoryId);
        }
        public async Task<int> UpdateMenuItem(int id, CreateMenu menu)
        {
            var existingMenu = await _context.MenuItems.FindAsync(id);
            if (existingMenu == null)
            {
                return 0; // Not found
            }
            existingMenu.ItemName = menu.ItemName;
            existingMenu.Description = menu.Description;
            existingMenu.Price = menu.Price;
            existingMenu.CategoryID = menu.CategoryId;
            existingMenu.Cost = menu.Cost;
            existingMenu.ImageURL = menu.ImageUrl;
            existingMenu.IsAvailable = menu.IsAvailable;
            existingMenu.UpdatedAt = DateTime.UtcNow;
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteMenuItem(int id)
        {
            var existingMenu = await _context.MenuItems.FindAsync(id);
            if (existingMenu == null)
            {
                return 0; // Not found
            }
            existingMenu.IsAvailable = false; // Soft delete
            existingMenu.UpdatedAt = DateTime.UtcNow;
            _context.MenuItems.Update(existingMenu);
            return await _context.SaveChangesAsync();
        }
    }
}
