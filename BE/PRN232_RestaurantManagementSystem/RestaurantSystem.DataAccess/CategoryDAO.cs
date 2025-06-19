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
    public class CategoryDAO
    {
        private readonly AnJiiDbContext _context;
        public CategoryDAO(AnJiiDbContext context)
        {
            _context = context;
        }
        public async Task<List<CategoryModel>> GetCategoryModelAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryModel
                {
                    CategoryId = c.CategoryID,
                    CategoryName = c.CategoryName,
                })
                .ToListAsync();
        }
    }
}
