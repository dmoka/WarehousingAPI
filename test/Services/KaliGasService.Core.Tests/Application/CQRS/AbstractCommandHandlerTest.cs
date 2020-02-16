using System;
using System.Reflection;
using System.Threading.Tasks;
using KaliGasService.Core.Application.CQRS;
using KaliGasService.Core.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NFluent;
using Warehousing.Data.Database;
using Warehousing.Testhelpers;
using Warehousing.Testhelpers.Asserters;
using Xunit;

namespace KaliGasService.Core.Tests.Application.CQRS
{
    public class AbstractCommandHandlerTest : IntegrationTestBase
    {

        public AbstractCommandHandlerTest()
        {
            RegisterHandlersPresentInCurrentAssembly();
        }

        [Fact]
        public async Task  GivenInvalidCommand_whenHandleCommand_thenReturnsCommandResultWithFailedStatus()
        {
            //Arrange
            var invalidCommand = new BasicCommand();

            //Act
            var commandResult = await SendAsync(invalidCommand);

            //Assert
            Check.That(commandResult.IsSuccess).IsFalse();
            Check.That(commandResult.Payload).IsFalse();

            ErrorAsserter.AssertErrors(commandResult.Errors,
                error1 => error1.HasErrorCode("1001").And.HasErrorMessage("Name is empty"),
                error2 => error2.HasErrorCode("1002").And.HasErrorMessage("Description is empty"));
        }

        [Fact]
        public async Task GivenValidCommand_whenHandleCommand_thenReturnsCommandResultWithSuccessStatus()
        {
            //Arrange
            var validCommand = new BasicCommand {Name = "Ariston", Description = "Desc"};

            //Act
            var commandResult = await SendAsync(validCommand);

            //Assert
            Check.That(commandResult.IsSuccess).IsTrue();
            Check.That(commandResult.Payload).IsTrue();
            Check.That(commandResult.Errors).IsEmpty();
        }

        [Fact]
        public async Task GivenHandlerThrowingExceptionFromTheDomain_whenHandleCommand_thenReturnsCommandResultWithFailedStatus()
        {
            //Arrange
            var validCommand = new BasicCommandHavingHandlerThrowingException() { Name = "Ariston", Description = "Desc" };

            //Act
            var commandResult = await SendAsync(validCommand);

            //Assert
            Check.That(commandResult.IsSuccess).IsFalse();
            Check.That(commandResult.Payload).IsFalse();

            ErrorAsserter.AssertErrors(commandResult.Errors,
                error1 => error1.HasErrorCode("UnexpectedError").And.HasErrorMessage("Something went wrong in the domain"));
        }

        private void RegisterHandlersPresentInCurrentAssembly()
        {
            Services.AddMediatR(Assembly.GetExecutingAssembly());
            Services.AddScoped<IValidator<BasicCommand>, BasicCommandValidator>();
            Services.AddScoped<IValidator<BasicCommandHavingHandlerThrowingException>, DummyBasicCommandValidator>();
            Provider = Services.BuildServiceProvider();
        }
    }

    public class BasicCommand : Command<Result<bool>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class BasicCommandHavingHandlerThrowingException : Command<Result<bool>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class BasicCommandValidator : Validator<BasicCommand>
    {
        public override Task Validate(BasicCommand @object)
        {
            if (@object.Name.IsNullOrEmpty())
            {
                Errors.Add(Error.Create("1001", "Name is empty"));
            }

            if (@object.Description.IsNullOrEmpty())
            {
                Errors.Add(Error.Create("1002", "Description is empty"));
            }

            return Task.CompletedTask;
        }
    }

    public class DummyBasicCommandValidator : Validator<BasicCommandHavingHandlerThrowingException>
    {
        public override Task Validate(BasicCommandHavingHandlerThrowingException @object)
        {
            return Task.CompletedTask;
        }
    }

    public class BasicCommandHandler : AbstractCommandHandler<BasicCommand, Result<bool>>
    {
        public override Task<Result<bool>> HandleRequest(BasicCommand request)
        {
            return Task.FromResult(Result<bool>.Success(true));
        }

        public BasicCommandHandler(IValidator<BasicCommand> validator) : base(validator)
        {
        }
    }

    public class BasicCommandHandlerThrowingException : AbstractCommandHandler<BasicCommandHavingHandlerThrowingException, Result<bool>>
    {
        public override Task<Result<bool>> HandleRequest(BasicCommandHavingHandlerThrowingException request)
        {
            throw new Exception("Something went wrong in the domain");
        }

        public BasicCommandHandlerThrowingException(IValidator<BasicCommandHavingHandlerThrowingException> validator) : base(validator)
        {
        }
    }
}