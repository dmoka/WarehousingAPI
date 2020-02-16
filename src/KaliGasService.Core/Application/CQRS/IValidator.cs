using System.Collections.Generic;
using System.Threading.Tasks;

namespace KaliGasService.Core.Application.CQRS
{
    public interface IValidator<in T>
    {
        IList<Error> Errors { get; set; }
        
        Task Validate(T @object);

        bool IsValid();
    }
}