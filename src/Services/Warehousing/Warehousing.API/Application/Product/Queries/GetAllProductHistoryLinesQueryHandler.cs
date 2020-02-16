using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KaliGasService.Core.Application.CQRS;
using Warehousing.Data.Entities.Product;

namespace Warehousing.API.Application.Product.Queries
{
    public class GetAllProductHistoryLinesQueryHandler : AbstractQueryHandler<GetAllProductHistoryLinesQuery, Result<IEnumerable<ProductHistoryLineDto>>, IEnumerable<ProductHistoryLineDto>>
    {
        private readonly IProductHistoryLineDao _productHistoryLineDao;

        public GetAllProductHistoryLinesQueryHandler(IProductHistoryLineDao productHistoryLineDao)
        {
            _productHistoryLineDao = productHistoryLineDao;
        }

        public override async Task<Result<IEnumerable<ProductHistoryLineDto>>> HandleQuery(GetAllProductHistoryLinesQuery request)
        {
            var historyLines = await _productHistoryLineDao.GetAllAsync(request.ProductId);

            return Result<IEnumerable<ProductHistoryLineDto>>.Success(historyLines);
        }
        
    }
}
