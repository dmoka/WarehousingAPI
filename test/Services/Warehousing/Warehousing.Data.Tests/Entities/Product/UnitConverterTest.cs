using System;
using NFluent;
using Warehousing.Data.Entities.Product;
using Warehousing.Domain.Product;
using Xunit;

namespace Warehousing.Data.Tests.Entities.Product
{
    public class UnitConverterTest
    {
        private readonly UnitConverter _sut = new UnitConverter();

        [Fact]
        public void AssertThatNumberOfConversionsEqualsToNumberOfEnums()
        {
            Check.That(_sut.GetAllEnumTypes()).CountIs(Enum.GetValues(typeof(Unit)).Length);
        }

        [Fact]
        public void AssertEnumConversions()
        {
            Check.That(_sut.EnumToCode(Unit.Piece)).IsEqualTo("Piece");
            Check.That(_sut.EnumToCode(Unit.Package)).IsEqualTo("Package");
        }

    }
}