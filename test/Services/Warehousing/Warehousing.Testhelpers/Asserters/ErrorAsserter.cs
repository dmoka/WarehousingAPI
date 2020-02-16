using System;
using System.Collections.Generic;
using System.Text;
using KaliGasService.Core.Application.CQRS;
using KaliGasService.TestHelpers.Asserters;
using KaliGasService.TestHelpers.Extensions;
using NFluent;

namespace Warehousing.Testhelpers.Asserters
{
    public class ErrorAsserter : AbstractAsserter<Error, ErrorAsserter>
    {
        public static ErrorAsserter AssertThat(Error actual)
        {
            return new ErrorAsserter(actual);
        }

        public ErrorAsserter(Error actual) : base(actual)
        {
        }

        public ErrorAsserter HasErrorCode(string errorCode)
        {
            Check.WithCustomMessage(nameof(Actual.Code)).That(Actual.Code).IsEqualToValue(errorCode);
            return this;
        }

        public ErrorAsserter HasErrorMessage(string errorMessage)
        {
            Check.WithCustomMessage(nameof(Actual.Message)).That(Actual.Message).IsEqualToValue(errorMessage);
            return this;
        }

        public static void AssertErrors(IEnumerable<Error> errors, params Action<ErrorAsserter>[] errorAsserters)
        {
            AssertEntries(e => new ErrorAsserter(e), errors, errorAsserters );
        }

    }
}
