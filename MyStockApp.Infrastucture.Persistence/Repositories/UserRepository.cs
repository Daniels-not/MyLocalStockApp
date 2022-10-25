using Microsoft.EntityFrameworkCore;
using MyStockApp.Core.Application.Helpers;
using MyStockApp.Core.Application.Interfaces.Repositories;
using MyStockApp.Core.Application.ViewModels.User;
using MyStockApp.Core.Domain.Entities;
using MyStockApp.Infrastucture.Persistence.Contexts;
using System.Threading.Tasks;
using MyStockApp.Infrastructure.Persistence.Repository;

namespace MyStockApp.Infrastucture.Persistence.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _dbContext;

        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<User> AddAsync(User entity)
        {
            entity.Password = PasswordHasher.ComputeSha256Hash(entity.Password);
            await base.AddAsync(entity);
            return entity;
        }

        public async Task<User> LoginAsync(LoginViewModel loginData)
        {
            string passwordEncrypt = PasswordHasher.ComputeSha256Hash(loginData.Password);
            User user = await _dbContext.Set<User>().FirstOrDefaultAsync(user=> user.Username == loginData.Username && user.Password == passwordEncrypt);
            return user;
        }

    }
}
