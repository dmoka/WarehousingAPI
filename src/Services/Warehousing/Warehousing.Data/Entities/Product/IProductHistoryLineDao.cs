using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KaliGasService.Core.Data.DAO;

namespace Warehousing.Data.Entities.Product
{
    public interface IProductHistoryLineDao : IAbstractDao<ProductHistoryLineDto>
    {
        Task<IEnumerable<ProductHistoryLineDto>> GetAllAsync(Guid productId);
    }
}