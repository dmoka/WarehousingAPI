using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;

namespace KaliGasService.Core.Data.Dapper
{
    public class MySqlGuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override void SetValue(IDbDataParameter parameter, Guid guid)
        {
            parameter.Value = guid.ToString();
        }

        public override Guid Parse(object value)
        {
            return new Guid((string)value);
        }
    }
}
