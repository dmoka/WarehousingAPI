using System;
using System.Threading.Tasks;
using KaliGasService.Core.Data.DAO;

namespace Warehousing.Data.Entities.Product
{
    public interface IProductDao : IAbstractDao<ProductDto>
    {
        Task<bool> OtherProductPresentWithName(Guid id, string name);

        Task<bool> Exists(string name);

        Task<bool> HasMinimumQuantity(Guid id, int quantity);
    }
}