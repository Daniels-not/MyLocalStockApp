using Microsoft.AspNetCore.Http;
using MyStockApp.Core.Application.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStockApp.Core.Application.ViewModels.Products
{
    public class SaveProductViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must put the name of the product")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "You must put the price of the product")]
        public double? Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must put the category of the product")]
        public int CategoryId { get; set; }
        public List<CategoryViewModel>? Categories { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File2 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File3 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File4 { get; set; }

        public int UserId { get; set; }

    }
}
