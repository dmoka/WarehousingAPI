using System;
using NFluent;
using Warehousing.Data.Entities.Product;
using Warehousing.Domain.Product;
using Xunit;

namespace Warehousing.Data.Tests.Entities.Product
{
    public class ProductHistoryTypeConverterTest
    {
        private readonly ProductHistoryTypeConverter _sut = new ProductHistoryTypeConverter();

        [Fact]
        public void AssertThatNumberOfConversionsEqualsToNumberOfEnums()
        {
            Check.That(_sut.GetAllEnumTypes()).CountIs(Enum.GetValues(typeof(ProductHistoryType)).Length);
        }

        [Fact]
        public void AssertEnumConversions()
        {
            Check.That(_sut.EnumToCode(ProductHistoryType.Pick)).IsEqualTo("Pick");
            Check.That(_sut.EnumToCode(ProductHistoryType.Unpick)).IsEqualTo("Unpick");
        }

    }
}