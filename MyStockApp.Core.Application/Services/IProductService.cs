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
using MyStockApp.Core.Application.ViewModels.Products;
using MyStockApp.Core.Application.ViewModels.Documents;

namespace MyStockApp.Core.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IHttpContextAccessor _httpContextAccess;
        private readonly UserViewModel userViewModel;

        public ProductService(IProductRepository productRepository, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _httpContextAccess = httpContextAccessor;
            userViewModel = _httpContextAccess.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task Update(SaveProductViewModel data)
        {
            Product product = await _productRepository.GetByIdAsync(data.Id);
            product.Id = data.Id;
            product.Name = data.Name;
            product.Price = data.Price;
            product.ImageUrl = data.ImageUrl;
            product.Description = data.Description;
            product.CategoryId = data.CategoryId;

            await _productRepository.UpdateAsync(product);
        }

        public async Task<SaveProductViewModel> Add(SaveProductViewModel data)
        {
            Product product = new();
            product.Name = data.Name;
            product.Price = data.Price;
            product.ImageUrl = data.ImageUrl;
            product.Description = data.Description;
            product.CategoryId = data.CategoryId;
            product.UserId = userViewModel.Id;

            product = await _productRepository.AddAsync(product);

            SaveProductViewModel productdata = new();

            productdata.Id = product.Id;
            productdata.Name = product.Name;
            productdata.Price = product.Price;
            productdata.ImageUrl = product.ImageUrl;
            productdata.Description = product.Description;
            productdata.CategoryId = product.CategoryId;

            return productdata;
        }

        public async Task Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            await _productRepository.DeleteAsync(product);
        }

        public async Task<SaveProductViewModel> GetByIdSaveViewModel(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            SaveProductViewModel data = new();
            data.Id = product.Id;
            data.Name = product.Name;
            data.Description = product.Description;
            data.CategoryId = product.CategoryId;
            data.Price = product.Price;
            data.ImageUrl = product.ImageUrl;

            return data;
        }

        public async Task<List<ProductViewModel>> GetAllViewModel()
        {
            var productList = await _productRepository.GetAllWithIncludeAsync(new List<string> { "Category" });

            return productList.Where(product => product.UserId == userViewModel.Id).Select(product => new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Id = product.Id,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                CategoryName = product.Category.Name,
                CategoryId = product.Category.Id
            }).ToList();
        }

        public async Task<List<ProductViewModel>> GetAllViewModelWithFilters(FilterProductViewModel filters)
        {
            var productList = await _productRepository.GetAllWithIncludeAsync(new List<string> { "Category" });

            var listViewModels = productList.Where(product => product.UserId != userViewModel.Id).Select(product => new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Id = product.Id,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                CategoryName = product.Category.Name,
                CategoryId = product.Category.Id
            }).ToList();

            if (filters.CategoryId != null)
            {
                List<ProductViewModel> filtered = new List<ProductViewModel>();
                if(filters.CategoryId.Contains(0))
                {
                    return listViewModels;
                }

                foreach(var category in filters.CategoryId)
                {
                    filtered.AddRange(listViewModels.Where(product => product.CategoryId == category));
                }
                return filtered;
            }

            if(filters.Name != null)
            {
                 return listViewModels.Where(product => product.Name.Contains(filters.Name)).ToList();
            }

            return listViewModels;
        }

        public async Task<ProductViewModel> GetProductDetails(int id)
        {
            var data = await _productRepository.GetByIdProductAsync(id);

            ProductViewModel product = new();

            product.Name = data.Name;
            product.Price = data.Price;
            product.ImageUrl = data.ImageUrl;
            product.Description = data.Description;
            product.CategoryId = data.CategoryId;
            product.CategoryName = data.Category.Name;

            List<SaveDocumentViewModel> documents = new List<SaveDocumentViewModel>();

            foreach(var doc in data.Documents)
            {
                documents.Add(new SaveDocumentViewModel()
                {
                    ProductId = doc.ProductID,
                    ImageUrl = doc.ImageUrl
                });
            }

            product.Documents = documents;
            return product;
        }

    }
}   
