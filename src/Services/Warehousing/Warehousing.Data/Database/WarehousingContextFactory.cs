using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Warehousing.Data.Database
{
    public class WarehousingDbContextFactory : IDesignTimeDbContextFactory<WarehousingDbContext>
    {
        //TODO: get the connection string from appsettings
        public WarehousingDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WarehousingDbContext>();
            optionsBuilder.UseSqlite("Data Source=warehousing.db");

            return new WarehousingDbContext(optionsBuilder.Options);
        }
    }
}
