using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KaliGasService.Core.Domain;
using Warehousing.Data.Database;

namespace Warehousing.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly WarehousingDbContext _ordersContext;

        public UnitOfWork(IDomainEventDispatcher domainEventDispatcher, WarehousingDbContext ordersContext)
        {
            _domainEventDispatcher = domainEventDispatcher;
            _ordersContext = ordersContext;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _domainEventDispatcher.DispatchEventsAsync();
            return await _ordersContext.SaveChangesAsync(cancellationToken);
        }
    }
}
