using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KaliGasService.Core.Data.DAO
{
    public interface IAbstractDao<T>
    {
        Task<T> GetAsync(Guid id);

        Task<IEnumerable<T>> GetAllAsync();
    }
}