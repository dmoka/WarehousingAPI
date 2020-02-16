using System;

namespace KaliGasService.Core.Data.DAO
{
    public class TableNameGetter
    {
        public static string Get(DatabaseType databaseType, string schemaName, string tableName)
        {
            switch (databaseType)
            {
                case DatabaseType.MSSQL:
                    return $"{schemaName}.{tableName}";
                case DatabaseType.SQLite:
                    return tableName;
                default:
                    throw new NotSupportedException($"The database type {databaseType} is not supported");
            }
        }
    }
}
