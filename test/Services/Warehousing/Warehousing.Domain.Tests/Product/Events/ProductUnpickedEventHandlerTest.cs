using System;
using Moq;
using Warehousing.Domain.Product;
using Warehousing.Domain.Product.Events;
using Xunit;

namespace Warehousing.Domain.Tests.Product.Events
{
    public class ProductUnpickedEventHandlerTest
    {
        [Fact]
        public void GivenProductUnpickedEvent_whenHandle_thenProductHistoryLineIsInserted()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var mockedRepo = new Mock<IProductHistoryLineRepository>();
            var eventHandler = new ProductUnpickedEventHandler(mockedRepo.Object);

            //Act
            var result = eventHandler.Handle(new ProductUnpickedEvent(productId, 3));

            //Assert
            mockedRepo.Verify(mock => mock.Insert(It.Is<ProductHistoryLine>(line => 
                HistoryLineChecker(line, productId, 3, ProductHistoryType.Unpick))));
        }

        private static bool HistoryLineChecker(ProductHistoryLine line, Guid productId, int deltaQuantity, ProductHistoryType type)
        {
            return line.ProductId == productId && line.DeltaQuantity == deltaQuantity && line.Type == type;
        }
    }
}