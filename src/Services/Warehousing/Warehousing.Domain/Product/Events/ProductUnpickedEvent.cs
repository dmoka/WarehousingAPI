using System;
using KaliGasService.Core.Domain;

namespace Warehousing.Domain.Product.Events
{
    public class ProductUnpickedEvent : DomainEvent
    {
        public Guid ProductId { get; }
        public int Quantity { get; }

        public ProductUnpickedEvent(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}