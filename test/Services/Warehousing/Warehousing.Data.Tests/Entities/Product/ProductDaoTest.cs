using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NFluent;
using Warehousing.Data.Database;
using Warehousing.Data.Entities.Product;
using Warehousing.Domain.Product;
using Warehousing.Testhelpers;
using Warehousing.Testhelpers.Fakes;
using Xunit;

namespace Warehousing.Data.Tests.Entities.Product
{
    /// <summary>
    /// BE AWARE!!! This class tests the basic functionality of AbstractDAO. In case of getting rid of, place the tests to other valid test classes.
    /// </summary>
    public class ProductDaoTest : IntegrationTestBase
    {

        [Fact]
        public async Task GivenProductInDb_whenGetAsync_thenReturned()
        {
            //Arrange
            var product = ProductFakes.ProductWithAllPropsFilled1();
            await AddToDbContextAsync(product);

            //Act
            var productDao = Provider.GetRequiredService<IProductDao>();
            var productFromDb = await productDao.GetAsync(product.Id);

            //Assert
            Check.That(productFromDb).IsNotNull();
            Check.That(productFromDb.Id).IsInstanceOf<Guid>();
            Check.That(productFromDb.Name).IsEqualTo("Ariston");
            Check.That(productFromDb.Type).IsEqualTo(ProductType.GasBoiler);
        }

        [Fact]
        public async Task GivenMultipleProductsInDb_whenGetAllAsync_thenReturnedAll ()
        {
            //Arrange
            var reader = Provider.GetRequiredService<IProductDao>();
            await AddToDbContextAsync(ProductFakes.ProductWithAllPropsFilled1());
            await AddToDbContextAsync(ProductFakes.ProductWithAllPropsFilled2());

            //Act
            var products = await reader.GetAllAsync();

            //Assert
            Check.That(products).CountIs(2);
        }

        [Fact]
        public async Task GivenNoProduct_whenExists_thenReturnFalse()
        {
            //Arrange
            var reader = Provider.GetRequiredService<IProductDao>();

            //Act
            var exists = await reader.Exists("Ariston");

            //Assert
            Check.That(exists).IsFalse();
        }

        [Fact]
        public async Task GivenSingleProduct_whenExists_thenReturnTrue()
        {
            //Arrange
            var reader = Provider.GetRequiredService<IProductDao>();
            await AddToDbContextAsync(ProductFakes.ProductWithAllPropsFilled1());

            //Act
            var exists = await reader.Exists("Ariston");

            //Assert
            Check.That(exists).IsTrue();
        }

        [Fact]
        public async Task GiveProductButWithDifferentName_whenExists_thenReturnFalse()
        {
            //Arrange
            var reader = Provider.GetRequiredService<IProductDao>();
            await AddToDbContextAsync(ProductFakes.ProductWithAllPropsFilled1());

            //Act
            var exists = await reader.Exists("Ariston2");

            //Assert
            Check.That(exists).IsFalse();
        }

        [Fact]
        public async Task GiveMultipleProducts_whenExists_thenReturnTrue()
        {
            //Arrange
            var reader = Provider.GetRequiredService<IProductDao>();
            await AddToDbContextAsync(ProductFakes.ProductWithAllPropsFilled1());
            await AddToDbContextAsync(ProductFakes.ProductWithAllPropsFilled2());

            //Act
            var exists = await reader.Exists("Ariston 2");

            //Assert
            Check.That(exists).IsTrue();
        }

       [Fact]
        public async Task GivenSingleProduct_whenOtherProductPresentWithSameName_returnsFalse()
        {
            //Arrange
            var product = ProductFakes.ProductWithAllPropsFilled1();
            await AddToDbContextAsync(product);

            //Act
            var productDao = Provider.GetRequiredService<IProductDao>();
            var exists = await productDao.OtherProductPresentWithName(product.Id, "Ariston");

            //Assert
            Check.That(exists).IsFalse();
        }
        

        [Fact]
        public async Task GivenSingleProduct_whenOtherProductPresentWithSameName_returnsTrue()
        {
            //Arrange
            var product1 = ProductFakes.ProductWithAllPropsFilled1();
            await AddToDbContextAsync(product1);
            await AddToDbContextAsync(ProductFakes.ProductWithAllPropsFilled2());

            //Act
            var productDao = Provider.GetRequiredService<IProductDao>();
            var exists = await productDao.OtherProductPresentWithName(product1.Id, "Ariston 2");

            //Assert
            Check.That(exists).IsTrue();
        }

        [Fact]
        public async Task GivenSingleProductQuantity_whenHasQuantityWithSameQuantity_returnsTrue()
        {
            //Arrange
            var product = ProductFakes.ProductWithAllPropsFilled1();
            await AddToDbContextAsync(product);

            //Act
            var productDao = Provider.GetRequiredService<IProductDao>();
            var exists = await productDao.HasMinimumQuantity(
                product.Id, 
                product.Quantity);

            //Assert
            Check.That(exists).IsTrue();
        }

        [Fact]
        public async Task GivenSingleProductQuantity_whenHasQuantityWithQuantityPlusOffByOne_returnsFalse()
        {
            //Arrange
            var product = ProductFakes.ProductWithAllPropsFilled1();
            await AddToDbContextAsync(product);

            //Act
            var productDao = Provider.GetRequiredService<IProductDao>();
            var exists = await productDao.HasMinimumQuantity(
                product.Id,
                product.Quantity + 1);

            //Assert
            Check.That(exists).IsFalse();
        }
        [Fact]
        public async Task GivenProductQuantity_whenHasQuantityWithQuantityMinusOffByOne_returnsTrue()
        {
            //Arrange
            var product = ProductFakes.ProductWithAllPropsFilled1();
            await AddToDbContextAsync(product);

            //Act
            var productDao = Provider.GetRequiredService<IProductDao>();
            var exists = await productDao.HasMinimumQuantity(
                product.Id,
                product.Quantity - 1);

            //Assert
            Check.That(exists).IsTrue();
        }
    }
}