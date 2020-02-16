using System;
using System.Threading.Tasks;
using NFluent;
using Warehousing.Data.Database;
using Warehousing.Data.Entities.Product;
using Warehousing.Domain.Product;
using Warehousing.Testhelpers;
using Warehousing.Testhelpers.Fakes;
using Xunit;

namespace Warehousing.Data.Tests.Entities.Product
{
    public class ProductRepositoryTest : IntegrationTestBase
    {
        [Fact]
        public void GivenNullAsProduct_whenInserted_thenThrowsException()
        {
            //Arrange
            var repository = new ProductRepository(DbContext);
            Domain.Product.Product product = null;
            
            //Act and Assert
            Check.ThatCode(() => repository.Insert(product))
                .Throws<ArgumentException>()
                .WithMessage("To-be-inserted Product object is null");
        }

        [Fact]
        public async Task GivenProductWithAllPropertiesFilled_whenInserted_thenInsertedSuccesfully()
        {
            //Arrange
            var repository = new ProductRepository(DbContext);

            //Act
            repository.Insert(ProductFakes.ProductWithAllPropsFilled1());
            await DbContext.SaveChangesAsync();

            //Assert
            Check.That(DbContext.Products).CountIs(1);
        }

        [Fact]
        public async Task GivenProductInDb_whenFind_thenReturned()
        {
            //Arrange
            await AddToDbContextAsync(ProductFakes.ProductWithAllPropsFilled1());

            var product2 = ProductFakes.ProductWithAllPropsFilled2();
            await AddToDbContextAsync(product2);

            var repository = new ProductRepository(DbContext);

            //Act
            var product = await repository.FindByNameAsync(product2.Name);

            //Assert
            Check.That(product).IsNotNull();
            Check.That(product.Name).IsEqualTo("Ariston 2");
        }

        [Fact]
        public async Task NoProductInDb_whenFind_thenReturnsNull()
        {
            //Arrange
            var repository = new ProductRepository(DbContext);

            //Act
            var product = await repository.FindByNameAsync(ProductFakes.ProductWithAllPropsFilled2().Name);

            //Assert
            Check.That(product).IsNull();
        }

        [Fact]
        public async Task GivenProductInDb_whenGetById_thenReturned()
        {
            //Arrange
            await AddToDbContextAsync(ProductFakes.ProductWithAllPropsFilled1());

            var product2 = ProductFakes.ProductWithAllPropsFilled2();
            await AddToDbContextAsync(product2);

            var repository = new ProductRepository(DbContext);

            //Act
            var product = await repository.GetAsync(product2.Id);

            //Assert
            Check.That(product).IsNotNull();
            Check.That(product.Name).IsEqualTo("Ariston 2");
        }


        [Fact]
        public void NoProductInDb_whenGetById_thenReturned()
        {
            //Arrange
            var repository = new ProductRepository(DbContext);

            //Act
            Check.ThatAsyncCode(() => repository.GetAsync(ProductFakes.ProductWithAllPropsFilled2().Id))
                .ThrowsAny();
        }

        [Fact]
        public async Task GivenNoProductsInDb_whenGetAll_thenReturnsNoProduct()
        {
            //Arrange
            var repository = new ProductRepository(DbContext);

            //Act
            var products = await repository.GetAllAsync();

            //Assert
            Check.That(products).IsEmpty();
        }

        [Fact]
        public async Task GivenMultipleProductsInDb_whenGetAll_thenEntitiesReturned()
        {
            //Arrange
            await AddToDbContextAsync(ProductFakes.ProductWithAllPropsFilled1());
            await AddToDbContextAsync(ProductFakes.ProductWithAllPropsFilled2());
            var repository = new ProductRepository(DbContext);

            //Act
            var products = await repository.GetAllAsync();
            
            //Assert
            Check.That(products).CountIs(2);
        }

        [Fact]
        public void GivenUpdateWithNull_WhenUpdate_thenReturnsException()
        {
            //Arrange
            var repository = new ProductRepository(DbContext);

            //Act and Assert
            Check.ThatCode(() => repository.Update(null))
                .Throws<ArgumentException>()
                .WithMessage("To-be-updated Product object is null");
        }
    }
}
