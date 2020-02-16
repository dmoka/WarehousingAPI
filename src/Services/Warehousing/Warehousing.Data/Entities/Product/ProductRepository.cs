using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warehousing.Data.Database;
using Warehousing.Domain.Product;

namespace Warehousing.Data.Entities.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbSet<Domain.Product.Product> _entities;
        private readonly WarehousingDbContext _dbContext;
        public ProductRepository(WarehousingDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
            _entities = dbContext.Set<Domain.Product.Product>();
        }

        public Task<Domain.Product.Product> GetAsync(Guid id)
        {
            return _entities.SingleAsync(p => p.Id == id);
        }


        public Task<Domain.Product.Product> FindByNameAsync(string name)
        {
            return _entities.Where(p => p.Name == name).SingleOrDefaultAsync();
        }

        public Task<List<Domain.Product.Product>> GetAllAsync()
        {
            return _entities.ToListAsync();
        }

        public Domain.Product.Product Insert(Domain.Product.Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("To-be-inserted Product object is null");
            }

            var newProduct = _entities.Add(entity);

            return newProduct.Entity;
        }

        public void Update(Domain.Product.Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("To-be-updated Product object is null");
            }

            var product = _entities.First(g => g.Id == entity.Id);

            _dbContext.Entry(product).CurrentValues.SetValues(entity);
        }
    }
}
