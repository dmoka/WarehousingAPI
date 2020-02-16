using System;
using KaliGasService.Core.Application.CQRS;
using KaliGasService.TestHelpers.Extensions;
using NFluent;
using Xunit;

namespace KaliGasService.Core.Tests.Application.CQRS
{
    public class ErrorTest
    {
        [Theory]
        [InlineData("Code1", "Message1", "Code1: Message1")]
        [InlineData("Code2", "Message2", "Code2: Message2")]
        public void TestToStringMethod(string code, string message, string toStringResult)
        {
            Check.That(Error.Create(code, message).ToString()).IsEqualToValue(toStringResult);
        }

        [Fact]
        public void WhenCodeIsNull_ThenThrowsArgumentException()
        {
            Check.ThatCode(() => Error.Create(null, "message"))
                .Throws<ArgumentException>()
                .WithMessage("Error Code field is not filled");
        }

        [Fact]
        public void WhenCodeIsEmpty_ThenThrowsArgumentException()
        {
            Check.ThatCode(() => Error.Create("", "message"))
                .Throws<ArgumentException>()
                .WithMessage("Error Code field is not filled");
        }

        [Fact]
        public void WhenMessageIsNull_ThenThrowsArgumentException()
        {
            Check.ThatCode(() => Error.Create("Code", null))
                .Throws<ArgumentException>()
                .WithMessage("Error Message field is not filled");
        }

        [Fact]
        public void WhenmessageIsEmpty_ThenThrowsArgumentException()
        {
            Check.ThatCode(() => Error.Create("Code", ""))
                .Throws<ArgumentException>()
                .WithMessage("Error Message field is not filled");
        }
    }
}