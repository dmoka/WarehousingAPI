using FluentAssertions;
using FluentAssertions.Json;
using Newtonsoft.Json.Linq;

namespace Warehousing.Testhelpers.Asserters
{
    public class JsonAsserter
    {

        public string Actual { get; }

        protected JsonAsserter(string actual)
        {
            Actual = actual;
        }

        public static JsonAsserter AssertThat(string actual)
        {
            return new JsonAsserter(actual);
        }


        public void IsEqualToArray(string expected)
        {
            var actualJTokenArray = JArray.Parse(Actual);
            var expectedJTokenArray = JArray.Parse(expected);

            for (int i = 0; i < actualJTokenArray.Count; i++)
            {
                actualJTokenArray[i].Should().BeEquivalentTo(expectedJTokenArray[i], $"Index for failing element: {i}");
            }
            //TODO: try catch json reader exception when string has no "" quotes around
        }

        public void IsEqualTo(string expected)
        {
            var expectedJToken = JToken.Parse(expected);
            var actualJToken = JToken.Parse(Actual);

            actualJToken.Should().BeEquivalentTo(expectedJToken);
        }

    }
}
