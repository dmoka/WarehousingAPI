using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KaliGasService.Core.Extensions;

namespace KaliGasService.Core.Application.CQRS
{
    public abstract class Validator<T> : IValidator<T>
    {
        public IList<Error> Errors { get; set; } = new List<Error>();

        public abstract Task Validate(T @object);

        public bool IsValid()
        {
            return Errors.IsNullOrEmpty();
        }

        public void AddError(string errorCode, string errorMessage)
        {
            Errors.Add(Error.Create(errorCode, errorMessage));
        }

        public void AddError(Error error)
        {
            Errors.Add(error);
        }
    }
}
