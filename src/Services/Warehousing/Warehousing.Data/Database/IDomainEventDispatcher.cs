using System.Threading.Tasks;

namespace Warehousing.Data.Database
{
    public interface IDomainEventDispatcher
    {
        Task DispatchEventsAsync();
    }
}