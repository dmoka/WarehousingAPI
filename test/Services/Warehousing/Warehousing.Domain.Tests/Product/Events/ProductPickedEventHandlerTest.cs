using System;
using Moq;
using Warehousing.Domain.Product;
using Warehousing.Domain.Product.Events;
using Xunit;

namespace Warehousing.Domain.Tests.Product.Events
{
    public class ProductPickedEventHandlerTest
    {
        [Fact]
        public void GivenProductPickedEvent_whenHandle_thenProductHistoryLineIsInserted()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var mockedRepo = new Mock<IProductHistoryLineRepository>();
            var eventHandler = new ProductPickedEventHandler(mockedRepo.Object);

            //Act
            var result = eventHandler.Handle(new ProductPickedEvent(productId, 3));

            //Assert
            mockedRepo.Verify(mock => mock.Insert(It.Is<ProductHistoryLine>(line =>
                HistoryLineChecker(line, productId, 3, ProductHistoryType.Pick))));
        }

        private static bool HistoryLineChecker(ProductHistoryLine line, Guid productId, int deltaQuantity, ProductHistoryType type)
        {
            return line.ProductId == productId && line.DeltaQuantity == deltaQuantity && line.Type == type;
        }
    }
}