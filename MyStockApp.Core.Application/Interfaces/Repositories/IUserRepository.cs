using MyStockApp.Core.Application.Interfaces.Repositories;
using MyStockApp.Core.Application.Interfaces.Services;
using MyStockApp.Core.Application.ViewModels.User;
using MyStockApp.Core.Domain.Entities;
using MyStockApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStockApp.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> LoginAsync(LoginViewModel logindata);
    }
}
