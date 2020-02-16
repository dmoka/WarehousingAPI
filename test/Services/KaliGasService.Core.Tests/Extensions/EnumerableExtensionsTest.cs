using System.Collections.Generic;
using KaliGasService.Core.Extensions;
using NFluent;
using Xunit;

namespace KaliGasService.Core.Tests.Extensions
{
    public class EnumerableExtensionsTest
    {
        [Fact]
        public void GivenNullList_whenIsNullOrEmpty_ThenReturnsTrue()
        {
            IEnumerable<string> list = null;

            Check.That(list.IsNullOrEmpty()).IsTrue();
        }

        [Fact]
        public void GivenEmptyList_whenIsNullOrEmpty_ThenReturnsTrue()
        {
            var list = new List<string>();

            Check.That(list.IsNullOrEmpty()).IsTrue();
        }


        [Fact]
        public void GivenListWithData_whenIsNullOrEmpty_ThenReturnsTrue()
        {
            var list = new List<string>();
            list.Add("Dani");

            Check.That(list.IsNullOrEmpty()).IsFalse();
        }
    }
}