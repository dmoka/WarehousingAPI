using System;
using MediatR;

namespace KaliGasService.Core.Application.CQRS
{
    public abstract class Command<TCommandResult> : IRequest<TCommandResult> where TCommandResult : Result<bool>
    {
        public DateTime Timestamp { get; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

    }
}
