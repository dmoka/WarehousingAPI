using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Warehousing.Data.Database;
using Warehousing.Domain.Product;

namespace Warehousing.Data.Entities.Product
{
    public class ProductHistoryLineRepository : IProductHistoryLineRepository
    {
        private readonly DbSet<ProductHistoryLine> _entities;

        public ProductHistoryLineRepository(WarehousingDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _entities = dbContext.Set<ProductHistoryLine>();
        }

        public void Insert(ProductHistoryLine product)
        {
            if (product == null)
            {
                throw new ArgumentException("To-be-inserted ProductHistoryLine object is null");
            }

            _entities.Add(product);
        }
    }
}
