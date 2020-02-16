using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace KaliGasService.Core.Application.CQRS
{
    //TODO: add logging for here and command handler, in decorator or pipeline

    public abstract class AbstractQueryHandler<TQuery, TQueryResult, TResultValue> : IRequestHandler<TQuery, TQueryResult>
        where TQuery : Query<TQueryResult>
        where TQueryResult : class
    {
        public async Task<TQueryResult> Handle(TQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await HandleQuery(request);
            }
            catch (Exception e)
            {
                return Result<TResultValue>.Failure(Error.CreateUnexpectedError(e.Message)) as TQueryResult;
            }
        }

        public abstract Task<TQueryResult> HandleQuery(TQuery request);
    }
}