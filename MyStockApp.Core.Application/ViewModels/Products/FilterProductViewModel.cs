using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStockApp.Core.Application.ViewModels.Products
{
    public class FilterProductViewModel
    {
        public int[]? CategoryId { get; set; }
        public string? Name { get; set; }
    }
}
