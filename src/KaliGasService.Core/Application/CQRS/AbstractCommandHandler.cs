using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace KaliGasService.Core.Application.CQRS
{
    public abstract class AbstractCommandHandler<TCommand, TCommandResult> : IRequestHandler<TCommand, TCommandResult>
        where TCommand : Command<TCommandResult>
        where TCommandResult : Result<bool>
    {
        private readonly IValidator<TCommand> _validator;

        protected AbstractCommandHandler(IValidator<TCommand> validator)
        {
            _validator = validator;
        }

        public async Task<TCommandResult> Handle(TCommand request, CancellationToken cancellationToken)
        {
            await _validator.Validate(request);

            if (!_validator.IsValid())
            {
                return Result<bool>.Failure(_validator.Errors) as TCommandResult;
            }

            try
            {
                return await HandleRequest(request);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure(Error.CreateUnexpectedError(e.Message)) as TCommandResult;
            }
        }

        public abstract Task<TCommandResult> HandleRequest(TCommand request);

    }
}