using System;
using KaliGasService.Core.Data.DAO;
using Warehousing.Domain.Product;

namespace Warehousing.Data.Entities.Product
{
    public class ProductHistoryLineDto : IEntityDto
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public int DeltaQuantity { get; set; }

        public ProductHistoryType Type { get; set; }

        public DateTime OccurredOn { get; set; }
    }
}