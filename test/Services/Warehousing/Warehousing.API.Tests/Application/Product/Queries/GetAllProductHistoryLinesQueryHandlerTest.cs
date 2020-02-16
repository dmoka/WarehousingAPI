using System;
using System.Threading.Tasks;
using NFluent;
using Warehousing.API.Application.Product.Queries;
using Warehousing.Testhelpers;
using Warehousing.Testhelpers.Fakes;
using Xunit;

namespace Warehousing.API.Tests.Application.Product.Queries
{
    public class GetAllProductHistoryLinesQueryHandlerTest : IntegrationTestBase
    {
        [Fact]
        public async Task GivenNoHistoryLines_thenGetAllProductHistoryLines_thenReturnsNoLines()
        {
            var result = await SendAsync(new GetAllProductHistoryLinesQuery(Guid.Empty));

            Check.That(result.IsSuccess).IsTrue();
            Check.That(result.Payload).CountIs(0);
        }

        [Fact]
        public async Task GivenSingleHistoryLines_thenGetAllProductHistoryLines_thenReturnsSingleLine()
        {
            var historyLine = ProductHistoryLineFakes.ProductHistoryLineWithPick();
            await AddToDbContextAsync(historyLine);

            var result = await SendAsync(new GetAllProductHistoryLinesQuery(historyLine.ProductId));

            Check.That(result.IsSuccess).IsTrue();
            Check.That(result.Payload).CountIs(1);
        }

        [Fact]
        public async Task GivenMultipleHistoryLines_thenGetAllProductHistoryLines_thenReturnsAll()
        {
            var productId = Guid.NewGuid();
            await AddToDbContextAsync(
                ProductHistoryLineFakes.ProductHistoryLineWithPick(productId), 
                ProductHistoryLineFakes.ProductHistoryLineWithUnpick(productId));

            var result = await SendAsync(new GetAllProductHistoryLinesQuery(productId));

            Check.That(result.IsSuccess).IsTrue();
            Check.That(result.Payload).CountIs(2);
        }
    }
}