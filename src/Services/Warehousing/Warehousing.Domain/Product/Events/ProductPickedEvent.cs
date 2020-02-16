using System;
using KaliGasService.Core.Domain;

namespace Warehousing.Domain.Product.Events
{
    public class ProductPickedEvent : DomainEvent
    {
        public Guid ProductId { get; }
        public int Quantity { get; }

        public ProductPickedEvent(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}