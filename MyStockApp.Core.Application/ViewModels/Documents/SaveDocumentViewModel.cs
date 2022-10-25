using Microsoft.AspNetCore.Http;
using MyStockApp.Core.Application.ViewModels.Categories;
using MyStockApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStockApp.Core.Application.ViewModels.Documents
{
    public class SaveDocumentViewModel
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public int ProductId { get; set; }
    }
}
