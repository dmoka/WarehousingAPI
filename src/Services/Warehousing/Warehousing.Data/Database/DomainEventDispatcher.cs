using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaliGasService.Core.Domain;
using MediatR;

namespace Warehousing.Data.Database
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;
        private readonly WarehousingDbContext _dbContext;

        public DomainEventDispatcher(IMediator mediator, WarehousingDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        public async Task DispatchEventsAsync()
        {
            var domainEvents = GetDomainEventEntities();
            foreach (var entity in domainEvents)
            {
                var events = entity.Events.ToArray();
                entity.ClearEvents();

                await SendAllEventsViaMediatR(events);
            }
        }

        private IEnumerable<Entity> GetDomainEventEntities()
        {
            var entityEntries = _dbContext.ChangeTracker.Entries<Entity>();
            return entityEntries
                .Select(po => po.Entity)
                .Where(po => po.HasEvents())
                .ToArray();
        }

        private async Task SendAllEventsViaMediatR(IDomainEvent[] events)
        {
            foreach (var @event in events)
            {
                await _mediator.Publish(@event);
            }
        }

    }
}
