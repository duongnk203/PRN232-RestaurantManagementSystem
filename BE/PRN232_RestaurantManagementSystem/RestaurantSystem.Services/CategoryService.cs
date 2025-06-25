using RestaurantSystem.DataAccess;
using RestaurantSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services
{
    public interface ICategoryService
    {
        Task<ServiceResult<List<CategoryModel>>> GetCategoriesAsync();
    }
    public class CategoryService : ICategoryService
    {
        private readonly CategoryDAO categoryDAO;
        public CategoryService(CategoryDAO categoryDAO)
        {
            this.categoryDAO = categoryDAO;
        }
        public async Task<ServiceResult<List<CategoryModel>>> GetCategoriesAsync()
        {
            try
            {
                var categories = await categoryDAO.GetCategoryModelAsync();
                if (categories == null || !categories.Any())
                {
                    return ServiceResult<List<CategoryModel>>.NotFound("No categories found");
                }
                return ServiceResult<List<CategoryModel>>.Success(categories, "Categories retrieved successfully");
            }
            catch (Exception ex)
            {
                return ServiceResult<List<CategoryModel>>.Error("An error occurred while retrieving categories", new List<string> { ex.Message });
            }
        }
    }
}
