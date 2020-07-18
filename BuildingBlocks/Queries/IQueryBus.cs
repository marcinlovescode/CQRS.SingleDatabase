using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.Queries
{
    public interface IQueryBus
    {
        Task<TResult> SendAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
    }
}