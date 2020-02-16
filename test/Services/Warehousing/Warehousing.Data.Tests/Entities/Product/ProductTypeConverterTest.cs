using System;
using NFluent;
using Warehousing.Data.Entities.Product;
using Warehousing.Domain.Product;
using Xunit;

namespace Warehousing.Data.Tests.Entities.Product
{
    public class ProductTypeConverterTest
    {
        private readonly ProductTypeConverter _sut = new ProductTypeConverter();

        [Fact]
        public void AssertThatNumberOfConversionsEqualsToNumberOfEnums()
        {
            Check.That(_sut.GetAllEnumTypes()).CountIs(Enum.GetValues(typeof(ProductType)).Length);
        }

        [Fact]
        public void AssertEnumConversions()
        {
            Check.That(_sut.EnumToCode(ProductType.FlueGasDrainage)).IsEqualTo("FlueGasDrainage");
            Check.That(_sut.EnumToCode(ProductType.Controller)).IsEqualTo("Controller");
            Check.That(_sut.EnumToCode(ProductType.Filter)).IsEqualTo("Filter");
            Check.That(_sut.EnumToCode(ProductType.GasBoiler)).IsEqualTo("GasBoiler");
            Check.That(_sut.EnumToCode(ProductType.Other)).IsEqualTo("Other");
            Check.That(_sut.EnumToCode(ProductType.Pipe)).IsEqualTo("Pipe");
            Check.That(_sut.EnumToCode(ProductType.Valve)).IsEqualTo("Valve");
        }
    }
}