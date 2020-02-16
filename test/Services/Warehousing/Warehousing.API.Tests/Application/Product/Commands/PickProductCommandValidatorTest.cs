using System;
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
    public class PickProductCommandValidatorTest : IntegrationTestBase
    {
        [Fact]
        public async Task GivenProductWithQuantity_whenPickedWithMoreQuantity_thenIsNotValid()
        {
            //Arrange
            var product = ProductFakes.ProductWithAllPropsFilled1();
            await AddToDbContextAsync(product);

            var productEntityDao = Provider.GetService<IProductDao>();
            var validator = new PickProductCommandValidator(productEntityDao);

            //Act
            await validator.Validate(new PickProductCommand(product.Id,product.Name,product.Quantity + 1));

            //Assert
            Check.That(validator.IsValid()).IsFalse();
            ErrorAsserter.AssertErrors(validator.Errors,
                error => error
                    .HasErrorCode("ProductHasNotEnoughQuantity")
                    .HasErrorMessage("The product Ariston has not enough quantity to be picked"));
        }

        [Fact]
        public async Task GivenProductWithQuantity_whenPickedWithSameQuantity_thenValid()
        {
            //Arrange
            var product = ProductFakes.ProductWithAllPropsFilled1();
            await AddToDbContextAsync(product);

            var productEntityDao = Provider.GetService<IProductDao>();
            var validator = new PickProductCommandValidator(productEntityDao);

            //Act
            await validator.Validate(new PickProductCommand(product.Id, product.Name, product.Quantity));

            //Assert
            Check.That(validator.IsValid()).IsTrue();
        }

        [Fact]
        public async Task GivenProductWithQuantity_whenPickedWithLessQuantity_thenValid()
        {
            //Arrange
            var product = ProductFakes.ProductWithAllPropsFilled1();
            await AddToDbContextAsync(product);

            var productEntityDao = Provider.GetService<IProductDao>();
            var validator = new PickProductCommandValidator(productEntityDao);

            //Act
            await validator.Validate(new PickProductCommand(product.Id, product.Name, product.Quantity - 1));

            //Assert
            Check.That(validator.IsValid()).IsTrue();
        }
    }
}