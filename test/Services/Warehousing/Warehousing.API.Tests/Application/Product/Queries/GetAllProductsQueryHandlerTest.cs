using System.Threading.Tasks;
using KaliGasService.Core.Application.CQRS;
using NFluent;
using Warehousing.API.Application.Product.Queries;
using Warehousing.Data.Database;
using Warehousing.Testhelpers;
using Warehousing.Testhelpers.Fakes;
using Xunit;

namespace Warehousing.API.Tests.Application.Product.Queries
{
    public class GetAllProductsQueryHandlerTest : IntegrationTestBase
    {
        [Fact]
        public async Task GivenNoProducts_thenGetAllProductsQuerySend_thenReturnsNoProducts()
        {
            var result = await SendAsync(new GetAllProductsQuery());

            Check.That(result.IsSuccess).IsTrue();
            Check.That(result.Payload).CountIs(0);
        }

        [Fact]
        public async Task GivenSingleProduct_thenGetAllProductsQuerySend_thenReturnsSingleProduct()
        {
            await AddToDbContextAsync(ProductFakes.ProductWithAllPropsFilled1());

            var result = await SendAsync(new GetAllProductsQuery());

            Check.That(result.IsSuccess).IsTrue();
            Check.That(result.Payload).CountIs(1);
        }

        [Fact]
        public async Task GivenMultipleProducts_thenGetAllProductsQuerySend_thenReturnsMultipleProducts()
        {
            await AddToDbContextAsync(ProductFakes.ProductWithAllPropsFilled1(), ProductFakes.ProductWithAllPropsFilled2());

            var result = await SendAsync(new GetAllProductsQuery());

            Check.That(result.IsSuccess).IsTrue();
            Check.That(result.Payload).CountIs(2);
        }
    }
}