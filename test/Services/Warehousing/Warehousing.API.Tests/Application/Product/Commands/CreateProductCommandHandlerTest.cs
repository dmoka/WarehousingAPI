using System.Linq;
using System.Threading.Tasks;
using NFluent;
using Warehousing.API.Application.Product.Commands;
using Warehousing.Data.Database;
using Warehousing.Domain.Product;
using Warehousing.Testhelpers;
using Warehousing.Testhelpers.Asserters;
using Xunit;

namespace Warehousing.API.Tests.Application.Product.Commands
{
    public class CreateProductCommandHandlerTest : IntegrationTestBase
    {
        [Fact]
        public async Task GivenCreateProductCommandRequest_whenSent_thenNewProductIsCreated()
        {
            //Arrange
            var createProductCommand = new CreateProductCommand("Ariston", "12345678910", ProductType.FlueGasDrainage, "989765", 5,Unit.Piece, 40000, 50000,25,10000,50000, 62500, "Megjegyzes");

            //Act
            var result = await SendAsync(createProductCommand);

            //Assert
            Check.That(result.IsSuccess).IsTrue();
            Check.That(DbContext.Products).CountIs(1);

            var product = DbContext.Products.Single();
            ProductAsserter.AssertThat(product)
                .HasName("Ariston")
                .HasArticleNumber("12345678910")
                .HasProductType(ProductType.FlueGasDrainage)
                .HasCustomTariffNumber("989765")
                .HasQuantity(5)
                .HasUnit(Unit.Piece)
                .HasNetUnitPrice(40000)
                .HasNetValue(50000)
                .HasVat(25)
                .HasVatSum(10000)
                .HasGrossUnitPrice(50000)
                .HasGrossValue(62500)
                .HasNotes("Megjegyzes");
        }
    }
}