using System;
using MediatR;

namespace KaliGasService.Core.Application.CQRS
{
    public class  Query<TQueryResult> : IRequest<TQueryResult>
    {
        public DateTime Timestamp { get; }

        protected Query()
        {
            Timestamp = DateTime.Now;
        }
    }
}
