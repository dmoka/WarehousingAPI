using System;
using System.Data;

namespace KaliGasService.Core.Data.DAO
{
    public class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
    {
        private readonly DatabaseType _databaseType;

        private IDbConnection _connection;

        private readonly Func<IDbConnection> _dbConnectionInitializer;

        public SqlConnectionFactory(Func<IDbConnection> dbConnectionInitializer, DatabaseType databaseType)
        {
            _dbConnectionInitializer = dbConnectionInitializer;
            _databaseType = databaseType;
        }

        public IDbConnection GetOpenConnection()
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                _connection = _dbConnectionInitializer.Invoke();
                _connection.Open();
            }

            return _connection;
        }

        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Dispose();
            }
        }

        public DatabaseType GetDbType()
        {
            return _databaseType;
        }

    }
}
