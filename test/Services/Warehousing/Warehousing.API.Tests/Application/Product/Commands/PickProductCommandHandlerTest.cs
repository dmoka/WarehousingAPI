using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NFluent;
using Warehousing.API.Application.Product.Commands;
using Warehousing.Data.Entities.Product;
using Warehousing.Testhelpers;
using Warehousing.Testhelpers.Asserters;
using Warehousing.Testhelpers.Fakes;
using Xunit;

namespace Warehousing.API.Tests.Application.Product.Commands
{
    public class PickProductCommandHandlerTest : IntegrationTestBase
    {
        [Fact]
        public async Task GivenProductWithNoQuantity_whenPickedWithSomeQuantity_thenReturnsError()
        {
            //Arrange
            var product = ProductFakes.ProductWithAllPropsFilled1(0);
            await AddToDbContextAsync(product);

            //Act
            var result = await SendAsync(new PickProductCommand(product.Id, product.Name, 2));

            //Assert
            Check.That(result.IsSuccess).IsFalse();
            ErrorAsserter.AssertErrors(result.Errors,
                error1 => error1.HasErrorCode("ProductHasNotEnoughQuantity").HasErrorMessage("The product Ariston has not enough quantity to be picked"));
        }

        [Fact]
        public async Task GivenProduct_whenPicked_thenProductHasBeenPicked()
        {
            //Arrange
            var product = ProductFakes.ProductWithAllPropsFilled1(5);
            await AddToDbContextAsync(product);

            //Act
            var result = await SendAsync(new PickProductCommand(product.Id, product.Name, 2));

            //Assert
            Check.That(result.IsSuccess).IsTrue();

            var productDao = Provider.GetService<IProductDao>();
            var productFromDb = await productDao.GetAsync(product.Id);
            Check.That(productFromDb.Quantity).IsEqualTo(3);
        }

        [Fact]
        public async Task GivenProduct_whenPicked_thenHistoryLineCreated()
        {
            //Arrange
            var product = ProductFakes.ProductWithAllPropsFilled1(5);
            await AddToDbContextAsync(product);

            //Act
            var result = await SendAsync(new PickProductCommand(product.Id, product.Name, 2));

            //Assert
            Check.That(result.IsSuccess).IsTrue();

            var historyLineDao = Provider.GetService<IProductHistoryLineDao>();
            var historyLines = await historyLineDao.GetAllAsync();
            Check.That(historyLines).CountIs(1);

        }
    }
}