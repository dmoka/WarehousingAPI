using System.Collections.Generic;
using KaliGasService.Core.Application.CQRS;
using KaliGasService.TestHelpers.Extensions;
using NFluent;
using Warehousing.API.Application.Errors;
using Xunit;
namespace Warehousing.API.Tests.Application.Errors
{
    public class ErrorFormatterTest
    {
        [Fact]
        public void GivenNoError_whenFormat_thenReturnsEmpty()
        {
            var errorMessage = ErrorFormatter.Format(new List<Error>());

            Check.That(errorMessage).IsEmpty();
        }

        [Fact]
        public void GivenSingleError_whenFormat_thenReturnFormattedErrorMessage()
        {
            var errorMessage = ErrorFormatter.Format(new List<Error>()
            {
                Error.Create("code1", "message1")
            });

            Check.That(errorMessage).IsEqualToValue("code1: message1");
        }

        [Fact]
        public void GivenMultipleError_whenFormat_thenReturnFormattedErrorMessage()
        {
            var errorMessage = ErrorFormatter.Format(new List<Error>()
            {
                Error.Create("code1", "message1"),
                Error.Create("code2", "message2")
            });

            Check.That(errorMessage).IsEqualToValue(
                "code1: message1\n" +
                        "code2: message2");
        }
    }
}