using System.Threading.Tasks;
using MediatR;

namespace KaliGasService.Core.Application.CQRS.Notification
{
    public interface IEventBus
    {
        Task RaiseEvent<T>(T @event) where T : INotification;
    }
}