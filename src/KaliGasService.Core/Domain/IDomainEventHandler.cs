using System.Threading.Tasks;
using MediatR;

namespace KaliGasService.Core.Domain
{
    public interface IDomainEventHandler<in T> : INotificationHandler<T> where T : IDomainEvent
    {
    }
}