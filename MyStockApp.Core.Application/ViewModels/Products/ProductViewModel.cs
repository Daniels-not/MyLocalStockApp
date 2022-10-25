using MyStockApp.Core.Application.ViewModels.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStockApp.Core.Application.ViewModels.Products
{
    public class ProductViewModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; } = String.Empty;
        public string? Description { get; set; } = String.Empty;
        public string? ImageUrl { get; set; } = String.Empty;
        public double? Price { get; set; } = double.NaN;

        public string? CategoryName { get; set; } = String.Empty;
        public int? CategoryId { get; set; } = Int16.MaxValue;
        public ICollection<SaveDocumentViewModel> Documents { get; set; }
    }
}
