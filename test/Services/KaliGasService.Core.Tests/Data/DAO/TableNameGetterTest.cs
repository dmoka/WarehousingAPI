using KaliGasService.Core.Data.DAO;
using KaliGasService.TestHelpers.Extensions;
using NFluent;
using Xunit;

namespace KaliGasService.Core.Tests.Data.DAO
{
    public class TableNameGetterTest
    {
        [Fact]
        public void GivenSqlLiteDb_whenGetTableName_thenNoSchemaNameIsPrefixedSinceSqlLiteDoesNotSupportSchemas()
        {
            var tableName = TableNameGetter.Get(DatabaseType.SQLite, "schemaName", "table_name");

            Check.That(tableName).IsEqualToValue("table_name");
        }

        [Fact]
        public void GivenMSSQLDb_whenGetTableName_thenSchemaNameIsPrefixed()
        {
            var tableName = TableNameGetter.Get(DatabaseType.MSSQL, "schemaName", "table_name");

            Check.That(tableName).IsEqualToValue("schemaName.table_name");
        }
    }
}