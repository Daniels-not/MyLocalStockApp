using MyStockApp.Core.Application.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStockApp.Core.Application.Interfaces.Services
{
    public interface ICategoryService : IGenericService<SaveCategoryViewModel, CategoryViewModel>
    {
      
    }
}
