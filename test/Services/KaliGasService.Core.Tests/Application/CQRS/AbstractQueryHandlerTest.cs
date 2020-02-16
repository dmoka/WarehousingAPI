using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using KaliGasService.Core.Application.CQRS;
using KaliGasService.TestHelpers.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NFluent;
using Warehousing.Data.Database;
using Warehousing.Testhelpers;
using Warehousing.Testhelpers.Asserters;
using Xunit;

namespace KaliGasService.Core.Tests.Application.CQRS
{
    public class AbstractQueryHandlerTest : IntegrationTestBase
    {
        public AbstractQueryHandlerTest()
        {
            RegisterHandlersPresentInCurrentAssembly();
        }


        [Fact]
        public async Task GivenBasicQuery_whenHandle_thenSuccessResultWithPayload()
        {
            var result = await SendAsync(new BasicQuery());

            Check.That(result.IsSuccess).IsTrue();
            Check.That(result.Payload.Name).IsEqualToValue("Result Name");
        }

        [Fact]
        public async Task GivenBasicQuery_whenHandlingThrowsException_thenReturnsFailureResult()
        {
            var result = await SendAsync(new BasicQueryHavingHandlerThrowingException());

            Check.That(result.IsSuccess).IsFalse();
            Check.That(result.Payload).IsNull();

            ErrorAsserter.AssertErrors(result.Errors,
                error1 => error1.HasErrorCode("UnexpectedError").And.HasErrorMessage("Db error"));
        }

        private void RegisterHandlersPresentInCurrentAssembly()
        {
            Services.AddMediatR(Assembly.GetExecutingAssembly());
            Provider = Services.BuildServiceProvider();
        }

    }

    public class ExampleDto
    {
        public string Name { get; set; }
    }

    public class BasicQuery : Query<Result<ExampleDto>>
    {
    }

    public class BasicQueryHavingHandlerThrowingException : Query<Result<ExampleDto>>
    {
    }

    public class BasicQueryHandler : AbstractQueryHandler<BasicQuery, Result<ExampleDto>, ExampleDto>
    {
        public override Task<Result<ExampleDto>> HandleQuery(BasicQuery request)
        {
            return Task.FromResult(Result<ExampleDto>.Success(new ExampleDto() { Name = "Result Name" }));
        }
    }

    public class BasicQueryHandlerThrowingException : AbstractQueryHandler<BasicQueryHavingHandlerThrowingException, Result<ExampleDto>, ExampleDto>
    {
        public override Task<Result<ExampleDto>> HandleQuery(BasicQueryHavingHandlerThrowingException request)
        {
            throw new Exception("Db error");
        }
    }


}