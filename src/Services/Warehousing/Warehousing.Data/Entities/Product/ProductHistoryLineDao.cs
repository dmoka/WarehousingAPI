using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using KaliGasService.Core.Data.DAO;
using Warehousing.Data.Entities.Product.TableConfigs;

namespace Warehousing.Data.Entities.Product
{
    public class ProductHistoryLineDao : AbstractDao<ProductHistoryLineDto>, IProductHistoryLineDao
    {
        public ProductHistoryLineDao(ISqlConnectionFactory sqlConnectionFactory) : base(sqlConnectionFactory, ProductHistoryLineConfiguration.SchemaName, ProductHistoryLineConfiguration.TableName)
        {
        }

        public async Task<IEnumerable<ProductHistoryLineDto>> GetAllAsync(Guid productId)
        {
            var sql = "SELECT * " +
                      "FROM " + $"{TableName()} " +
                      "WHERE ProductId = @productId";

            return await Connection().QueryAsync<ProductHistoryLineDto>(sql, new {productId});
        }
    }
}
