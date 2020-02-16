using System.Collections.Generic;
using KaliGasService.Core.Extensions;

namespace KaliGasService.Core.Application.CQRS
{
    public class Result<T> 
    {
        public T Payload { get;  }

        public IEnumerable<Error> Errors { get; } = new List<Error>();

        public bool IsSuccess => Errors.IsNullOrEmpty();

        public Result(T payload)
        {
            Payload = payload;
        }

        public Result(params Error[] errors)
        {
            Errors = errors;
        }

        public Result(IEnumerable<Error> errors)
        {
            Errors = errors;
        }

        public static Result<T> Success(T payload)
            => new Result<T>(payload);

        public static Result<T> Failure(params Error[] errorMessages)
            => new Result<T>(errorMessages);

        public static Result<T> Failure(IEnumerable<Error> errorMessages)
            => new Result<T>(errorMessages);

        public static implicit operator bool(Result<T> result) => result.IsSuccess;

    }
}
