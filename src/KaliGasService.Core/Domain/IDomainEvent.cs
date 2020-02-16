using System;
using MediatR;

namespace KaliGasService.Core.Domain
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}