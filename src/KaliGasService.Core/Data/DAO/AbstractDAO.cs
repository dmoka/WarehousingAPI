using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace KaliGasService.Core.Data.DAO
{
    public abstract class AbstractDao<T> : IAbstractDao<T> where T : IEntityDto
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly string _schemaName;
        private readonly string _tableName;

        protected AbstractDao(ISqlConnectionFactory sqlConnectionFactory, string schemaName, string tableName)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _schemaName = schemaName;
            _tableName = tableName;
        }

        public virtual async Task<T> GetAsync(Guid id)
        {
            var sql = "SELECT * " +
                            "FROM " + $"{TableName()} " +
                            "WHERE Id = @id";

            return await Connection().QuerySingleOrDefaultAsync<T>(sql, new {id });
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var sql = $"SELECT * FROM {TableName()}";

            return await Connection().QueryAsync<T>(sql);
        }

        public async Task<dynamic> GetTables()
        {
            var sql = "SELECT name FROM sqlite_master WHERE type = 'table'";
            
            return await Connection().QueryAsync(sql);
        }

        public string TableName() => GetTableName(_sqlConnectionFactory.GetDbType());
        public string TableName(string tableName) => GetTableName(_sqlConnectionFactory.GetDbType(), tableName);
 
        public IDbConnection Connection() => _sqlConnectionFactory.GetOpenConnection();

        public string GetTableName(DatabaseType dbType) => TableNameGetter.Get(dbType, _schemaName, _tableName);
        public string GetTableName(DatabaseType dbType, string tableName) => TableNameGetter.Get(dbType, _schemaName, tableName);
    }
}
