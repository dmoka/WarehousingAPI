using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using KaliGasService.Core.Data.DAO;

namespace KaliGasService.Core.Data.Dapper
{
    //TODO: add as stackoverflow answer -> https://stackoverflow.com/questions/43973685/dapper-map-multiple-joins-sql-query
    //TODO: add tests
    public static class DapperExtensions
    {
        public static async Task<T1> QuerySingleAsyncWithJoin<T1, T2>(this IDbConnection dbConnection, Guid id, string sql, Func<T1, ICollection<T2>> childSelector, object param)
            where T1 : IEntityDto
            where T2 : IEntityDto
        {
            var entities1 = new Dictionary<Guid, T1>();
            var entities2 = new Dictionary<Guid, T2>();

            await dbConnection.QueryAsync<T1, T2, T1>(
                sql,
                (c, a) =>
                {
                    if (!entities1.TryGetValue(c.Id, out var entity1))
                    {
                        entities1.Add(c.Id, entity1 = c);
                    }

                    if (!entities2.TryGetValue(a.Id, out var entity2))
                    {
                        entities2.Add(a.Id, entity2 = a);
                        childSelector.Invoke(entity1).Add(entity2);
                    }

                    return entity1;
                },
                param);

            return entities1.Values.First();
        }

        public static async Task<IEnumerable<T1>> QueryAllAsyncWithJoin<T1, T2>(
            this IDbConnection dbConnection,
            string sql,
            Func<T1, ICollection<T2>> childSelector,
            object param = null)
            where T1 : IEntityDto
            where T2 : IEntityDto
        {
            var entities1 = new Dictionary<Guid, T1>();
            var entities2 = new Dictionary<Guid, T2>();

            await dbConnection.QueryAsync<T1, T2, T1>(
                sql,
                (c, a) =>
                {
                    if (!entities1.TryGetValue(c.Id, out var entity1))
                    {
                        entities1.Add(c.Id, entity1 = c);
                    }

                    if (!entities2.TryGetValue(a.Id, out var entity2))
                    {
                        entities2.Add(a.Id, entity2 = a);
                        childSelector.Invoke(entity1).Add(entity2);
                    }

                    return entity1;
                },
                param);

            return entities1.Values;
        }

    }
}
