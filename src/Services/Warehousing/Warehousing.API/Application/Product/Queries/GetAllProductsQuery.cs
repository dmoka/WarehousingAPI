using System.Collections.Generic;
using KaliGasService.Core.Application.CQRS;
using Warehousing.Data.Entities.Product;

namespace Warehousing.API.Application.Product.Queries
{
    public class GetAllProductsQuery : Query<Result<IEnumerable<ProductDto>>>
    {
    }
}
