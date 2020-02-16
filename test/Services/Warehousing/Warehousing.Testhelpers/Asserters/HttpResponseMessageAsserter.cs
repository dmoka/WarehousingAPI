using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KaliGasService.TestHelpers.Asserters;
using NFluent;

namespace Warehousing.Testhelpers.Asserters
{
    public class HttpResponseMessageAsserter : AbstractAsserter<HttpResponseMessage, HttpResponseMessageAsserter>
    {

        public static HttpResponseMessageAsserter AssertThat(HttpResponseMessage httpResponseMessage)
        {
            return new HttpResponseMessageAsserter(httpResponseMessage);
        }

        public HttpResponseMessageAsserter(HttpResponseMessage actual) : base(actual)
        {
        }

        public HttpResponseMessageAsserter HasStatusCode(HttpStatusCode statusCode)
        {
            Check.That(Actual.StatusCode).IsEqualTo(statusCode);

            return this;
        }

        public async Task<HttpResponseMessageAsserter> HasJsonInBody(string expectedJson)
        {
            JsonAsserter.AssertThat(await Actual.Content.ReadAsStringAsync())
                .IsEqualTo(expectedJson);

            return this;
        }

        public async Task<HttpResponseMessageAsserter> HasJsonArrayInBody(string expectedJson)
        {
            JsonAsserter.AssertThat(await Actual.Content.ReadAsStringAsync())
                .IsEqualToArray(expectedJson);

            return this;
        }
    }
}
