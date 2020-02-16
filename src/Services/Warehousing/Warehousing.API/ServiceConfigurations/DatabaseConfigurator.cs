using KaliGasService.Core.Data.DAO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Warehousing.Data.Database;

namespace Warehousing.API.ServiceConfigurations
{
    public static class DatabaseConfigurator
    {
        public static void ConfigureDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<WarehousingDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            services.AddScoped<ISqlConnectionFactory>(_ => new SqlConnectionFactory(() => new SqliteConnection(connectionString), DatabaseType.SQLite));
        }
    }
}
