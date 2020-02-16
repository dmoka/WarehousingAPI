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
    public class CreateProductCommandValidatorTest
    {
        [Fact]
        public async Task GivenNoProductExistWithName_whenValidated_thenValid()
        {
            //Arrange
            var mockedDao = new Mock<IProductDao>();
            mockedDao.Setup(mock => mock.Exists( It.IsAny<string>())).ReturnsAsync(false);

            var validator = new CreateProductCommandValidator(mockedDao.Object);

            //Act
            await validator.Validate(_createProductCommand);

            //Assert
            Check.That(validator.IsValid()).IsTrue();
        }

        [Fact]
        public async Task GivenProductExistWithName_whenValidated_thenInvalid()
        {
            //Arrange
            var mockedDao = new Mock<IProductDao>();
            mockedDao.Setup(mock => mock.Exists(It.IsAny<string>())).ReturnsAsync(true);

            var validator = new CreateProductCommandValidator(mockedDao.Object);

            //Act
            await validator.Validate(_createProductCommand);

            //Assert
            Check.That(validator.IsValid()).IsFalse();
            ErrorAsserter.AssertErrors(validator.Errors,
                error1 => error1.HasErrorCode("ProductNameAlreadyReserved").And.HasErrorMessage("The product with this name does already exists"));
        }

        private readonly CreateProductCommand _createProductCommand = new CreateProductCommand("Ariston", "12345678910", ProductType.FlueGasDrainage, "989765", 5, Unit.Piece, 40000, 50000, 25, 10000, 50000, 62500, "Megjegyzes");

    }
}