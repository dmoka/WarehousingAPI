using System;
using System.Threading.Tasks;
using NFluent;
using Warehousing.Data.Entities.Product;
using Warehousing.Domain.Product;
using Warehousing.Testhelpers;
using Warehousing.Testhelpers.Fakes;
using Xunit;
namespace Warehousing.Data.Tests.Entities.Product
{
    public class ProductHistoryLineRepositoryTest : IntegrationTestBase
    {

        [Fact]
        public void GivenNullAsHistoryLine_whenInserted_thenThrowsException()
        {
            //Arrange
            var repository = new ProductHistoryLineRepository(DbContext);
            ProductHistoryLine historyLine = null;

            //Act and Assert
            Check.ThatCode(() => repository.Insert(historyLine))
                .Throws<ArgumentException>()
                .WithMessage("To-be-inserted ProductHistoryLine object is null");
        }

        [Fact]
        public async Task GivenHistoryLine_whenInserted_thenInsertedSuccesfully()
        {
            //Arrange
            var repository = new ProductHistoryLineRepository(DbContext);

            //Act
            repository.Insert(ProductHistoryLineFakes.ProductHistoryLineWithPick());
            await DbContext.SaveChangesAsync();

            //Assert
            Check.That(DbContext.ProductHistoryLines).CountIs(1);
        }
    }
}