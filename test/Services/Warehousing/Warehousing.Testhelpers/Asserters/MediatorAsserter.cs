using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using KaliGasService.Core.Application.CQRS;
using MediatR;
using Moq;

namespace Warehousing.Testhelpers.Asserters
{
    public class MediatorAsserter
    {
        public Mock<IMediator> MockedMediator;

        public static MediatorAsserter AssertThat(Mock<IMediator> mockedMediator)
        {
            return new MediatorAsserter(mockedMediator);
        }

        public MediatorAsserter(Mock<IMediator> mockedMediator)
        {
            MockedMediator = mockedMediator;
        }

        public MediatorAsserter SentQueryWithResponse<TRequest, TResponse>() where TRequest : Query<TResponse>
        {
            MockedMediator.Verify(mock => mock.Send(It.IsAny<TRequest>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public MediatorAsserter SentCommand<TRequest>() where TRequest : Command<Result<bool>>
        {
            MockedMediator.Verify(mock => mock.Send(It.IsAny<TRequest>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public MediatorAsserter SentCommand<TRequest>(Expression<Func<TRequest, bool>> commandChecker) where TRequest : Command<Result<bool>>
        {
            MockedMediator.Verify(mock => mock.Send(It.Is<TRequest>(commandChecker), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }
    }
}
