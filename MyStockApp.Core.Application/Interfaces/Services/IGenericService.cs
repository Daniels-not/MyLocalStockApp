using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStockApp.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel>
           where SaveViewModel : class
           where ViewModel : class
    {
        Task Update(SaveViewModel vm);

        Task<SaveViewModel> Add(SaveViewModel vm);

        Task Delete(int id);

        Task<SaveViewModel> GetByIdSaveViewModel(int id);

        Task<List<ViewModel>> GetAllViewModel();
    }
}
