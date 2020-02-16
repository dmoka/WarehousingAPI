using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NFluent;
using Warehousing.API.Application.Product.Commands;
using Warehousing.Data.Entities.Product;
using Warehousing.Testhelpers;
using Warehousing.Testhelpers.Fakes;
using Xunit;

namespace Warehousing.API.Tests.Application.Product.Commands
{
    public class UnpickProductCommandHandlerTest : IntegrationTestBase
    {
        [Fact]
        public async Task GivenProduct_whenPicked_thenProductHasBeenPicked()
        {
            //Arrange
            var product = ProductFakes.ProductWithAllPropsFilled1(5);
            await AddToDbContextAsync(product);

            //Act
            var result = await SendAsync(new UnpickProductCommand(product.Id, product.Name, 2));

            //Assert
            Check.That(result.IsSuccess).IsTrue();

            var productDao = Provider.GetService<IProductDao>();
            var productFromDb = await productDao.GetAsync(product.Id);
            Check.That(productFromDb.Quantity).IsEqualTo(7);
        }

        [Fact]
        public async Task GivenProduct_whenPicked_thenHistoryLineIsInserted()
        {
            //Arrange
            var product = ProductFakes.ProductWithAllPropsFilled1(5);
            await AddToDbContextAsync(product);

            //Act
            var result = await SendAsync(new UnpickProductCommand(product.Id, product.Name, 2));

            //Assert
            Check.That(result.IsSuccess).IsTrue();

            var historyLineDao = Provider.GetService<IProductHistoryLineDao>();
            var historyLines = await historyLineDao.GetAllAsync();
            Check.That(historyLines).CountIs(1);
        }
    }
}