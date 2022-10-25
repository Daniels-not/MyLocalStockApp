using Microsoft.EntityFrameworkCore;
using MyStockApp.Core.Application.Interfaces.Repositories;
using MyStockApp.Core.Domain.Entities;
using MyStockApp.Infrastucture.Persistence.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStockApp.Infrastructure.Persistence.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationContext _dbContext;

        public ProductRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<Product> GetByIdProductAsync(int id)
        {
            var data = await _dbContext.Set<Product>().Where(a => a.Id == id).Include(a => a.User).Include(a => a.Documents).Include(a => a.Category).ToListAsync();
            return data.First();
        }
    }
}
