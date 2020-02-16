using System.Threading.Tasks;

namespace KaliGasService.Core.Application.CQRS
{
    public class NullValidator<T> : Validator<T>
    {
        public override Task Validate(T @object)
        {
            return Task.CompletedTask;
        }
    }
}
