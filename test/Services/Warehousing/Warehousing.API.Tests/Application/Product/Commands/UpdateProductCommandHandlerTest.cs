using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NFluent;
using Warehousing.API.Application.Product.Commands;
using Warehousing.Data.Database;
using Warehousing.Data.Entities.Product;
using Warehousing.Domain.Product;
using Warehousing.Testhelpers;
using Warehousing.Testhelpers.Asserters;
using Warehousing.Testhelpers.Fakes;
using Xunit;

namespace Warehousing.API.Tests.Application.Product.Commands
{
    public class UpdateProductCommandHandlerTest : IntegrationTestBase
    {

        [Fact]
        public async Task GivenTwoProducts_whenUpdateProductWithAlreadyExistingName_returnsError()
        {
            //Arrange
            await AddToDbContextAsync(ProductFakes.ProductWithAllPropsFilled1());
            await AddToDbContextAsync(ProductFakes.ProductWithAllPropsFilled1WithNewName());

            //Act
            var result = await SendAsync(CreateCommandModifyingAllFields(ProductFakes.ProductWithAllPropsFilled1().Id));

            //Assert
            Check.That(result.IsSuccess).IsFalse();
            ErrorAsserter.AssertErrors(result.Errors,
                error1 => error1.HasErrorCode("ProductNameAlreadyReserved").HasErrorMessage("The product with this name does already exists"));

        }

        
        [Fact]
        public async Task GivenProducts_whenUpdateProduct_thenUpdated()
        {
            //Arrange
            var product1 = ProductFakes.ProductWithAllPropsFilled1();
            await AddToDbContextAsync(product1);
            await AddToDbContextAsync(ProductFakes.ProductWithAllPropsFilled2());

            //Act
            var result = await SendAsync(CreateCommandModifyingAllFields(product1.Id));

            //Assert
            Check.That(result.IsSuccess).IsTrue();

            var productEntityDao = Provider.GetService<IProductDao>();
            var product = await productEntityDao.GetAsync(product1.Id);

            ProductDtoAsserter.AssertThat(product)
                .HasName("Ariston new")
                .HasArticleNumber("123456789new")
                .HasProductType(ProductType.Other)
                .HasCustomTariffNumber("730901")
                .HasQuantity(999)
                .HasUnit(Unit.Piece)
                .HasNetUnitPrice(40001)
                .HasNetValue(40002)
                .HasVat(25)
                .HasVatSum(11001)
                .HasGrossUnitPrice(51001)
                .HasGrossValue(51002)
                .HasNotes("note new");
        }

        private static UpdateProductCommand CreateCommandModifyingAllFields(Guid id)
        {
            return new UpdateProductCommand(
                id,
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
}