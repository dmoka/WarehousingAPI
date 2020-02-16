using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NFluent;
using Warehousing.Data.Entities.Product;
using Warehousing.Domain.Product;
using Warehousing.Testhelpers;
using Warehousing.Testhelpers.Fakes;
using Xunit;

namespace Warehousing.Data.Tests.Entities.Product
{
    public class ProductHistoryLineDaoTest : IntegrationTestBase
    {
        [Fact]
        public async Task GivenNoHistoryLinesInDb_whenGetAll_thenReturnsEmptyCollection()
        {
            //Act
            var historyLines = await GetAllHistoryLines(Guid.NewGuid());

            //Assert
            Check.That(historyLines).IsEmpty();
        }

        [Fact]
        public async Task GivenSingleHistoryLineInDb_whenGetAll_thenReturnsSingle()
        {
            //Arrange
            var historyLine = ProductHistoryLineFakes.ProductHistoryLineWithPick();
            await AddToDbContextAsync(historyLine);

            //Act
            var historyLines = await GetAllHistoryLines(historyLine.ProductId);


            //Assert
            Check.That(historyLines).CountIs(1);
        }

        [Fact]
        public async Task GivenMultipleHistoryLinesInDb_whenGetAll_thenReturnsSingle()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var historyLine1 = ProductHistoryLineFakes.ProductHistoryLineWithPick(productId);
            var historyLine2 = ProductHistoryLineFakes.ProductHistoryLineWithUnpick(productId);
            await AddToDbContextAsync(historyLine1, historyLine2);

            //Act
            var historyLines = await GetAllHistoryLines(productId);

            //Assert
            Check.That(historyLines).CountIs(2);
        }

        private async Task<IEnumerable<ProductHistoryLineDto>> GetAllHistoryLines(Guid productId)
        {
            var historyLineDao = Provider.GetRequiredService<IProductHistoryLineDao>();
            return await historyLineDao.GetAllAsync(productId);
        }
    }
}