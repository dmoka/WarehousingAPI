using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaliGasService.Core.Application.CQRS;
using Warehousing.Data.Entities.Product;

namespace Warehousing.API.Application.Product.Queries
{
    public class GetAllProductHistoryLinesQuery : Query<Result<IEnumerable<ProductHistoryLineDto>>>
    {
        public Guid ProductId { get; }

        public GetAllProductHistoryLinesQuery(Guid productId)
        {
            ProductId = productId;
        }
    }
}
