using NFluent;
using NFluent.Extensibility;
using Xunit;
using Xunit.Sdk;

namespace KaliGasService.TestHelpers.Extensions
{
    public static class StringCheckExtensions
    {
        public static ICheckLink<ICheck<string>> IsEqualToValue(this ICheck<string> check, string expected)
        {
            ExtensibilityHelper.BeginCheck(check)
                .FailIfNull()
                .OnNegate("On Negate is not implemented")
                .FailWhen(s => s != expected, GetErrorMessage(check, expected))
                .DefineExpectedValue(expected)
                .EndCheck();

            return ExtensibilityHelper.BuildCheckLink(check);
        }

        private static string GetErrorMessage(ICheck<string> check, string expected)
        {
            string errorMessage = null;
            try
            {
                Assert.Equal(expected, ExtensibilityHelper.ExtractChecker(check).Value);
            }
            catch (EqualException e)
            {
                errorMessage = e.Message;
            }

            return errorMessage;
        }
    }
}
