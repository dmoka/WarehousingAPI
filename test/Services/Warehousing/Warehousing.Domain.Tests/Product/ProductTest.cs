using NFluent;
using Warehousing.Domain.Product;
using Warehousing.Domain.Product.Events;
using Warehousing.Testhelpers.Asserters;
using Warehousing.Testhelpers.Fakes;
using Xunit;
namespace Warehousing.Domain.Tests.Product
{
    public class ProductTest
    {
        [Fact]
        public void GivenProductWithQuantity_whenPicked_thenQuantityHasBeenSubstracted()
        {
            //Arrange
            var product = ProductFakes.ProductWithAllPropsFilled1();
            var originalQuantity = product.Quantity;

            //Act
            product.Pick(2);

            //Assert
            ProductAsserter.AssertThat(product)
                .HasQuantity(originalQuantity - 2);
        }

        [Fact]
        public void GivenProductWithQuantity_whenPicked_thenRaisedProductPickedEvent()
        {
            //Arrange
            var product = ProductFakes.ProductWithAllPropsFilled1();

            //Act
            product.Pick(2);

            //Assert
            ProductAsserter.AssertThat(product)
                .HasRaisedEvent(new ProductPickedEvent(product.Id, 2));
        }


        [Fact]
        public void GivenProductWithQuantity_whenUnpicked_thenQuantityHasBeenAdded()
        {
            //Arrange
            var product = ProductFakes.ProductWithAllPropsFilled1();
            var originalQuantity = product.Quantity;

            //Act
            product.Unpick(3);

            //Assert
            ProductAsserter.AssertThat(product)
                .HasQuantity(originalQuantity + 3);
        }

        [Fact]
        public void GivenProductWithQuantity_whenPicked_thenRaisedProductUnpickedEvent()
        {
            //Arrange
            var product = ProductFakes.ProductWithAllPropsFilled1();

            //Act
            product.Unpick(2);

            //Assert
            ProductAsserter.AssertThat(product)
                .HasRaisedEvent(new ProductUnpickedEvent(product.Id, 2));
        }
    }
}