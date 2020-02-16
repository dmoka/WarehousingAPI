using System.Threading.Tasks;
using MediatR;

namespace KaliGasService.Core.Application.CQRS.Notification
{
    public class EventBus : IEventBus
    {
        private readonly IMediator _mediator;

        public EventBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task RaiseEvent<T>(T @event) where T : INotification
        {
            return _mediator.Publish(@event);
        }
    }
}