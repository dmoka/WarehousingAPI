using System;
using System.Collections.Generic;
using System.Text;
using KaliGasService.Core.Domain;

namespace Warehousing.Domain.Product
{
    public class ProductHistoryLine : Entity
    {
        public Guid ProductId { get; }

        public int DeltaQuantity { get; }

        public ProductHistoryType Type { get; }

        public DateTime OccurredOn { get; }

        public ProductHistoryLine(Guid productId, int deltaQuantity, ProductHistoryType type, DateTime occurredOn)
        {
            ProductId = productId;
            DeltaQuantity = deltaQuantity;
            Type = type;
            OccurredOn = occurredOn;
        }
    }
}
