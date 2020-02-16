using System;
using System.Threading.Tasks;
using Moq;
using NFluent;
using Warehousing.API.Application.Product.Commands;
using Warehousing.Data.Entities.Product;
using Warehousing.Domain.Product;
using Warehousing.Testhelpers.Asserters;
using Warehousing.Testhelpers.Fakes;
using Xunit;

namespace Warehousing.API.Tests.Application.Product.Commands
{
    public class UpdateProductCommandValidatorTest
    {
        [Fact]
        public async Task GivenNoOtherProductWithSameNameInDb_whenValidated_thenValid()
        {
            //Arrange
            var mockedDao = new Mock<IProductDao>();
            mockedDao.Setup(mock => mock.OtherProductPresentWithName(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(false);

            var validator = new UpdateProductCommandValidator(mockedDao.Object);

            //Act
            await validator.Validate(_updateProductCommand);

            //Assert
            Check.That(validator.IsValid()).IsTrue();
        }

        [Fact]
        public async Task GivenOtherProductWithSameNameInDb_whenValidated_thenValid()
        {
            //Arrange
            var mockedDao = new Mock<IProductDao>();
            mockedDao.Setup(mock => mock.OtherProductPresentWithName(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(true);

            var validator = new UpdateProductCommandValidator(mockedDao.Object);

            //Act
            await validator.Validate(_updateProductCommand);

            //Assert
            Check.That(validator.IsValid()).IsFalse();
            ErrorAsserter.AssertErrors(validator.Errors,
                error1 => error1.HasErrorCode("ProductNameAlreadyReserved").And.HasErrorMessage("The product with this name does already exists"));
        }

        private readonly UpdateProductCommand _updateProductCommand = new UpdateProductCommand(
            ProductFakes.ProductWithAllPropsFilled1().Id,
            "Ariston new",
            "123456789new",
            ProductType.Other,
            "730901",
            999,
            Unit.Piece,
            40001,
            40002,
            25,
            11001,
            51001,
            51002,
            "note new");
    }
}