using System.Data;

namespace KaliGasService.Core.Data.DAO
{
    public enum DatabaseType
    {
        MSSQL,
        SQLite
    }

    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();

        DatabaseType GetDbType();
    }
}
