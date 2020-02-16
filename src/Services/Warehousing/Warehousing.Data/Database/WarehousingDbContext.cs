using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Warehousing.Domain.Product;

namespace Warehousing.Data.Database
{
    public class WarehousingDbContext : DbContext
    {
        private IDbContextTransaction _currentTransaction;

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductHistoryLine> ProductHistoryLines { get; set; }

        public WarehousingDbContext(DbContextOptions<WarehousingDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarehousingDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        //Use transactions if we want to clean use SaveContext() several time in the app
        public async Task BeginTransactionAsync() 
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync().ConfigureAwait(false);

                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
