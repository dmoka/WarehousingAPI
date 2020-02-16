using System;
using MediatR;

namespace KaliGasService.Core.Application.CQRS.Notification
{
    public class DomainNotification : INotification
    {
        public Guid DomainNotificationId { get; }
        public string Value { get; }

        public DomainNotification(string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Value = value;
        }
    }
}
