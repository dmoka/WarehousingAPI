using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KaliGasService.Core.Domain;

namespace Warehousing.Domain.Product.Events
{
    public class ProductUnpickedEventHandler : IDomainEventHandler<ProductUnpickedEvent>
    {
        private readonly IProductHistoryLineRepository _productHistoryLineRepository;

        public ProductUnpickedEventHandler(IProductHistoryLineRepository productHistoryLineRepository)
        {
            _productHistoryLineRepository = productHistoryLineRepository;
        }

        public Task Handle(ProductUnpickedEvent productPickedEvent, CancellationToken cancellationToken = default)
        {
            _productHistoryLineRepository.Insert(
                new ProductHistoryLine(
                    productPickedEvent.ProductId, 
                    productPickedEvent.Quantity,
                    ProductHistoryType.Unpick,
                    DateTime.Now));

            return Task.CompletedTask;
        }
    }
}
