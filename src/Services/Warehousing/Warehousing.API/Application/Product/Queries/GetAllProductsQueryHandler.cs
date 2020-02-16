using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KaliGasService.Core.Application.CQRS;
using Warehousing.Data.Entities.Product;

namespace Warehousing.API.Application.Product.Queries
{
    public class GetAllProductsQueryHandler : AbstractQueryHandler<GetAllProductsQuery, Result<IEnumerable<ProductDto>>, IEnumerable<ProductDto>>
    {
        private readonly IProductDao _productEntityDao;

        public GetAllProductsQueryHandler(IProductDao productEntityDao)
        {
            _productEntityDao = productEntityDao;
        }

        public override async Task<Result<IEnumerable<ProductDto>>> HandleQuery(GetAllProductsQuery request)
        {
            var products = await _productEntityDao.GetAllAsync();

            return Result<IEnumerable<ProductDto>>.Success(products);
        }

    }
}
