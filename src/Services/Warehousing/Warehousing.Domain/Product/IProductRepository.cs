using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehousing.Domain.Product
{

    public interface IProductRepository 
    {
        Task<Product> GetAsync(Guid id); 

        Task<Product> FindByNameAsync(string name); 

        Task<List<Product>> GetAllAsync();

        Product Insert(Product product);

        void Update(Product product);
    }
}
