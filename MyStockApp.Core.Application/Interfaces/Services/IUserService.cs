using MyStockApp.Core.Application.Interfaces.Services;
using MyStockApp.Core.Application.ViewModels.User;
using System.Threading.Tasks;

namespace MyStockApp.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<SaveUserViewModel, UserViewModel>
    {
        Task<UserViewModel> Login(LoginViewModel vm);
    }
}
