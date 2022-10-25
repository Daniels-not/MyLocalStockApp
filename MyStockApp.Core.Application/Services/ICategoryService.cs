using Microsoft.AspNetCore.Http;
using MyStockApp.Core.Application.Helpers;
using MyStockApp.Core.Application.Interfaces.Repositories;
using MyStockApp.Core.Application.Interfaces.Services;
using MyStockApp.Core.Application.ViewModels.Categories;
using MyStockApp.Core.Application.ViewModels.User;
using MyStockApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStockApp.Core.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccess;
        private readonly UserViewModel userViewModel;

        public CategoryService(ICategoryRepository categoryRepository, IHttpContextAccessor httpContextAccessor)
        {
            _categoryRepository = categoryRepository;
            _httpContextAccess = httpContextAccessor;
            userViewModel = _httpContextAccess.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task Update(SaveCategoryViewModel data)
        {
            Category category = await _categoryRepository.GetByIdAsync(data.Id);
            category.Id = data.Id;
            category.Name = data.Name;
            category.Description = data.Description;

            await _categoryRepository.UpdateAsync(category);
        }

        public async Task<SaveCategoryViewModel> Add(SaveCategoryViewModel data)
        {
            Category category = new();
            category.Name = data.Name;
            category.Description = data.Description;

            category = await _categoryRepository.AddAsync(category);

            SaveCategoryViewModel categorydata = new();

            categorydata.Id = category.Id;
            categorydata.Name = category.Name;
            categorydata.Description = category.Description;

            return categorydata;
        }

        public async Task Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            await _categoryRepository.DeleteAsync(category);
        }

        public async Task<SaveCategoryViewModel> GetByIdSaveViewModel(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            SaveCategoryViewModel data = new();
            data.Id = category.Id;
            data.Name = category.Name;
            data.Description = category.Description;

            return data;
        }

        public async Task<List<CategoryViewModel>> GetAllViewModel()
        {
            var categoryList = await _categoryRepository.GetAllWithIncludeAsync(new List<string> { "Products" });

            return categoryList.Select(category => new CategoryViewModel
            {
                Name = category.Name,
                Description = category.Description,
                Id = category.Id,
                ProductsQuantity = category.Products.Where(product => product.UserId == userViewModel.Id).Count()
            }).ToList();
        }
    }
}
