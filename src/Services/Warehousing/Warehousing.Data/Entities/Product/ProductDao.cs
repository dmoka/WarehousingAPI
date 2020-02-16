using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using KaliGasService.Core.Data.DAO;
using Warehousing.Data.Entities.Product.TableConfigs;

namespace Warehousing.Data.Entities.Product
{
    public class ProductDao : AbstractDao<ProductDto>, IProductDao
    {
        public ProductDao(ISqlConnectionFactory sqlConnectionFactory) : base(sqlConnectionFactory, ProductConfiguration.SchemaName, ProductConfiguration.TableName)
        {
        }

        public async Task<bool> Exists(string name)
        {
            var sql = $"SELECT 1 " +
                            $"WHERE EXISTS (SELECT 1 FROM {TableName()} WHERE Name = @name)";

            var exists = (await Connection().QueryAsync<object>(sql, new { name })).Any();

            return exists;
        }

        public async Task<bool> OtherProductPresentWithName(Guid id, string name)
        {

            var sql = $"SELECT 1 " +
                            $"WHERE EXISTS (SELECT 1 FROM {TableName()} WHERE Id <> @id AND Name = @name)";

            var exists = (await Connection().QueryAsync<object>(sql, new { id , name })).Any();

            return exists;
        }

        public async Task<bool> HasMinimumQuantity(Guid id, int quantity)
        {
            var sql = $"SELECT 1 " +
                      $"WHERE EXISTS (SELECT 1 FROM {TableName()} WHERE Id = @id AND Quantity >= @quantity)";

            var exists = (await Connection().QueryAsync<object>(sql, new { id, quantity })).Any();

            return exists;
        }
    }
}
