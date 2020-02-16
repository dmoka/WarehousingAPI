using System;

namespace KaliGasService.Core.Domain
{
    public abstract class DomainEvent : IDomainEvent
    {
        public DateTime OccurredOn { get; }

        protected DomainEvent()
        {
            OccurredOn = DateTime.Now;
        }
    }
}