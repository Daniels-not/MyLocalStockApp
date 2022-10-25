using Microsoft.EntityFrameworkCore;
using MyStockApp.Core.Application.Interfaces.Repositories;
using MyStockApp.Core.Domain.Entities;
using MyStockApp.Infrastucture.Persistence.Contexts;
using MyStockApp.Core.Application.Interfaces.Repositories;
using MyStockApp.Core.Domain.Entities;
using MyStockApp.Infrastucture.Persistence.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStockApp.Infrastructure.Persistence.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationContext _dbContext;

        public CategoryRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


    }
}
