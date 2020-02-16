using System.Collections.Generic;
using KaliGasService.Core.Data;
using KaliGasService.Core.Types;
using NFluent;
using Xunit;

namespace KaliGasService.Core.Tests.Data
{
    public class BasicEnumConverterTest
    {
        private enum TestEnum
        {
            Enum1,
            Enum2,
            Enum3
        }

        private class TestConverter : BasicEnumConverter<string, TestEnum>
        {
            public TestConverter() : base(new BiDictionary<string, TestEnum>
            {
                {"A", TestEnum.Enum1},
                {"B", TestEnum.Enum2},
                {"C", TestEnum.Enum3}
            })
            { }
        }

        [Fact]
        public void TestCodeToEnum()
        {
            //Arrange
            var testConverter = new TestConverter();

            //Act and Assert
            Check.That(testConverter.CodeToEnum("A")).IsEqualTo(TestEnum.Enum1);
            Check.That(testConverter.CodeToEnum("B")).IsEqualTo(TestEnum.Enum2);
            Check.That(testConverter.CodeToEnum("C")).IsEqualTo(TestEnum.Enum3);
        }

        [Fact]
        public void TestEnumToCode()
        {
            //Arrange
            var testConverter = new TestConverter();

            //Act and Assert
            Check.That(testConverter.EnumToCode(TestEnum.Enum1)).IsEqualTo("A");
            Check.That(testConverter.EnumToCode(TestEnum.Enum2)).IsEqualTo("B");
            Check.That(testConverter.EnumToCode(TestEnum.Enum3)).IsEqualTo("C");
        }

        [Fact]
        public void TestGetKeys()
        {
            //Arrange
            var testConverter = new TestConverter();

            //Act
            var allCodes = testConverter.GetAllCodes();

            //Assert
            Assert.Equal(
                new List<string> { "A", "B", "C" },
                allCodes);
        }

        [Fact]
        public void TestGetEnums()
        {
            //Arrange
            var testConverter = new TestConverter();

            //Act
            var allCodes = testConverter.GetAllEnumTypes();

            //Assert
            Assert.Equal(
                new List<TestEnum> { TestEnum.Enum1, TestEnum.Enum2, TestEnum.Enum3 },
                allCodes);
        }
    }
}